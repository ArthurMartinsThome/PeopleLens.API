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
    public class TestQuestionDataSource : BaseDataSource, ITestQuestionDataSource
    {
        public TestQuestionDataSource(IOptions<EnvironmentSettings> envOptions) : base(envOptions.Value.ConnectionString) { }

        #region SQL
        private const string _TableName = "test_question";
        private const string _FieldId = "id";
        private const string _FieldTestId = "test_id";
        private const string _FieldQuestionId = "question_id";

        private IEnumerable<string> _AllFields = new[]
        {
            $"\"{_TableName}\".\"{_FieldId}\"",
            $"\"{_TableName}\".\"{_FieldTestId}\"",
            $"\"{_TableName}\".\"{_FieldQuestionId}\""
        };

        private const string SearchSql = $@"
            SELECT 
                [fields]
            FROM {_TableName}
            [where]";
        private const string InsertSql = $@"
            INSERT INTO 
                {_TableName}
                ({_FieldTestId},{_FieldQuestionId})
            VALUES
                (@{_FieldTestId},@{_FieldQuestionId})";
        private const string UpdateSql = $@"
            UPDATE 
                {_TableName}
            SET
                [fieldsandvalues]
            [where]";
        private const string DeleteSql = $@"
            DELETE FROM {_TableName} [where]";

        #endregion

        private Domain.Model.TestQuestion Convert(Model.TestQuestion obj)
        {
            return new Domain.Model.TestQuestion
            {
                Id = obj.id,
                TestId = obj.test_id,
                QuestionId = obj.question_id
            };
        }
        private Model.TestQuestion Convert(Domain.Model.TestQuestion obj)
        {
            return new Model.TestQuestion
            {
                id = obj.Id,
                test_id = obj.TestId,
                question_id = obj.QuestionId
            };
        }

        public async Task<IResult<Paged<Domain.Model.TestQuestion>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.TestQuestion>(filters);
                var result = await Query<Model.TestQuestion>(SearchSql, _AllFields, parsedFilters, skip, take);
                var countResult = await Query<int>(SearchSql, new[] { "count(1)" }, parsedFilters);
                var returnData = result.Data.Select(Convert).ToArray();
                return DefaultResult<Paged<Domain.Model.TestQuestion>>.Create(new Paged<Domain.Model.TestQuestion>(returnData, skip, take, countResult.Data.Single()));
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.TestQuestion>>.Error("Ocorreu um erro ao procurar a(s) pergunta(s) do teste.", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.TestQuestion>>> Search(IEnumerable<Filter> filters)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.TestQuestion>(filters);
                var result = await Query<Model.TestQuestion>(SearchSql, _AllFields, parsedFilters);
                if (!result.Succeded)
                    return result.ChangeType<IEnumerable<Domain.Model.TestQuestion>>(default).AddMessage("Ocorreu um erro ao procurar os dados de pergunta(s) do teste.");
                if (!result.Data.Any())
                    return result.ChangeType<IEnumerable<Domain.Model.TestQuestion>>(default).AddMessage("Não há dados de pergunta(s) do teste para serem exibidos.");
                var returnData = result.Data.Select(Convert).ToArray();
                return result.ChangeType<IEnumerable<Domain.Model.TestQuestion>>(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.TestQuestion>>.Error("Ocorreu um erro ao procurar os dados de pergunta(s) do teste.", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.TestQuestion obj)
        {
            try
            {
                var result = await InsertAsync(InsertSql, Convert(obj));
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao cadastrar a pergunta do teste.");
                obj.Id = result.Data;
                var returnData = result.Data;
                return DefaultResult<int>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Ocorreu um erro ao cadastrar a pergunta do teste.", ex);
            }
        }

        public async Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.TestQuestion oldObj, Domain.Model.TestQuestion newObj)
        {
            try
            {
                if (filters == null || !filters.Any(x => x._Fields.ElementAt(0) == "Id" && x._Values.Any(y => int.Parse(y.ToString()) > 0)))
                    return DefaultResult<bool>.Break("ID da pergunta do teste não informado.", null);
                var updateList = new Dictionary<string, object>();
                if (oldObj.TestId != newObj.TestId)
                    updateList.Add(_FieldTestId, newObj.TestId);
                if (oldObj.QuestionId != newObj.QuestionId)
                    updateList.Add(_FieldQuestionId, newObj.QuestionId);

                var parsedFilters = FilterParser.Parse<Model.TestQuestion>(filters);
                var result = await UpdateAsync(UpdateSql, updateList, parsedFilters);
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao editar a pergunta do teste.");
                var returnData = result.Data;
                return DefaultResult<bool>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Ocorreu um erro na edição da pergunta do teste.", ex);
            }
        }
    }
}