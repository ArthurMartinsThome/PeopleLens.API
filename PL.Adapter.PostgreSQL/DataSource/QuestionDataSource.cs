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
    public class QuestionDataSource : BaseDataSource, IQuestionDataSource
    {
        public QuestionDataSource(IOptions<EnvironmentSettings> envOptions) : base(envOptions.Value.ConnectionString) { }

        #region SQL
        private const string _TableName = "question";
        private const string _FieldId = "id";
        private const string _FieldResponseTypeId = "response_type_id";
        private const string _FieldQuestionText = "question_text";
        private const string _FieldCreatedAt = "created_at";
        private const string _FieldUpdatedAt = "updated_at";

        private IEnumerable<string> _AllFields = new[]
        {
            $"\"{_TableName}\".\"{_FieldId}\"",
            $"\"{_TableName}\".\"{_FieldResponseTypeId}\"",
            $"\"{_TableName}\".\"{_FieldQuestionText}\"",
            $"\"{_TableName}\".\"{_FieldCreatedAt}\"",
            $"\"{_TableName}\".\"{_FieldUpdatedAt}\""
        };

        private const string SearchSql = $@"
            SELECT 
                [fields]
            FROM ""{_TableName}""
            [where]";
        private const string InsertSql = $@"
            INSERT INTO 
                ""{_TableName}""
                ({_FieldResponseTypeId},{_FieldQuestionText},{_FieldCreatedAt},{_FieldUpdatedAt})
            VALUES
                (@{_FieldResponseTypeId},@{_FieldQuestionText},@{_FieldCreatedAt},@{_FieldUpdatedAt})";
        private const string UpdateSql = $@"
            UPDATE 
                {_TableName}
            SET
                [fieldsandvalues]
            [where]";
        private const string DeleteSql = $@"
            DELETE FROM {_TableName} [where]";

        #endregion

        private Domain.Model.Question Convert(Model.Question obj)
        {
            return new Domain.Model.Question
            {
                Id = obj.id,
                ResponseTypeId = obj.response_type_id,
                QuestionText = obj.question_text,
                CreatedAt = obj.created_at,
                UpdatedAt = obj.updated_at
            };
        }
        private Model.Question Convert(Domain.Model.Question obj)
        {
            return new Model.Question
            {
                id = obj.Id,
                response_type_id = obj.ResponseTypeId,
                question_text = obj.QuestionText,
                created_at = obj.CreatedAt,
                updated_at = obj.UpdatedAt
            };
        }

        public async Task<IResult<Paged<Domain.Model.Question>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.Question>(filters);
                var result = await Query<Model.Question>(SearchSql, _AllFields, parsedFilters, skip, take);
                var countResult = await Query<int>(SearchSql, new[] { "count(1)" }, parsedFilters);
                var returnData = result.Data.Select(Convert).ToArray();
                return DefaultResult<Paged<Domain.Model.Question>>.Create(new Paged<Domain.Model.Question>(returnData, skip, take, countResult.Data.Single()));
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.Question>>.Error("Ocorreu um erro ao procurar a(s) pergunta(s).", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.Question>>> Search(IEnumerable<Filter> filters)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.Question>(filters);
                var result = await Query<Model.Question>(SearchSql, _AllFields, parsedFilters);
                if (!result.Succeded)
                    return result.ChangeType<IEnumerable<Domain.Model.Question>>(default).AddMessage("Ocorreu um erro ao procurar os dados de pergunta(s).");
                if (!result.Data.Any())
                    return result.ChangeType<IEnumerable<Domain.Model.Question>>(default).AddMessage("Não há dados de pergunta(s) para serem exibidos.");
                var returnData = result.Data.Select(Convert).ToArray();
                return result.ChangeType<IEnumerable<Domain.Model.Question>>(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.Question>>.Error("Ocorreu um erro ao procurar os dados de pergunta(s).", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.Question obj)
        {
            try
            {
                var result = await InsertAsync(InsertSql, Convert(obj));
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao cadastrar a pergunta.");
                obj.Id = result.Data;
                var returnData = result.Data;
                return DefaultResult<int>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Ocorreu um erro ao cadastrar a pergunta.", ex);
            }
        }

        public async Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.Question oldObj, Domain.Model.Question newObj)
        {
            try
            {
                if (filters == null || !filters.Any(x => x._Fields.ElementAt(0) == "Id" && x._Values.Any(y => int.Parse(y.ToString()) > 0)))
                    return DefaultResult<bool>.Break("ID da pergunta não informado.", null);
                var updateList = new Dictionary<string, object>();
                if (oldObj.ResponseTypeId != newObj.ResponseTypeId)
                    updateList.Add(_FieldResponseTypeId, newObj.ResponseTypeId);
                if (oldObj.QuestionText != newObj.QuestionText)
                    updateList.Add(_FieldQuestionText, newObj.QuestionText);

                updateList.Add(_FieldUpdatedAt, DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                var parsedFilters = FilterParser.Parse<Model.Question>(filters);
                var result = await UpdateAsync(UpdateSql, updateList, parsedFilters);
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao editar a pergunta.");
                var returnData = result.Data;
                return DefaultResult<bool>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Ocorreu um erro na edição da pergunta.", ex);
            }
        }
    }
}