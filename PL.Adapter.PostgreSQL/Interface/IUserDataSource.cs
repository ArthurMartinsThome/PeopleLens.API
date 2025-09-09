using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util.Model.Paged;

namespace PL.Adapter.PostgreSQL.Interface
{
    public interface IUserDataSource
    {
        Task<IResult<Paged<Domain.Model.User>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take);
        Task<IResult<IEnumerable<Domain.Model.User>>> Search(IEnumerable<Filter> filters);
        Task<IResult<int>> Insert(Domain.Model.User obj);
        Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.User oldObj, Domain.Model.User newObj);
    }
}