using PL.Domain.Model;
using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Util.Model.Paged;

namespace PL.Application.Interface
{
    public interface IQuestionConfigurationService
    {
        Task<IResult<Paged<QuestionConfiguration>>> SearchPaged(QuestionConfigurationFilter filter, int skip, int take);
        Task<IResult<IEnumerable<QuestionConfiguration>>> Search(QuestionConfigurationFilter filter);
        Task<IResult<int>> Insert(QuestionConfiguration obj);
        Task<IResult<bool>> Update(Domain.Model.QuestionConfiguration obj);
    }
}