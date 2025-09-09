using PL.Domain.Model;
using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Util.Model.Paged;

namespace PL.Application.Interface
{
    public interface IKeyConfigurationQuestionService
    {
        Task<IResult<Paged<KeyConfigurationQuestion>>> SearchPaged(KeyConfigurationQuestionFilter filter, int skip, int take);
        Task<IResult<IEnumerable<KeyConfigurationQuestion>>> Search(KeyConfigurationQuestionFilter filter);
        Task<IResult<int>> Insert(KeyConfigurationQuestion obj);
        Task<IResult<bool>> Update(KeyConfigurationQuestion obj);
    }
}