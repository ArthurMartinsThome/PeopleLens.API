using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util.Model.Paged;

namespace PL.Adapter.PostgreSQL.Interface
{
    public interface IResponseOptionDataSource
    {
        Task<IResult<Paged<Domain.Model.ResponseOption>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take);
        Task<IResult<IEnumerable<Domain.Model.ResponseOption>>> Search(IEnumerable<Filter> filters);
        Task<IResult<int>> Insert(Domain.Model.ResponseOption obj);
        Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.ResponseOption oldObj, Domain.Model.ResponseOption newObj);
    }
}