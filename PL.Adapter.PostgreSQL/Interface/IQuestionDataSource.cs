using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util.Model.Paged;

namespace PL.Adapter.PostgreSQL.Interface
{
    public interface IQuestionDataSource
    {
        Task<IResult<Paged<Domain.Model.Question>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take);
        Task<IResult<IEnumerable<Domain.Model.Question>>> Search(IEnumerable<Filter> filters);
        Task<IResult<int>> Insert(Domain.Model.Question obj);
        Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.Question oldObj, Domain.Model.Question newObj);
    }
}