using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;

namespace PL.Adapter.MySql.Interface
{
    public interface IAuthDataSource
    {
        Task<IResult<IEnumerable<Domain.Model.Auth>>> Search(IEnumerable<Filter> filters);
        Task<IResult<int>> Insert(Domain.Model.Auth obj);
        Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.Auth oldObj, Domain.Model.Auth newObj);
    }
}