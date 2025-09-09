using PL.Adapter.PostgreSQL.Interface;
using PL.Application.Interface;
using PL.Domain.Model.Enum;
using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util.Model.Paged;

namespace PL.Application.Service
{
    public class KeyConfigurationQuestionService : IKeyConfigurationQuestionService
    {
        private readonly IKeyConfigurationQuestionDataSource _dataSource;

        public KeyConfigurationQuestionService(IKeyConfigurationQuestionDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        private IEnumerable<Filter> GetFilters(KeyConfigurationQuestionFilter filter)
        {
            var filterList = new List<Filter>();
            if (filter == null)
                return filterList;
            if (filter.Id.HasValue && filter.Id > 0)
                filterList.Add(Filter.Create("Id", EOperator.Equal, new[] { filter.Id.Value }));
            if (filter.QuestionId.HasValue && filter.QuestionId > 0)
                filterList.Add(Filter.Create("QuestionId", EOperator.Equal, filter.QuestionId.Value));
            if (filter.KeyConfigId.HasValue && filter.KeyConfigId > 0)
                filterList.Add(Filter.Create("KeyConfigId", EOperator.Equal, filter.KeyConfigId.Value));
            if (!string.IsNullOrEmpty(filter.Value))
                filterList.Add(Filter.Create("Value", EOperator.Equal, filter.Value));

            return filterList;
        }

        public async Task<IResult<Paged<Domain.Model.KeyConfigurationQuestion>>> SearchPaged(KeyConfigurationQuestionFilter filter, int skip, int take)
        {
            try
            {
                var result = await _dataSource.SearchPaged(GetFilters(filter), skip, take);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.KeyConfigurationQuestion>>.Error("Houve uma falha ao buscar os dados da(s) configuração(ões) de chave de questão.", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.KeyConfigurationQuestion>>> Search(KeyConfigurationQuestionFilter filter)
        {
            try
            {
                var filters = GetFilters(filter);
                var result = await _dataSource.Search(filters);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.KeyConfigurationQuestion>>.Error("Houve uma falha ao buscar os dados da configuração de chave de questão.", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.KeyConfigurationQuestion obj)
        {
            try
            {
                var result = await _dataSource.Insert(obj);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Houve uma falha ao cadastrar a configuração de chave de questão.", ex);
            }
        }

        public async Task<IResult<bool>> Update(Domain.Model.KeyConfigurationQuestion obj)
        {
            try
            {
                if (!obj.Id.HasValue || obj.Id <= 0)
                    return DefaultResult<bool>.Error("Falta referenciar qual o id para editar a configuração de chave de questão.", new Exception($"Update: newObj.Id == (null/0)"));

                var searchResult = await Search(new KeyConfigurationQuestionFilter { Id = obj.Id });
                if (!searchResult.Succeded || searchResult.Data == null)
                    return DefaultResult<bool>.Break("Configuração de chave de questão não encontrada para atualização.");

                var oldObj = searchResult.Data.First();
                var newObj = (Domain.Model.KeyConfigurationQuestion)searchResult.Data.First().Clone();

                if (!string.IsNullOrEmpty(obj.KeyName))
                    newObj.KeyName = obj.KeyName;
                if (!string.IsNullOrEmpty(obj.Description))
                    newObj.Description = obj.Description;

                var filters = GetFilters(new KeyConfigurationQuestionFilter() { Id = obj.Id });
                return await _dataSource.Update(filters, oldObj, newObj);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Houve uma falha ao editar a configuração de chave de questão.", ex);
            }
        }
    }
}