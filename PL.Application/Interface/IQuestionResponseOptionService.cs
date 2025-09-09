using PL.Domain.Model;
using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Util.Model.Paged;

namespace PL.Application.Interface
{
    public interface IQuestionResponseOptionService
    {
        Task<IResult<Paged<QuestionResponseOption>>> SearchPaged(QuestionResponseOptionFilter filter, int skip, int take);
        Task<IResult<IEnumerable<QuestionResponseOption>>> Search(QuestionResponseOptionFilter filter);
        Task<IResult<int>> Insert(QuestionResponseOption obj);
        Task<IResult<bool>> Update(Domain.Model.QuestionResponseOption obj);
    }
}