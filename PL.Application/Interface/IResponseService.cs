using PL.Domain.Model;
using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Util.Model.Paged;

namespace PL.Application.Interface
{
    public interface IResponseService
    {
        Task<IResult<Paged<Response>>> SearchPaged(ResponseFilter filter, int skip, int take);
        Task<IResult<IEnumerable<Response>>> Search(ResponseFilter filter);
        Task<IResult<int>> Insert(Response obj);
        Task<IResult<bool>> Update(Response obj);
    }
}