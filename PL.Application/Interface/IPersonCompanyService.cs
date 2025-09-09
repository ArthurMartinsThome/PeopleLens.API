using PL.Domain.Model;
using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Util.Model.Paged;

namespace PL.Application.Interface
{
    public interface IPersonCompanyService
    {
        Task<IResult<Paged<PersonCompany>>> SearchPaged(PersonCompanyFilter filter, int skip, int take);
        Task<IResult<IEnumerable<PersonCompany>>> Search(PersonCompanyFilter filter);
        Task<IResult<int>> Insert(PersonCompany obj);
        Task<IResult<bool>> Update(Domain.Model.PersonCompany obj);
    }
}