using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util.Model.Paged;

namespace PL.Adapter.PostgreSQL.Interface
{
    public interface IQuestionConfigurationDataSource
    {
        Task<IResult<Paged<Domain.Model.QuestionConfiguration>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take);
        Task<IResult<IEnumerable<Domain.Model.QuestionConfiguration>>> Search(IEnumerable<Filter> filters);
        Task<IResult<int>> Insert(Domain.Model.QuestionConfiguration obj);
        Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.QuestionConfiguration oldObj, Domain.Model.QuestionConfiguration newObj);
    }
}