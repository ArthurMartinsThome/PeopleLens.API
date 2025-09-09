using PL.Domain.Model;
using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Util.Model.Paged;

namespace PL.Application.Interface
{
    public interface ITestAppliedService
    {
        Task<IResult<Paged<TestApplied>>> SearchPaged(TestAppliedFilter filter, int skip, int take);
        Task<IResult<IEnumerable<TestApplied>>> Search(TestAppliedFilter filter);
        Task<IResult<int>> Insert(TestApplied obj);
        Task<IResult<bool>> Update(Domain.Model.TestApplied obj);
    }
}