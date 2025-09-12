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
    public class QuestionResponseOptionDataSource : BaseDataSource, IQuestionResponseOptionDataSource
    {
        public QuestionResponseOptionDataSource(IOptions<EnvironmentSettings> envOptions) : base(envOptions.Value.ConnectionString) { }

        #region SQL
        private const string _TableName = "question_response_option";
        private const string _FieldId = "id";
        private const string _FieldQuestionId = "question_id";
        private const string _FieldText = "text";
        private const string _FieldResponseTypeProfileId = "response_type_profile_id";
        private const string _FieldWeight = "weight";

        private IEnumerable<string> _AllFields = new[]
        {
            $"\"{_TableName}\".\"{_FieldId}\"",
            $"\"{_TableName}\".\"{_FieldQuestionId}\"",
            $"\"{_TableName}\".\"{_FieldText}\"",
            $"\"{_TableName}\".\"{_FieldResponseTypeProfileId}\"",
            $"\"{_TableName}\".\"{_FieldWeight}\""
        };

        private const string SearchSql = $@"
            SELECT 
                [fields]
            FROM {_TableName}
            [where]";
        private const string InsertSql = $@"
            INSERT INTO 
                {_TableName}
                ({_FieldQuestionId},{_FieldText},{_FieldResponseTypeProfileId},{_FieldWeight})
            VALUES
                (@{_FieldQuestionId},@{_FieldText},@{_FieldResponseTypeProfileId},@{_FieldWeight})";
        private const string UpdateSql = $@"
            UPDATE 
                {_TableName}
            SET
                [fieldsandvalues]
            [where]";

        #endregion

        private Domain.Model.QuestionResponseOption Convert(Model.QuestionResponseOption obj)
        {
            return new Domain.Model.QuestionResponseOption
            {
                Id = obj.id,
                QuestionId = obj.question_id,
                Text = obj.text,
                ResponseTypeProfileId = obj.response_type_profile_id,
                Weight = obj.weight
            };
        }
        private Model.QuestionResponseOption Convert(Domain.Model.QuestionResponseOption obj)
        {
            return new Model.QuestionResponseOption
            {
                id = obj.Id,
                question_id = obj.QuestionId,
                text = obj.Text,
                response_type_profile_id = obj.ResponseTypeProfileId,
                weight = obj.Weight
            };
        }

        public async Task<IResult<Paged<Domain.Model.QuestionResponseOption>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.QuestionResponseOption>(filters);
                var result = await Query<Model.QuestionResponseOption>(SearchSql, _AllFields, parsedFilters, skip, take);
                var countResult = await Query<int>(SearchSql, new[] { "count(1)" }, parsedFilters);
                var returnData = result.Data.Select(Convert).ToArray();
                return DefaultResult<Paged<Domain.Model.QuestionResponseOption>>.Create(new Paged<Domain.Model.QuestionResponseOption>(returnData, skip, take, countResult.Data.Single()));
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.QuestionResponseOption>>.Error("Ocorreu um erro ao procurar a(s) opção(ões) de resposta da(s) pergunta(s).", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.QuestionResponseOption>>> Search(IEnumerable<Filter> filters)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.QuestionResponseOption>(filters);
                var result = await Query<Model.QuestionResponseOption>(SearchSql, _AllFields, parsedFilters);
                if (!result.Succeded)
                    return result.ChangeType<IEnumerable<Domain.Model.QuestionResponseOption>>(default).AddMessage("Ocorreu um erro ao procurar os dados de opção(ões) de resposta da(s) pergunta(s).");
                if (!result.Data.Any())
                    return result.ChangeType<IEnumerable<Domain.Model.QuestionResponseOption>>(default).AddMessage("Não há dados de opção(ões) de resposta da(s) pergunta(s) para serem exibidos.");
                var returnData = result.Data.Select(Convert).ToArray();
                return result.ChangeType<IEnumerable<Domain.Model.QuestionResponseOption>>(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.QuestionResponseOption>>.Error("Ocorreu um erro ao procurar os dados de opção(ões) de resposta da(s) pergunta(s).", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.QuestionResponseOption obj)
        {
            try
            {
                var result = await InsertAsync(InsertSql, Convert(obj));
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao cadastrar a opção de resposta da pergunta.");
                obj.Id = result.Data;
                var returnData = result.Data;
                return DefaultResult<int>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Ocorreu um erro ao cadastrar a opção de resposta da pergunta.", ex);
            }
        }

        public async Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.QuestionResponseOption oldObj, Domain.Model.QuestionResponseOption newObj)
        {
            try
            {
                if (filters == null || !filters.Any(x => x._Fields.ElementAt(0) == "Id" && x._Values.Any(y => int.Parse(y.ToString()) > 0)))
                    return DefaultResult<bool>.Break("ID da opção de resposta da pergunta não informado.", null);
                var updateList = new Dictionary<string, object>();
                if (oldObj.QuestionId != newObj.QuestionId)
                    updateList.Add(_FieldQuestionId, newObj.QuestionId);
                if (oldObj.Text != newObj.Text)
                    updateList.Add(_FieldText, newObj.Text);
                if (oldObj.ResponseTypeProfileId != newObj.ResponseTypeProfileId)
                    updateList.Add(_FieldResponseTypeProfileId, newObj.ResponseTypeProfileId);
                if (oldObj.Weight != newObj.Weight)
                    updateList.Add(_FieldWeight, newObj.Weight);

                var parsedFilters = FilterParser.Parse<Model.QuestionResponseOption>(filters);
                var result = await UpdateAsync(UpdateSql, updateList, parsedFilters);
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao editar a opção de resposta da pergunta.");
                var returnData = result.Data;
                return DefaultResult<bool>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Ocorreu um erro na edição da opção de resposta da pergunta.", ex);
            }
        }
    }
}