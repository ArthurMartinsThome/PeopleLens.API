using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util.Model.Paged;

namespace PL.Adapter.PostgreSQL.Interface
{
    public interface ITestTypeDataSource
    {
        Task<IResult<Paged<Domain.Model.TestType>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take);
        Task<IResult<IEnumerable<Domain.Model.TestType>>> Search(IEnumerable<Filter> filters);
        Task<IResult<int>> Insert(Domain.Model.TestType obj);
        Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.TestType oldObj, Domain.Model.TestType newObj);
    }
}