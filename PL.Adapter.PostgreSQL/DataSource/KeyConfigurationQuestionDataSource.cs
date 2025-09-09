using Microsoft.Extensions.Options;
using PL.Adapter.PostgreSQL.Common;
using PL.Adapter.PostgreSQL.Interface;
using PL.Infra.DefaultResult;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util;
using PL.Infra.Util.Model;
using PL.Infra.Util.Model.Paged;

namespace PL.Adapter.PostgreSQL.DataSource
{
    public class KeyConfigurationQuestionDataSource : BaseDataSource, IKeyConfigurationQuestionDataSource
    {
        public KeyConfigurationQuestionDataSource(IOptions<EnvironmentSettings> envOptions) : base(envOptions.Value.ConnectionString) { }

        #region SQL
        private const string _TableName = $"key_configuration_question";
        private const string _FieldId = $"id";
        private const string _FieldKeyName = $"key_name";
        private const string _FieldDescription = $"description";
        private const string _FieldCreatedAt = $"created_at";
        private const string _FieldUpdatedAt = $"updated_at";

        private IEnumerable<string> _AllFields = new[]
        {
            $"\"{_TableName}\".\"{_FieldId}\"",
            $"\"{_TableName}\".\"{_FieldKeyName}\"",
            $"\"{_TableName}\".\"{_FieldDescription}\"",
            $"\"{_TableName}\".\"{_FieldCreatedAt}\"",
            $"\"{_TableName}\".\"{_FieldUpdatedAt}\""
        };

        private const string SearchSql = $@"
            SELECT 
                [fields]
            FROM {_TableName}
            [where]";
        private const string InsertSql = $@"
            INSERT INTO 
                {_TableName}
                ({_FieldKeyName},{_FieldDescription},{_FieldCreatedAt},{_FieldUpdatedAt})
            VALUES
                (@{_FieldKeyName},@{_FieldDescription},@{_FieldCreatedAt},@{_FieldUpdatedAt})";
        private const string UpdateSql = $@"
            UPDATE 
                {_TableName}
            SET
                [fieldsandvalues]
            [where]";
        private const string DeleteSql = $@"
            DELETE FROM {_TableName} [where]";

        #endregion

        private Domain.Model.KeyConfigurationQuestion Convert(Model.KeyConfigurationQuestion obj)
        {
            return new Domain.Model.KeyConfigurationQuestion
            {
                Id = obj.id,
                KeyName = obj.key_name,
                Description = obj.description,
                CreatedAt = obj.created_at,
                UpdatedAt = obj.updated_at
            };
        }
        private Model.KeyConfigurationQuestion Convert(Domain.Model.KeyConfigurationQuestion obj)
        {
            return new Model.KeyConfigurationQuestion
            {
                id = obj.Id,
                key_name = obj.KeyName,
                description = obj.Description,
                created_at = obj.CreatedAt,
                updated_at = obj.UpdatedAt
            };
        }

        public async Task<IResult<Paged<Domain.Model.KeyConfigurationQuestion>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.KeyConfigurationQuestion>(filters);
                var result = await Query<Model.KeyConfigurationQuestion>(SearchSql, _AllFields, parsedFilters, skip, take);
                var countResult = await Query<int>(SearchSql, new[] { "count(1)" }, parsedFilters);
                var returnData = result.Data.Select(Convert).ToArray();
                return DefaultResult<Paged<Domain.Model.KeyConfigurationQuestion>>.Create(new Paged<Domain.Model.KeyConfigurationQuestion>(returnData, skip, take, countResult.Data.Single()));
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.KeyConfigurationQuestion>>.Error("Ocorreu um erro ao procurar a(s) chave(s) de configuração de pergunta(s).", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.KeyConfigurationQuestion>>> Search(IEnumerable<Filter> filters)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.KeyConfigurationQuestion>(filters);
                var result = await Query<Model.KeyConfigurationQuestion>(SearchSql, _AllFields, parsedFilters);
                if (!result.Succeded)
                    return result.ChangeType<IEnumerable<Domain.Model.KeyConfigurationQuestion>>(default).AddMessage("Ocorreu um erro ao procurar os dados de chave(s) de configuração de pergunta(s).");
                if (!result.Data.Any())
                    return result.ChangeType<IEnumerable<Domain.Model.KeyConfigurationQuestion>>(default).AddMessage("Não há dados de chave(s) de configuração de pergunta(s) para serem exibidos.");
                var returnData = result.Data.Select(Convert).ToArray();
                return result.ChangeType<IEnumerable<Domain.Model.KeyConfigurationQuestion>>(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.KeyConfigurationQuestion>>.Error("Ocorreu um erro ao procurar os dados de chave(s) de configuração de pergunta(s).", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.KeyConfigurationQuestion obj)
        {
            try
            {
                var result = await InsertAsync(InsertSql, Convert(obj));
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao cadastrar a chave de configuração de pergunta.");
                obj.Id = result.Data;
                var returnData = result.Data;
                return DefaultResult<int>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Ocorreu um erro ao cadastrar a chave de configuração de pergunta.", ex);
            }
        }

        public async Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.KeyConfigurationQuestion oldObj, Domain.Model.KeyConfigurationQuestion newObj)
        {
            try
            {
                if (filters == null || !filters.Any(x => x._Fields.ElementAt(0) == "Id" && x._Values.Any(y => int.Parse(y.ToString()) > 0)))
                    return DefaultResult<bool>.Break("ID da chave de configuração de pergunta não informado.", null);
                var updateList = new Dictionary<string, object>();
                if (oldObj.KeyName != newObj.KeyName)
                    updateList.Add(_FieldKeyName, newObj.KeyName);
                if (oldObj.Description != newObj.Description)
                    updateList.Add(_FieldDescription, newObj.Description);

                updateList.Add(_FieldUpdatedAt, DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                var parsedFilters = FilterParser.Parse<Model.KeyConfigurationQuestion>(filters);
                var result = await UpdateAsync(UpdateSql, updateList, parsedFilters);
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao editar a chave de configuração de pergunta.");
                var returnData = result.Data;
                return DefaultResult<bool>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Ocorreu um erro na edição da chave de configuração de pergunta.", ex);
            }
        }
    }
}