using PL.Domain.Model;
using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Util.Model.Paged;

namespace PL.Application.Interface
{
    public interface IPersonService
    {
        Task<IResult<Paged<Person>>> SearchPaged(PersonFilter filter, int skip, int take);
        Task<IResult<IEnumerable<Person>>> Search(PersonFilter filter);
        Task<IResult<int>> Insert(Person obj);
        Task<IResult<bool>> Update(Domain.Model.Person obj);
    }
}