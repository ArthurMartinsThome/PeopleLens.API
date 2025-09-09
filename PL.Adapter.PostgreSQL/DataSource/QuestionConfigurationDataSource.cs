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
    public class QuestionConfigurationDataSource : BaseDataSource, IQuestionConfigurationDataSource
    {
        public QuestionConfigurationDataSource(IOptions<EnvironmentSettings> envOptions) : base(envOptions.Value.ConnectionString) { }

        #region SQL
        private const string _TableName = "question_configuration";
        private const string _FieldId = "id";
        private const string _FieldQuestionId = "question_id";
        private const string _FieldKeyConfigId = "key_config_id";
        private const string _FieldValue = "value";

        private IEnumerable<string> _AllFields = new[]
        {
            $"\"{_TableName}\".\"{_FieldId}\"",
            $"\"{_TableName}\".\"{_FieldQuestionId}\"",
            $"\"{_TableName}\".\"{_FieldKeyConfigId}\"",
            $"\"{_TableName}\".\"{_FieldValue}\""
        };

        private const string SearchSql = $@"
            SELECT 
                [fields]
            FROM {_TableName}
            [where]";
        private const string InsertSql = $@"
            INSERT INTO 
                {_TableName}
                ({_FieldQuestionId},{_FieldKeyConfigId},{_FieldValue})
            VALUES
                (@{_FieldQuestionId},@{_FieldKeyConfigId},@{_FieldValue})";
        private const string UpdateSql = $@"
            UPDATE 
                {_TableName}
            SET
                [fieldsandvalues]
            [where]";
        private const string DeleteSql = $@"
            DELETE FROM {_TableName} [where]";

        #endregion

        private Domain.Model.QuestionConfiguration Convert(Model.QuestionConfiguration obj)
        {
            return new Domain.Model.QuestionConfiguration
            {
                Id = obj.id,
                QuestionId = obj.question_id,
                KeyConfigId = obj.key_config_id,
                Value = obj.value
            };
        }
        private Model.QuestionConfiguration Convert(Domain.Model.QuestionConfiguration obj)
        {
            return new Model.QuestionConfiguration
            {
                id = obj.Id,
                question_id = obj.QuestionId,
                key_config_id = obj.KeyConfigId,
                value = obj.Value
            };
        }

        public async Task<IResult<Paged<Domain.Model.QuestionConfiguration>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.QuestionConfiguration>(filters);
                var result = await Query<Model.QuestionConfiguration>(SearchSql, _AllFields, parsedFilters, skip, take);
                var countResult = await Query<int>(SearchSql, new[] { "count(1)" }, parsedFilters);
                var returnData = result.Data.Select(Convert).ToArray();
                return DefaultResult<Paged<Domain.Model.QuestionConfiguration>>.Create(new Paged<Domain.Model.QuestionConfiguration>(returnData, skip, take, countResult.Data.Single()));
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.QuestionConfiguration>>.Error("Ocorreu um erro ao procurar a(s) configuração(ões) de pergunta(s).", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.QuestionConfiguration>>> Search(IEnumerable<Filter> filters)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.QuestionConfiguration>(filters);
                var result = await Query<Model.QuestionConfiguration>(SearchSql, _AllFields, parsedFilters);
                if (!result.Succeded)
                    return result.ChangeType<IEnumerable<Domain.Model.QuestionConfiguration>>(default).AddMessage("Ocorreu um erro ao procurar os dados de configuração de pergunta(s).");
                if (!result.Data.Any())
                    return result.ChangeType<IEnumerable<Domain.Model.QuestionConfiguration>>(default).AddMessage("Não há dados de configuração de pergunta(s) para serem exibidos.");
                var returnData = result.Data.Select(Convert).ToArray();
                return result.ChangeType<IEnumerable<Domain.Model.QuestionConfiguration>>(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.QuestionConfiguration>>.Error("Ocorreu um erro ao procurar os dados de configuração de pergunta(s).", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.QuestionConfiguration obj)
        {
            try
            {
                var result = await InsertAsync(InsertSql, Convert(obj));
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao cadastrar a configuração da pergunta.");
                obj.Id = result.Data;
                var returnData = result.Data;
                return DefaultResult<int>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Ocorreu um erro ao cadastrar a configuração da pergunta.", ex);
            }
        }

        public async Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.QuestionConfiguration oldObj, Domain.Model.QuestionConfiguration newObj)
        {
            try
            {
                if (filters == null || !filters.Any(x => x._Fields.ElementAt(0) == "Id" && x._Values.Any(y => int.Parse(y.ToString()) > 0)))
                    return DefaultResult<bool>.Break("ID da configuração da pergunta não informado.", null);
                var updateList = new Dictionary<string, object>();
                if (oldObj.QuestionId != newObj.QuestionId)
                    updateList.Add(_FieldQuestionId, newObj.QuestionId);
                if (oldObj.KeyConfigId != newObj.KeyConfigId)
                    updateList.Add(_FieldKeyConfigId, newObj.KeyConfigId);
                if (oldObj.Value != newObj.Value)
                    updateList.Add(_FieldValue, newObj.Value);

                var parsedFilters = FilterParser.Parse<Model.QuestionConfiguration>(filters);
                var result = await UpdateAsync(UpdateSql, updateList, parsedFilters);
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao editar a configuração da pergunta.");
                var returnData = result.Data;
                return DefaultResult<bool>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Ocorreu um erro na edição da configuração da pergunta.", ex);
            }
        }
    }
}