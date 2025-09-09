using PL.Domain.Model;
using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Util.Model.Paged;

namespace PL.Application.Interface
{
    public interface ICompanyService
    {
        Task<IResult<Paged<Company>>> SearchPaged(CompanyFilter filter, int skip, int take);
        Task<IResult<IEnumerable<Company>>> Search(CompanyFilter filter);
        Task<IResult<int>> Insert(Company obj);
        Task<IResult<bool>> Update(Company newObj);
    }
}