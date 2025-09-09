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
    public class ResponseTypeDataSource : BaseDataSource, IResponseTypeDataSource
    {
        public ResponseTypeDataSource(IOptions<EnvironmentSettings> envOptions) : base(envOptions.Value.ConnectionString) { }

        #region SQL
        private const string _TableName = "response_type";
        private const string _FieldId = "id";
        private const string _FieldName = "name";
        private const string _FieldDescription = "description";

        private IEnumerable<string> _AllFields = new[]
        {
            $"\"{_TableName}\".\"{_FieldId}\"",
            $"\"{_TableName}\".\"{_FieldName}\"",
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
                ({_FieldName},{_FieldDescription})
            VALUES
                (@{_FieldName},@{_FieldDescription})";
        private const string UpdateSql = $@"
            UPDATE 
                {_TableName}
            SET
                [fieldsandvalues]
            [where]";
        private const string DeleteSql = $@"
            DELETE FROM {_TableName} [where]";

        #endregion

        private Domain.Model.ResponseType Convert(Model.ResponseType obj)
        {
            return new Domain.Model.ResponseType
            {
                Id = obj.id,
                Name = obj.name,
                Description = obj.description
            };
        }
        private Model.ResponseType Convert(Domain.Model.ResponseType obj)
        {
            return new Model.ResponseType
            {
                id = obj.Id,
                name = obj.Name,
                description = obj.Description
            };
        }

        public async Task<IResult<Paged<Domain.Model.ResponseType>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.ResponseType>(filters);
                var result = await Query<Model.ResponseType>(SearchSql, _AllFields, parsedFilters, skip, take);
                var countResult = await Query<int>(SearchSql, new[] { "count(1)" }, parsedFilters);
                var returnData = result.Data.Select(Convert).ToArray();
                return DefaultResult<Paged<Domain.Model.ResponseType>>.Create(new Paged<Domain.Model.ResponseType>(returnData, skip, take, countResult.Data.Single()));
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.ResponseType>>.Error("Ocorreu um erro ao procurar o(s) tipo(s) de resposta.", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.ResponseType>>> Search(IEnumerable<Filter> filters)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.ResponseType>(filters);
                var result = await Query<Model.ResponseType>(SearchSql, _AllFields, parsedFilters);
                if (!result.Succeded)
                    return result.ChangeType<IEnumerable<Domain.Model.ResponseType>>(default).AddMessage("Ocorreu um erro ao procurar os dados de tipo(s) de resposta.");
                if (!result.Data.Any())
                    return result.ChangeType<IEnumerable<Domain.Model.ResponseType>>(default).AddMessage("Não há dados de tipo(s) de resposta para serem exibidos.");
                var returnData = result.Data.Select(Convert).ToArray();
                return result.ChangeType<IEnumerable<Domain.Model.ResponseType>>(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.ResponseType>>.Error("Ocorreu um erro ao procurar os dados de tipo(s) de resposta.", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.ResponseType obj)
        {
            try
            {
                var result = await InsertAsync(InsertSql, Convert(obj));
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao cadastrar o tipo de resposta.");
                obj.Id = result.Data;
                var returnData = result.Data;
                return DefaultResult<int>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Ocorreu um erro ao cadastrar o tipo de resposta.", ex);
            }
        }

        public async Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.ResponseType oldObj, Domain.Model.ResponseType newObj)
        {
            try
            {
                if (filters == null || !filters.Any(x => x._Fields.ElementAt(0) == "Id" && x._Values.Any(y => int.Parse(y.ToString()) > 0)))
                    return DefaultResult<bool>.Break("ID do tipo de resposta não informado.", null);
                var updateList = new Dictionary<string, object>();
                if (oldObj.Name != newObj.Name)
                    updateList.Add(_FieldName, newObj.Name);
                if (oldObj.Description != newObj.Description)
                    updateList.Add(_FieldDescription, newObj.Description);

                var parsedFilters = FilterParser.Parse<Model.ResponseType>(filters);
                var result = await UpdateAsync(UpdateSql, updateList, parsedFilters);
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao editar o tipo de resposta.");
                var returnData = result.Data;
                return DefaultResult<bool>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Ocorreu um erro na edição do tipo de resposta.", ex);
            }
        }
    }
}