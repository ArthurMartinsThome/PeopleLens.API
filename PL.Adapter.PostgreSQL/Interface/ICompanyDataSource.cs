using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util.Model.Paged;

namespace PL.Adapter.PostgreSQL.Interface
{
    public interface ICompanyDataSource
    {
        Task<IResult<Paged<Domain.Model.Company>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take);
        Task<IResult<IEnumerable<Domain.Model.Company>>> Search(IEnumerable<Filter> filters);
        Task<IResult<int>> Insert(Domain.Model.Company obj);
        Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.Company oldObj, Domain.Model.Company newObj);
    }
}