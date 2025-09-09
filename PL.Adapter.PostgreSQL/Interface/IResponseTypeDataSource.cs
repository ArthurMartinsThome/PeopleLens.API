using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util.Model.Paged;

namespace PL.Adapter.PostgreSQL.Interface
{
    public interface IResponseTypeDataSource
    {
        Task<IResult<Paged<Domain.Model.ResponseType>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take);
        Task<IResult<IEnumerable<Domain.Model.ResponseType>>> Search(IEnumerable<Filter> filters);
        Task<IResult<int>> Insert(Domain.Model.ResponseType obj);
        Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.ResponseType oldObj, Domain.Model.ResponseType newObj);
    }
}