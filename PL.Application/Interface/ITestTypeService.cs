using PL.Domain.Model;
using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Util.Model.Paged;

namespace PL.Application.Interface
{
    public interface ITestTypeService
    {
        Task<IResult<Paged<TestType>>> SearchPaged(TestTypeFilter filter, int skip, int take);
        Task<IResult<IEnumerable<TestType>>> Search(TestTypeFilter filter);
        Task<IResult<int>> Insert(TestType obj);
        Task<IResult<bool>> Update(TestType obj);
    }
}