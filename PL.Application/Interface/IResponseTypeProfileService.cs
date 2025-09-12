using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Util.Model.Paged;

namespace PL.Application.Interface
{
    public interface IResponseTypeProfileService
    {
        Task<IResult<Paged<Domain.Model.ResponseTypeProfile>>> SearchPaged(ResponseTypeProfileFilter filter, int skip, int take);
        Task<IResult<IEnumerable<Domain.Model.ResponseTypeProfile>>> Search(ResponseTypeProfileFilter filter);
        Task<IResult<int>> Insert(Domain.Model.ResponseTypeProfile obj);
        Task<IResult<bool>> Update(Domain.Model.ResponseTypeProfile obj);
    }
}