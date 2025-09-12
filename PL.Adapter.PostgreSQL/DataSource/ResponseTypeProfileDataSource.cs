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
    public class ResponseTypeProfileDataSource : BaseDataSource, IResponseTypeProfileDataSource
    {
        public ResponseTypeProfileDataSource(IOptions<EnvironmentSettings> envOptions) : base(envOptions.Value.ConnectionString) { }

        #region SQL
        private const string _TableName = "response_type_profile";
        private const string _FieldId = "id";
        private const string _FieldName = "name";
        private const string _FieldCode = "code";
        private const string _FieldDescription = "description";

        private IEnumerable<string> _AllFields = new[]
        {
            $"\"{_TableName}\".\"{_FieldId}\"",
            $"\"{_TableName}\".\"{_FieldName}\"",
            $"\"{_TableName}\".\"{_FieldCode}\"",
            $"\"{_TableName}\".\"{_FieldDescription}\""
        };

        private const string SearchSql = $@"
            SELECT 
                [fields]
            FROM {_TableName}
            [where]";
        private const string InsertSql = $@"
            INSERT INTO 
                {_TableName}
                ({_FieldName},{_FieldCode},{_FieldDescription})
            VALUES
                (@{_FieldName},@{_FieldCode},@{_FieldDescription})";
        private const string UpdateSql = $@"
            UPDATE 
                {_TableName}
            SET
                [fieldsandvalues]
            [where]";

        #endregion

        private Domain.Model.ResponseTypeProfile Convert(Model.ResponseTypeProfile obj)
        {
            return new Domain.Model.ResponseTypeProfile
            {
                Id = obj.id,
                Name = obj.name,
                Code = obj.code,
                Description = obj.description
            };
        }
        private Model.ResponseTypeProfile Convert(Domain.Model.ResponseTypeProfile obj)
        {
            return new Model.ResponseTypeProfile
            {
                id = obj.Id,
                name = obj.Name,
                code = obj.Code,
                description = obj.Description
            };
        }

        public async Task<IResult<Paged<Domain.Model.ResponseTypeProfile>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.ResponseTypeProfile>(filters);
                var result = await Query<Model.ResponseTypeProfile>(SearchSql, _AllFields, parsedFilters, skip, take);
                var countResult = await Query<int>(SearchSql, new[] { "count(1)" }, parsedFilters);
                var returnData = result.Data.Select(Convert).ToArray();
                return DefaultResult<Paged<Domain.Model.ResponseTypeProfile>>.Create(new Paged<Domain.Model.ResponseTypeProfile>(returnData, skip, take, countResult.Data.Single()));
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.ResponseTypeProfile>>.Error("Ocorreu um erro ao procurar o(s) perfil(is) de tipo de resposta.", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.ResponseTypeProfile>>> Search(IEnumerable<Filter> filters)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.ResponseTypeProfile>(filters);
                var result = await Query<Model.ResponseTypeProfile>(SearchSql, _AllFields, parsedFilters);
                if (!result.Succeded)
                    return result.ChangeType<IEnumerable<Domain.Model.ResponseTypeProfile>>(default).AddMessage("Ocorreu um erro ao procurar os dados do(s) perfil(is) de tipo de resposta.");
                if (!result.Data.Any())
                    return result.ChangeType<IEnumerable<Domain.Model.ResponseTypeProfile>>(default).AddMessage("Não há dados de perfil de tipo de resposta para serem exibidos.");
                var returnData = result.Data.Select(Convert).ToArray();
                return result.ChangeType<IEnumerable<Domain.Model.ResponseTypeProfile>>(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.ResponseTypeProfile>>.Error("Ocorreu um erro ao procurar os dados do(s) perfil(is) de tipo de resposta.", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.ResponseTypeProfile obj)
        {
            try
            {
                var result = await InsertAsync(InsertSql, Convert(obj));
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao cadastrar o perfil de tipo de resposta.");
                obj.Id = result.Data;
                var returnData = result.Data;
                return DefaultResult<int>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Ocorreu um erro ao cadastrar o perfil de tipo de resposta.", ex);
            }
        }

        public async Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.ResponseTypeProfile oldObj, Domain.Model.ResponseTypeProfile newObj)
        {
            try
            {
                if (filters == null || !filters.Any(x => x._Fields.ElementAt(0) == "Id" && x._Values.Any(y => int.Parse(y.ToString()) > 0)))
                    return DefaultResult<bool>.Break("ID do perfil de tipo de resposta não informado.", null);
                var updateList = new Dictionary<string, object>();
                if (oldObj.Name != newObj.Name)
                    updateList.Add(_FieldName, newObj.Name);
                if (oldObj.Code != newObj.Code)
                    updateList.Add(_FieldCode, newObj.Code);
                if (oldObj.Description != newObj.Description)
                    updateList.Add(_FieldDescription, newObj.Description);

                var parsedFilters = FilterParser.Parse<Model.ResponseTypeProfile>(filters);
                var result = await UpdateAsync(UpdateSql, updateList, parsedFilters);
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao editar o perfil de tipo de resposta.");
                var returnData = result.Data;
                return DefaultResult<bool>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Ocorreu um erro na edição do perfil de tipo de resposta.", ex);
            }
        }
    }
}