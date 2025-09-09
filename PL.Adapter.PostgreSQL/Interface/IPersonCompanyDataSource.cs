using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util.Model.Paged;

namespace PL.Adapter.PostgreSQL.Interface
{
    public interface IPersonCompanyDataSource
    {
        Task<IResult<Paged<Domain.Model.PersonCompany>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take);
        Task<IResult<IEnumerable<Domain.Model.PersonCompany>>> Search(IEnumerable<Filter> filters);
        Task<IResult<int>> Insert(Domain.Model.PersonCompany obj);
        Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.PersonCompany oldObj, Domain.Model.PersonCompany newObj);
    }
}