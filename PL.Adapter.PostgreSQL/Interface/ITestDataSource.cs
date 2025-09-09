using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util.Model.Paged;

namespace PL.Adapter.PostgreSQL.Interface
{
    public interface ITestDataSource
    {
        Task<IResult<Paged<Domain.Model.Test>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take);
        Task<IResult<IEnumerable<Domain.Model.Test>>> Search(IEnumerable<Filter> filters);
        Task<IResult<int>> Insert(Domain.Model.Test obj);
        Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.Test oldObj, Domain.Model.Test newObj);
    }
}