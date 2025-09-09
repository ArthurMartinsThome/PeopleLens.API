using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util.Model.Paged;

namespace PL.Adapter.PostgreSQL.Interface
{
    public interface ITestQuestionDataSource
    {
        Task<IResult<Paged<Domain.Model.TestQuestion>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take);
        Task<IResult<IEnumerable<Domain.Model.TestQuestion>>> Search(IEnumerable<Filter> filters);
        Task<IResult<int>> Insert(Domain.Model.TestQuestion obj);
        Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.TestQuestion oldObj, Domain.Model.TestQuestion newObj);
    }
}