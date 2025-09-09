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
    public class ResponseOptionDataSource : BaseDataSource, IResponseOptionDataSource
    {
        public ResponseOptionDataSource(IOptions<EnvironmentSettings> envOptions) : base(envOptions.Value.ConnectionString) { }

        #region SQL
        private const string _TableName = "response_option";
        private const string _FieldId = "id";
        private const string _FieldResponseId = "response_id";
        private const string _FieldQuestionResponseOptionId = "question_response_option_id";

        private IEnumerable<string> _AllFields = new[]
        {
            $"\"{_TableName}\".\"{_FieldId}\"",
            $"\"{_TableName}\".\"{_FieldResponseId}\"",
            $"\"{_TableName}\".\"{_FieldQuestionResponseOptionId}\""
        };

        private const string SearchSql = $@"
            SELECT 
                [fields]
            FROM {_TableName}
            [where]";
        private const string InsertSql = $@"
            INSERT INTO 
                {_TableName}
                ({_FieldResponseId},{_FieldQuestionResponseOptionId})
            VALUES
                (@{_FieldResponseId},@{_FieldQuestionResponseOptionId})";
        private const string UpdateSql = $@"
            UPDATE 
                {_TableName}
            SET
                [fieldsandvalues]
            [where]";
        private const string DeleteSql = $@"
            DELETE FROM {_TableName} [where]";

        #endregion

        private Domain.Model.ResponseOption Convert(Model.ResponseOption obj)
        {
            return new Domain.Model.ResponseOption
            {
                Id = obj.id,
                ResponseId = obj.response_id,
                QuestionResponseOptionId = obj.question_response_option_id
            };
        }
        private Model.ResponseOption Convert(Domain.Model.ResponseOption obj)
        {
            return new Model.ResponseOption
            {
                id = obj.Id,
                response_id = obj.ResponseId,
                question_response_option_id = obj.QuestionResponseOptionId
            };
        }

        public async Task<IResult<Paged<Domain.Model.ResponseOption>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.ResponseOption>(filters);
                var result = await Query<Model.ResponseOption>(SearchSql, _AllFields, parsedFilters, skip, take);
                var countResult = await Query<int>(SearchSql, new[] { "count(1)" }, parsedFilters);
                var returnData = result.Data.Select(Convert).ToArray();
                return DefaultResult<Paged<Domain.Model.ResponseOption>>.Create(new Paged<Domain.Model.ResponseOption>(returnData, skip, take, countResult.Data.Single()));
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.ResponseOption>>.Error("Ocorreu um erro ao procurar a(s) opção(ões) de resposta.", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.ResponseOption>>> Search(IEnumerable<Filter> filters)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.ResponseOption>(filters);
                var result = await Query<Model.ResponseOption>(SearchSql, _AllFields, parsedFilters);
                if (!result.Succeded)
                    return result.ChangeType<IEnumerable<Domain.Model.ResponseOption>>(default).AddMessage("Ocorreu um erro ao procurar os dados de opção(ões) de resposta.");
                if (!result.Data.Any())
                    return result.ChangeType<IEnumerable<Domain.Model.ResponseOption>>(default).AddMessage("Não há dados de opção(ões) de resposta para serem exibidos.");
                var returnData = result.Data.Select(Convert).ToArray();
                return result.ChangeType<IEnumerable<Domain.Model.ResponseOption>>(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.ResponseOption>>.Error("Ocorreu um erro ao procurar os dados de opção(ões) de resposta.", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.ResponseOption obj)
        {
            try
            {
                var result = await InsertAsync(InsertSql, Convert(obj));
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao cadastrar a opção de resposta.");
                obj.Id = result.Data;
                var returnData = result.Data;
                return DefaultResult<int>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Ocorreu um erro ao cadastrar a opção de resposta.", ex);
            }
        }

        public async Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.ResponseOption oldObj, Domain.Model.ResponseOption newObj)
        {
            try
            {
                if (filters == null || !filters.Any(x => x._Fields.ElementAt(0) == "Id" && x._Values.Any(y => int.Parse(y.ToString()) > 0)))
                    return DefaultResult<bool>.Break("ID da opção de resposta não informado.", null);
                var updateList = new Dictionary<string, object>();
                if (oldObj.ResponseId != newObj.ResponseId)
                    updateList.Add(_FieldResponseId, newObj.ResponseId);
                if (oldObj.QuestionResponseOptionId != newObj.QuestionResponseOptionId)
                    updateList.Add(_FieldQuestionResponseOptionId, newObj.QuestionResponseOptionId);

                var parsedFilters = FilterParser.Parse<Model.ResponseOption>(filters);
                var result = await UpdateAsync(UpdateSql, updateList, parsedFilters);
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao editar a opção de resposta.");
                var returnData = result.Data;
                return DefaultResult<bool>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Ocorreu um erro na edição da opção de resposta.", ex);
            }
        }
    }
}