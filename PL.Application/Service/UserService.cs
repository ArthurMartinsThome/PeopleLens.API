using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mysqlx.Crud;
using Org.BouncyCastle.Asn1.Ocsp;
using PL.Adapter.PostgreSQL.DataSource;
using PL.Adapter.PostgreSQL.Interface;
using PL.Application.Interface;
using PL.Domain.Dto;
using PL.Domain.Model;
using PL.Domain.Model.Enum;
using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util.Model;
using PL.Infra.Util.Model.Paged;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PL.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserDataSource _dataSource;
        private readonly IPersonService _personService;
        private readonly ICompanyService _companyService;
        private readonly IPersonCompanyService _personCompanyService;
        private readonly IOptions<EnvironmentSettings> _envOptions;

        public UserService(
            IOptions<EnvironmentSettings> envOptions, 
            IUserDataSource? dataSource,
            IPersonService personService,
            ICompanyService companyService,
            IPersonCompanyService personCompanyService)
        {
            _dataSource = dataSource;
            _envOptions = envOptions;
            _personService = personService;
            _companyService = companyService;
            _personCompanyService = personCompanyService;
        }

        private IEnumerable<Filter> GetFilters(UserFilter filter)
        {
            var filterList = new List<Filter>();
            if (filter == null)
                return filterList;
            if (filter.Id.HasValue && filter.Id > 0)
                filterList.Add(Filter.Create("Id", EOperator.Equal, new[] { filter.Id.Value }));
            if (filter.Ids != null && filter.Ids.Any())
                filterList.Add(Filter.Create("Id", EOperator.In, filter.Ids));
            if (filter.StatusId != null && filter.StatusId.Any())
                filterList.Add(Filter.Create("StatusId", EOperator.In, filter.StatusId));
            if (filter.PersonId.HasValue && filter.PersonId > 0)
                filterList.Add(Filter.Create("PersonId", EOperator.Equal, filter.PersonId));
            if (!string.IsNullOrEmpty(filter.Email))
                filterList.Add(Filter.Create("Email", EOperator.Equal, filter.Email));
            if (filter.CreatedFrom != null)
                filterList.Add(Filter.Create("CreatedAt", EOperator.GreaterThanOrEqual, filter.CreatedFrom));
            if (filter.CreatedTo != null)
                filterList.Add(Filter.Create("CreatedAt", EOperator.LessThanOrEqual, filter.CreatedTo));
            if (filter.HideInactive)
                filterList.Add(Filter.Create("StatusId", EOperator.NotEqual, new[] { EStatus.Inativo }));
            if (filter.HideDeleted)
                filterList.Add(Filter.Create("StatusId", EOperator.NotEqual, new[] { EStatus.Excluido }));
            return filterList;
        }

        public async Task<IResult<Paged<Domain.Model.User>>> SearchPaged(UserFilter filter, int skip, int take)
        {
            try
            {
                var result = await _dataSource.SearchPaged(GetFilters(filter), skip, take);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.User>>.Error("Houve uma falha ao buscar os dados do(s) usuário(s).", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.User>>> Search(UserFilter filter, bool? fetchAll = false)
        {
            try
            {
                var filters = GetFilters(filter);
                var result = await _dataSource.Search(filters);
                if (fetchAll == true && result.Succeded && result.HasData)
                    await FetchAllDependencies(result.Data, fetchAll);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.User>>.Error("Houve uma falha ao buscar os dados do usuário.", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.User obj)
        {
            try
            {
                obj.CreatedAt = obj.UpdatedAt = DateTime.UtcNow;
                obj.StatusId = EStatus.Ativo;
                var result = await _dataSource.Insert(obj);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Houve uma falha ao cadastrar o usuário.", ex);
            }
        }

        public async Task<IResult<bool>> Update(Domain.Model.User obj)
        {
            try
            {
                if (!obj.Id.HasValue || obj.Id <= 0)
                    return DefaultResult<bool>.Error("Falta referenciar qual o id para editar o usuário.", new Exception($"Update: newObj.Id == (null/0)"));

                var oldCompanyResult = await Search(new UserFilter { Id = obj.Id });
                if (!oldCompanyResult.Succeded || oldCompanyResult.Data == null)
                    return DefaultResult<bool>.Break("Usuário não encontrado para atualização.");

                var oldObj = oldCompanyResult.Data.First();
                var newObj = (Domain.Model.User)oldCompanyResult.Data.First().Clone();

                if (obj.PersonId != null && obj.PersonId > 0)
                    newObj.PersonId = obj.PersonId;
                if (obj.StatusId != null && obj.StatusId > 0)
                    newObj.StatusId = obj.StatusId;
                if (obj.RoleId != null && obj.RoleId > 0)
                    newObj.RoleId = obj.RoleId;
                if (!string.IsNullOrEmpty(obj.Email))
                    newObj.Email = obj.Email;
                if (!string.IsNullOrEmpty(obj.Password))
                    newObj.Password = obj.Password;

                if (PL.Infra.Util.ObjectCompare.EqualObjects(oldObj, newObj))
                {
                    var resultPerson = await _personService.Update(obj.Person);
                    if (!resultPerson.Succeded || !resultPerson.HasData || resultPerson.StatusCode != System.Net.HttpStatusCode.OK)
                        return DefaultResult<bool>.Break("Erro ao atualizar a pessoa.");

                    return DefaultResult<bool>.Create(true);
                }

                var filters = GetFilters(new UserFilter() { Id = obj.Id });
                var result = await _dataSource.Update(filters, oldObj, newObj);
                if(result.Succeded && result.HasData && result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var resultPerson = await _personService.Update(obj.Person);
                    if(!resultPerson.Succeded || !resultPerson.HasData || resultPerson.StatusCode != System.Net.HttpStatusCode.OK)
                        return DefaultResult<bool>.Break("Erro ao atualizar a pessoa.");
                }

                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Houve uma falha ao editar o usuário.", ex);
            }
        }

        public async Task<IResult<User>> Registeruser(RegisterDto obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Email))
                return DefaultResult<User>.Break("O email é obrigatório.");
            if (string.IsNullOrWhiteSpace(obj.Password))
                return DefaultResult<User>.Break("A senha é obrigatória.");
            if (string.IsNullOrWhiteSpace(obj.Name))
                return DefaultResult<User>.Break("O nome é obrigatório.");
            if (obj.Birthday == null || obj.Birthday <= DateTime.MinValue)
                return DefaultResult<User>.Break("A data de nascimento é obrigatória.");
            if (obj.CompanyId == null || obj.CompanyId <= 0)
                return DefaultResult<User>.Break("O id da empresa é obrigatório.");

            try
            {
                var person = new Person
                {
                    Name = obj.Name,
                    Birthday = obj.Birthday
                };
                var personResult = await _personService.Insert(person);
                if (!personResult.Succeded || !personResult.HasData || personResult.StatusCode != System.Net.HttpStatusCode.OK)
                    return personResult.ChangeType<User>(default);

                var personId = personResult.Data;

                var companyResult = await _companyService.Search(new CompanyFilter { Id = obj.CompanyId });
                if (!companyResult.Succeded || !companyResult.HasData || companyResult.StatusCode != System.Net.HttpStatusCode.OK)
                    return companyResult.ChangeType<User>(default);

                var defaultCompanyId = companyResult.Data.First().Id.Value;

                var personCompany = new PersonCompany
                {
                    PersonId = personId,
                    CompanyId = defaultCompanyId,
                    StatusId = EStatus.Ativo
                };
                var personCompanyResult = await _personCompanyService.Insert(personCompany);
                if (!personCompanyResult.Succeded || !personCompanyResult.HasData || personCompanyResult.StatusCode != System.Net.HttpStatusCode.OK)
                    return personCompanyResult.ChangeType<User>(default);

                var user = new User
                {
                    PersonId = personId,
                    Email = obj.Email,
                    Password = obj.Password,
                    RoleId = obj.RoleId != null ? (ERole)obj.RoleId : ERole.User,
                    StatusId = EStatus.Ativo
                };

                var userResult = await Insert(user);
                if (!userResult.Succeded || !userResult.HasData || userResult.StatusCode != System.Net.HttpStatusCode.OK)
                    return userResult.ChangeType<User>(default);

                user.Password = null;
                return DefaultResult<User>.Create(user);
            }
            catch (Exception ex)
            {
                return DefaultResult<User>.Error("Ocorreu um erro inesperado durante o cadastro do usuário.", ex);
            }
        }

        public async Task<IResult<LoginResponseDto>> AuthenticateAsync(string username, string passwordHash)
        {
            var auth = await Search(new UserFilter { Email = username });

            if(!auth.Succeded || auth.StatusCode != System.Net.HttpStatusCode.OK)
                return DefaultResult<LoginResponseDto>.Break(auth.LastMessage);
            if (!auth.HasData)
                return DefaultResult<LoginResponseDto>.Break("Usuário não existe.");
            if (auth.Data.First().Password != passwordHash)
                return DefaultResult<LoginResponseDto>.Break("Senha incorreta para esse usuário.");

            var token = GenerateJwtToken(auth.Data.First());

            return DefaultResult<LoginResponseDto>.Create(new LoginResponseDto
            {
                Token = token
            });
        }

        private string GenerateJwtToken(User obj)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_envOptions.Value.JWT_Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", obj.Id.ToString()),
                    new Claim("username", obj.Email),
                    new Claim("role", obj.RoleId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                NotBefore = DateTime.UtcNow.AddMinutes(-15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _envOptions.Value.JWT_Issuer,
                Audience = _envOptions.Value.JWT_Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task FetchAllDependencies(IEnumerable<User> orderList, bool? fetchAll = false)
        {
            try
            {
                if (orderList == null || !orderList.Any()) return;
                await FetchPerson(orderList, fetchAll);
            }
            catch (Exception ex)
            {

            }
        }

        private async Task FetchPerson(IEnumerable<User> objList, bool? fetchAll = false)
        {
            try
            {
                var searchObjList = await _personService.Search(new Domain.Model.Filter.PersonFilter()
                {
                    Ids = objList.Select(x => x.PersonId.Value).ToList()
                });
                if (searchObjList.Succeded && searchObjList.HasData)
                {
                    foreach (var item in objList)
                    {
                        var currentObj = searchObjList.Data.Where(x => x.Id.Value == item.PersonId).First();
                        item.Person = currentObj;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}