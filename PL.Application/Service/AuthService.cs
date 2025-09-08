using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PL.Adapter.MySql.Interface;
using PL.Application.Interface;
using PL.Domain.Dto;
using PL.Domain.Model;
using PL.Domain.Model.Enum;
using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PL.Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly IOptions<EnvironmentSettings> _envOptions;
        private readonly IAuthDataSource _dataSource;
        private readonly IUserService _userService;

        public AuthService(
            IOptions<EnvironmentSettings> envOptions,
            IAuthDataSource dataSource,
            IUserService userService)
        {
            _envOptions = envOptions;
            _dataSource = dataSource;
            _userService = userService;
        }

        private IEnumerable<Filter> GetFilters(AuthFilter filter)
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
            if (filter.UserId.HasValue && filter.UserId > 0)
                filterList.Add(Filter.Create("UserId", EOperator.Equal, filter.UserId));
            if (!string.IsNullOrEmpty(filter.Username))
                filterList.Add(Filter.Create("Username", EOperator.Equal, filter.Username));
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

        public async Task<IResult<IEnumerable<Auth>>> Search(AuthFilter filter)
        {
            try
            {
                var result = await _dataSource.Search(GetFilters(filter));
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Auth>>.Error("Houve uma falha ao buscar a(s) autenticação(ões).", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.Auth obj)
        {
            try
            {
                var result = await _dataSource.Insert(obj);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Houve uma falha ao cadastrar a autenticação.", ex);
            }
        }

        public async Task<IResult<LoginResponseDto>> AuthenticateAsync(string username, string passwordHash)
        {
            var auth = await Search(new AuthFilter { Username = username });

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

        /// <summary>
        /// Cadastra um novo usuário no sistema.
        /// Valida os dados, verifica a existência do username, faz o hash da senha e insere no banco.
        /// </summary>
        /// <param name="obj">Dados do usuário a ser cadastrado.</param>
        /// <returns>Um IResult contendo o User cadastrado ou um erro.</returns>
        public async Task<IResult<Auth>> RegisterUserAsync(RegisterDto obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Username))
                return DefaultResult<Auth>.Break("O username é obrigatório.");
            if (string.IsNullOrWhiteSpace(obj.Password))
                return DefaultResult<Auth>.Break("A senha é obrigatória.");
            if (string.IsNullOrWhiteSpace(obj.Name))
                return DefaultResult<Auth>.Break("O nome é obrigatório.");
            if (string.IsNullOrWhiteSpace(obj.Cellphone))
                return DefaultResult<Auth>.Break("O celular é obrigatório.");

            try
            {
                var searchResult = await Search(new AuthFilter { Username = obj.Username });

                if (searchResult.Succeded && searchResult.HasData)
                    return DefaultResult<Auth>.Break($"Já existe um usuário com o username '{obj.Username}'.");
                else if (!searchResult.Succeded)
                    return DefaultResult<Auth>.Break("Houve uma falha ao verificar a existência do usuário.");

                var newUser = new User
                {
                    Cellphone = obj.Cellphone.Trim(),
                    Name = obj.Name.Trim(),
                    CreatedAt = DateTime.UtcNow,
                    StatusId = EStatus.Ativo
                };

                var insertResult = await _userService.Insert(newUser);

                if (insertResult.Succeded && insertResult.HasData)
                {
                    newUser.Id = insertResult.Data;

                    var newAuth = new Auth
                    {
                        UserId = newUser.Id.Value,
                        Username = obj.Username.Trim(),
                        Password = obj.Password.Trim(),
                        Role = ERole.User,
                        StatusId = EStatus.Ativo,
                        CreatedAt = DateTime.UtcNow
                    };

                    var inserAuth = await Insert(newAuth);

                    if (inserAuth.Succeded && inserAuth.HasData)
                    {
                        newAuth.User = newUser;
                        return DefaultResult<Auth>.Create(newAuth);
                    }

                    return DefaultResult<Auth>.Break("Houve uma falha ao inserir o usuário no banco de dados.");
                }
                else
                    return DefaultResult<Auth>.Break("Houve uma falha ao inserir o usuário no banco de dados.");
            }
            catch (Exception ex)
            {
                return DefaultResult<Auth>.Error("Ocorreu um erro inesperado durante o cadastro do usuário.", ex);
            }
        }

        private string GenerateJwtToken(Auth obj)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_envOptions.Value.JWT_Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", obj.Id.ToString()),
                    new Claim("username", obj.Username),
                    new Claim("role", obj.Role.ToString()),
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