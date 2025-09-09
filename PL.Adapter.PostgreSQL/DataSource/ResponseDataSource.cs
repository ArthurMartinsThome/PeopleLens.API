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
    public class ResponseDataSource : BaseDataSource, IResponseDataSource
    {
        public ResponseDataSource(IOptions<EnvironmentSettings> envOptions) : base(envOptions.Value.ConnectionString) { }

        #region SQL
        private const string _TableName = "response";
        private const string _FieldId = "id";
        private const string _FieldTestAppliedId = "test_applied_id";
        private const string _FieldQuestionId = "question_id";
        private const string _FieldValue = "value";
        private const string _FieldCreatedAt = "created_at";
        private const string _FieldUpdatedAt = "updated_at";

        private IEnumerable<string> _AllFields = new[]
        {
            $"\"{_TableName}\".\"{_FieldId}\"",
            $"\"{_TableName}\".\"{_FieldTestAppliedId}\"",
            $"\"{_TableName}\".\"{_FieldQuestionId}\"",
            $"\"{_TableName}\".\"{_FieldValue}\"",
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
                ({_FieldTestAppliedId},{_FieldQuestionId},{_FieldValue},{_FieldCreatedAt},{_FieldUpdatedAt})
            VALUES
                (@{_FieldTestAppliedId},@{_FieldQuestionId},@{_FieldValue},@{_FieldCreatedAt},@{_FieldUpdatedAt})";
        private const string UpdateSql = $@"
            UPDATE 
                {_TableName}
            SET
                [fieldsandvalues]
            [where]";
        private const string DeleteSql = $@"
            DELETE FROM {_TableName} [where]";

        #endregion

        private Domain.Model.Response Convert(Model.Response obj)
        {
            return new Domain.Model.Response
            {
                Id = obj.id,
                TestAppliedId = obj.test_applied_id,
                QuestionId = obj.question_id,
                Value = obj.value,
                CreatedAt = obj.created_at,
                UpdatedAt = obj.updated_at
            };
        }
        private Model.Response Convert(Domain.Model.Response obj)
        {
            return new Model.Response
            {
                id = obj.Id,
                test_applied_id = obj.TestAppliedId,
                question_id = obj.QuestionId,
                value = obj.Value,
                created_at = obj.CreatedAt,
                updated_at = obj.UpdatedAt
            };
        }

        public async Task<IResult<Paged<Domain.Model.Response>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.Response>(filters);
                var result = await Query<Model.Response>(SearchSql, _AllFields, parsedFilters, skip, take);
                var countResult = await Query<int>(SearchSql, new[] { "count(1)" }, parsedFilters);
                var returnData = result.Data.Select(Convert).ToArray();
                return DefaultResult<Paged<Domain.Model.Response>>.Create(new Paged<Domain.Model.Response>(returnData, skip, take, countResult.Data.Single()));
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.Response>>.Error("Ocorreu um erro ao procurar a(s) resposta(s).", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.Response>>> Search(IEnumerable<Filter> filters)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.Response>(filters);
                var result = await Query<Model.Response>(SearchSql, _AllFields, parsedFilters);
                if (!result.Succeded)
                    return result.ChangeType<IEnumerable<Domain.Model.Response>>(default).AddMessage("Ocorreu um erro ao procurar os dados de resposta(s).");
                if (!result.Data.Any())
                    return result.ChangeType<IEnumerable<Domain.Model.Response>>(default).AddMessage("Não há dados de resposta(s) para serem exibidos.");
                var returnData = result.Data.Select(Convert).ToArray();
                return result.ChangeType<IEnumerable<Domain.Model.Response>>(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.Response>>.Error("Ocorreu um erro ao procurar os dados de resposta(s).", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.Response obj)
        {
            try
            {
                var result = await InsertAsync(InsertSql, Convert(obj));
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao cadastrar a resposta.");
                obj.Id = result.Data;
                var returnData = result.Data;
                return DefaultResult<int>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Ocorreu um erro ao cadastrar a resposta.", ex);
            }
        }

        public async Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.Response oldObj, Domain.Model.Response newObj)
        {
            try
            {
                if (filters == null || !filters.Any(x => x._Fields.ElementAt(0) == "Id" && x._Values.Any(y => int.Parse(y.ToString()) > 0)))
                    return DefaultResult<bool>.Break("ID da resposta não informado.", null);
                var updateList = new Dictionary<string, object>();
                if (oldObj.TestAppliedId != newObj.TestAppliedId)
                    updateList.Add(_FieldTestAppliedId, newObj.TestAppliedId);
                if (oldObj.QuestionId != newObj.QuestionId)
                    updateList.Add(_FieldQuestionId, newObj.QuestionId);
                if (oldObj.Value != newObj.Value)
                    updateList.Add(_FieldValue, newObj.Value);

                updateList.Add(_FieldUpdatedAt, DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                var parsedFilters = FilterParser.Parse<Model.Response>(filters);
                var result = await UpdateAsync(UpdateSql, updateList, parsedFilters);
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao editar a resposta.");
                var returnData = result.Data;
                return DefaultResult<bool>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Ocorreu um erro na edição da resposta.", ex);
            }
        }
    }
}