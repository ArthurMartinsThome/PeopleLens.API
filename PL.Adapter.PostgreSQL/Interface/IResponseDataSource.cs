using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util.Model.Paged;

namespace PL.Adapter.PostgreSQL.Interface
{
    public interface IResponseDataSource
    {
        Task<IResult<Paged<Domain.Model.Response>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take);
        Task<IResult<IEnumerable<Domain.Model.Response>>> Search(IEnumerable<Filter> filters);
        Task<IResult<int>> Insert(Domain.Model.Response obj);
        Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.Response oldObj, Domain.Model.Response newObj);
    }
}