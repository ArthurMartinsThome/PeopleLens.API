using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util.Model.Paged;

namespace PL.Adapter.PostgreSQL.Interface
{
    public interface ITestAppliedDataSource
    {
        Task<IResult<Paged<Domain.Model.TestApplied>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take);
        Task<IResult<IEnumerable<Domain.Model.TestApplied>>> Search(IEnumerable<Filter> filters);
        Task<IResult<int>> Insert(Domain.Model.TestApplied obj);
        Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.TestApplied oldObj, Domain.Model.TestApplied newObj);
    }
}