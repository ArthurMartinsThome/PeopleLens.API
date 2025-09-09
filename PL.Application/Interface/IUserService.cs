using PL.Domain.Dto;
using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Util.Model.Paged;

namespace PL.Application.Interface
{
    public interface IUserService
    {
        Task<IResult<Paged<Domain.Model.User>>> SearchPaged(UserFilter filter, int skip, int take);
        Task<IResult<IEnumerable<Domain.Model.User>>> Search(UserFilter filter);
        Task<IResult<int>> Insert(Domain.Model.User obj);
        Task<IResult<bool>> Update(Domain.Model.User obj);
        Task<IResult<LoginResponseDto>> AuthenticateAsync(string username, string passwordHash);
        Task<IResult<Domain.Model.User>> Registeruser(RegisterDto obj);
    }
}