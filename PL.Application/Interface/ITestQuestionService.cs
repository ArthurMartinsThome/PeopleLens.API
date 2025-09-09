using PL.Domain.Model;
using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Util.Model.Paged;

namespace PL.Application.Interface
{
    public interface ITestQuestionService
    {
        Task<IResult<Paged<TestQuestion>>> SearchPaged(TestQuestionFilter filter, int skip, int take);
        Task<IResult<IEnumerable<TestQuestion>>> Search(TestQuestionFilter filter);
        Task<IResult<int>> Insert(TestQuestion obj);
        Task<IResult<bool>> Update(TestQuestion obj);
    }
}