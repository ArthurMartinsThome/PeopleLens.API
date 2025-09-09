using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IOptions<EnvironmentSettings> _envOptions;

        public UserService(
            IOptions<EnvironmentSettings> envOptions, 
            IUserDataSource? dataSource)
        {
            _dataSource = dataSource;
            _envOptions = envOptions;
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

        public async Task<IResult<IEnumerable<Domain.Model.User>>> Search(UserFilter filter)
        {
            try
            {
                var filters = GetFilters(filter);
                var result = await _dataSource.Search(filters);
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
                obj.StatusId = EStatus.Ativo;
                var result = await _dataSource.Insert(obj);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Houve uma falha ao cadastrar o usuário.", ex);
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
    }
}