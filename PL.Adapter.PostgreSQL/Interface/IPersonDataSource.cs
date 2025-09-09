using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util.Model.Paged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Adapter.PostgreSQL.Interface
{
    public interface IPersonDataSource
    {
        Task<IResult<Paged<Domain.Model.Person>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take);
        Task<IResult<IEnumerable<Domain.Model.Person>>> Search(IEnumerable<Filter> filters);
        Task<IResult<int>> Insert(Domain.Model.Person obj);
        Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.Person oldObj, Domain.Model.Person newObj);
    }
}