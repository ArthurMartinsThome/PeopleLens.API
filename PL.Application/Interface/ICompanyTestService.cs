using PL.Domain.Model;
using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Util.Model.Paged;

namespace PL.Application.Interface
{
    public interface ICompanyTestService
    {
        Task<IResult<Paged<CompanyTest>>> SearchPaged(CompanyTestFilter filter, int skip, int take);
        Task<IResult<IEnumerable<CompanyTest>>> Search(CompanyTestFilter filter);
        Task<IResult<int>> Insert(CompanyTest obj);
        Task<IResult<bool>> Update(Domain.Model.CompanyTest obj);
    }
}