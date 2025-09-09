using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util.Model.Paged;

namespace PL.Adapter.PostgreSQL.Interface
{
    public interface IQuestionResponseOptionDataSource
    {
        Task<IResult<Paged<Domain.Model.QuestionResponseOption>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take);
        Task<IResult<IEnumerable<Domain.Model.QuestionResponseOption>>> Search(IEnumerable<Filter> filters);
        Task<IResult<int>> Insert(Domain.Model.QuestionResponseOption obj);
        Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.QuestionResponseOption oldObj, Domain.Model.QuestionResponseOption newObj);
    }
}