using PL.Domain.Model;
using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Util.Model.Paged;

namespace PL.Application.Interface
{
    public interface IQuestionService
    {
        Task<IResult<Paged<Question>>> SearchPaged(QuestionFilter filter, int skip, int take);
        Task<IResult<IEnumerable<Question>>> Search(QuestionFilter filter);
        Task<IResult<int>> Insert(Question obj);
        Task<IResult<bool>> Update(Question obj);
    }
}