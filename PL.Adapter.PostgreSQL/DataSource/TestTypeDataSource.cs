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
    public class TestTypeDataSource : BaseDataSource, ITestTypeDataSource
    {
        public TestTypeDataSource(IOptions<EnvironmentSettings> envOptions) : base(envOptions.Value.ConnectionString) { }

        #region SQL
        private const string _TableName = "test_type";
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

        private Domain.Model.TestType Convert(Model.TestType obj)
        {
            return new Domain.Model.TestType
            {
                Id = obj.id,
                Name = obj.name,
                Description = obj.description
            };
        }
        private Model.TestType Convert(Domain.Model.TestType obj)
        {
            return new Model.TestType
            {
                id = obj.Id,
                name = obj.Name,
                description = obj.Description
            };
        }

        public async Task<IResult<Paged<Domain.Model.TestType>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.TestType>(filters);
                var result = await Query<Model.TestType>(SearchSql, _AllFields, parsedFilters, skip, take);
                var countResult = await Query<int>(SearchSql, new[] { "count(1)" }, parsedFilters);
                var returnData = result.Data.Select(Convert).ToArray();
                return DefaultResult<Paged<Domain.Model.TestType>>.Create(new Paged<Domain.Model.TestType>(returnData, skip, take, countResult.Data.Single()));
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.TestType>>.Error("Ocorreu um erro ao procurar o(s) tipo(s) de teste.", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.TestType>>> Search(IEnumerable<Filter> filters)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.TestType>(filters);
                var result = await Query<Model.TestType>(SearchSql, _AllFields, parsedFilters);
                if (!result.Succeded)
                    return result.ChangeType<IEnumerable<Domain.Model.TestType>>(default).AddMessage("Ocorreu um erro ao procurar os dados de tipo(s) de teste.");
                if (!result.Data.Any())
                    return result.ChangeType<IEnumerable<Domain.Model.TestType>>(default).AddMessage("Não há dados de tipo(s) de teste para serem exibidos.");
                var returnData = result.Data.Select(Convert).ToArray();
                return result.ChangeType<IEnumerable<Domain.Model.TestType>>(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.TestType>>.Error("Ocorreu um erro ao procurar os dados de tipo(s) de teste.", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.TestType obj)
        {
            try
            {
                var result = await InsertAsync(InsertSql, Convert(obj));
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao cadastrar o tipo de teste.");
                obj.Id = result.Data;
                var returnData = result.Data;
                return DefaultResult<int>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Ocorreu um erro ao cadastrar o tipo de teste.", ex);
            }
        }

        public async Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.TestType oldObj, Domain.Model.TestType newObj)
        {
            try
            {
                if (filters == null || !filters.Any(x => x._Fields.ElementAt(0) == "Id" && x._Values.Any(y => int.Parse(y.ToString()) > 0)))
                    return DefaultResult<bool>.Break("ID do tipo de teste não informado.", null);
                var updateList = new Dictionary<string, object>();
                if (oldObj.Name != newObj.Name)
                    updateList.Add(_FieldName, newObj.Name);
                if (oldObj.Description != newObj.Description)
                    updateList.Add(_FieldDescription, newObj.Description);

                var parsedFilters = FilterParser.Parse<Model.TestType>(filters);
                var result = await UpdateAsync(UpdateSql, updateList, parsedFilters);
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao editar o tipo de teste.");
                var returnData = result.Data;
                return DefaultResult<bool>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Ocorreu um erro na edição do tipo de teste.", ex);
            }
        }
    }
}