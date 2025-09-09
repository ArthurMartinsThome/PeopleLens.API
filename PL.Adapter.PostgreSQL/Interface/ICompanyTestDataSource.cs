using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util.Model.Paged;

namespace PL.Adapter.PostgreSQL.Interface
{
    public interface ICompanyTestDataSource
    {
        Task<IResult<Paged<Domain.Model.CompanyTest>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take);
        Task<IResult<IEnumerable<Domain.Model.CompanyTest>>> Search(IEnumerable<Filter> filters);
        Task<IResult<int>> Insert(Domain.Model.CompanyTest obj);
        Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.CompanyTest oldObj, Domain.Model.CompanyTest newObj);
    }
}