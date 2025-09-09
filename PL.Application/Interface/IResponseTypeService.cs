using PL.Domain.Model;
using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Util.Model.Paged;

namespace PL.Application.Interface
{
    public interface IResponseTypeService
    {
        Task<IResult<Paged<ResponseType>>> SearchPaged(ResponseTypeFilter filter, int skip, int take);
        Task<IResult<IEnumerable<ResponseType>>> Search(ResponseTypeFilter filter);
        Task<IResult<int>> Insert(ResponseType obj);
        Task<IResult<bool>> Update(Domain.Model.ResponseType obj);
    }
}