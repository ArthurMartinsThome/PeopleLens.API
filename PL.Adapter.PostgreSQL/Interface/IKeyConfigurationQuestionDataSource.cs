using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util.Model.Paged;

namespace PL.Adapter.PostgreSQL.Interface
{
    public interface IKeyConfigurationQuestionDataSource
    {
        Task<IResult<Paged<Domain.Model.KeyConfigurationQuestion>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take);
        Task<IResult<IEnumerable<Domain.Model.KeyConfigurationQuestion>>> Search(IEnumerable<Filter> filters);
        Task<IResult<int>> Insert(Domain.Model.KeyConfigurationQuestion obj);
        Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.KeyConfigurationQuestion oldObj, Domain.Model.KeyConfigurationQuestion newObj);
    }
}