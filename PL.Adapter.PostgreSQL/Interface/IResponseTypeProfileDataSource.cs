using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util.Model.Paged;

namespace PL.Adapter.PostgreSQL.Interface
{
    public interface IResponseTypeProfileDataSource
    {
        Task<IResult<Paged<Domain.Model.ResponseTypeProfile>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take);
        Task<IResult<IEnumerable<Domain.Model.ResponseTypeProfile>>> Search(IEnumerable<Filter> filters);
        Task<IResult<int>> Insert(Domain.Model.ResponseTypeProfile obj);
        Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.ResponseTypeProfile oldObj, Domain.Model.ResponseTypeProfile newObj);
    }
}