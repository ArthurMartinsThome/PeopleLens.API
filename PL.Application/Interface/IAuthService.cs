using PL.Domain.Dto;
using PL.Domain.Model;
using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult.Interface;

namespace PL.Application.Interface
{
    public interface IAuthService
    {
        Task<IResult<IEnumerable<Auth>>> Search(AuthFilter filter);
        Task<IResult<LoginResponseDto>> AuthenticateAsync(string username, string passwordHash);
        Task<IResult<Auth>> RegisterUserAsync(RegisterDto obj);
    }
}