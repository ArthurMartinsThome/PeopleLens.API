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
    public class TestDataSource : BaseDataSource, ITestDataSource
    {
        public TestDataSource(IOptions<EnvironmentSettings> envOptions) : base(envOptions.Value.ConnectionString) { }

        #region SQL
        private const string _TableName = "test";
        private const string _FieldId = "id";
        private const string _FieldTestTypeId = "test_type_id";
        private const string _FieldTitle = "title";
        private const string _FieldDescription = "description";
        private const string _FieldCreatedAt = "created_at";
        private const string _FieldUpdatedAt = "updated_at";

        private IEnumerable<string> _AllFields = new[]
        {
            $"\"{_TableName}\".\"{_FieldId}\"",
            $"\"{_TableName}\".\"{_FieldTestTypeId}\"",
            $"\"{_TableName}\".\"{_FieldTitle}\"",
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
                ({_FieldTestTypeId},{_FieldTitle},{_FieldDescription},{_FieldCreatedAt},{_FieldUpdatedAt})
            VALUES
                (@{_FieldTestTypeId},@{_FieldTitle},@{_FieldDescription},NOW(),NOW())";
        private const string UpdateSql = $@"
            UPDATE 
                {_TableName}
            SET
                [fieldsandvalues], updated_at = NOW()
            [where]";
        private const string DeleteSql = $@"
            DELETE FROM {_TableName} [where]";

        #endregion

        private Domain.Model.Test Convert(Model.Test obj)
        {
            return new Domain.Model.Test
            {
                Id = obj.id,
                TestTypeId = obj.test_type_id,
                Title = obj.title,
                Description = obj.description,
                CreatedAt = obj.created_at,
                UpdatedAt = obj.updated_at
            };
        }
        private Model.Test Convert(Domain.Model.Test obj)
        {
            return new Model.Test
            {
                id = obj.Id,
                test_type_id = obj.TestTypeId,
                title = obj.Title,
                description = obj.Description
            };
        }

        public async Task<IResult<Paged<Domain.Model.Test>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.Test>(filters);
                var result = await Query<Model.Test>(SearchSql, _AllFields, parsedFilters, skip, take);
                var countResult = await Query<int>(SearchSql, new[] { "count(1)" }, parsedFilters);
                var returnData = result.Data.Select(Convert).ToArray();
                return DefaultResult<Paged<Domain.Model.Test>>.Create(new Paged<Domain.Model.Test>(returnData, skip, take, countResult.Data.Single()));
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.Test>>.Error("Ocorreu um erro ao procurar o(s) teste(s).", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.Test>>> Search(IEnumerable<Filter> filters)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.Test>(filters);
                var result = await Query<Model.Test>(SearchSql, _AllFields, parsedFilters);
                if (!result.Succeded)
                    return result.ChangeType<IEnumerable<Domain.Model.Test>>(default).AddMessage("Ocorreu um erro ao procurar os dados de teste(s).");
                if (!result.Data.Any())
                    return result.ChangeType<IEnumerable<Domain.Model.Test>>(default).AddMessage("Não há dados de teste(s) para serem exibidos.");
                var returnData = result.Data.Select(Convert).ToArray();
                return result.ChangeType<IEnumerable<Domain.Model.Test>>(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.Test>>.Error("Ocorreu um erro ao procurar os dados de teste(s).", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.Test obj)
        {
            try
            {
                var result = await InsertAsync(InsertSql, Convert(obj));
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao cadastrar o teste.");
                obj.Id = result.Data;
                var returnData = result.Data;
                return DefaultResult<int>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Ocorreu um erro ao cadastrar o teste.", ex);
            }
        }

        public async Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.Test oldObj, Domain.Model.Test newObj)
        {
            try
            {
                if (filters == null || !filters.Any(x => x._Fields.ElementAt(0) == "Id" && x._Values.Any(y => int.Parse(y.ToString()) > 0)))
                    return DefaultResult<bool>.Break("ID do teste não informado.", null);
                var updateList = new Dictionary<string, object>();
                if (oldObj.TestTypeId != newObj.TestTypeId)
                    updateList.Add(_FieldTestTypeId, newObj.TestTypeId);
                if (oldObj.Title != newObj.Title)
                    updateList.Add(_FieldTitle, newObj.Title);
                if (oldObj.Description != newObj.Description)
                    updateList.Add(_FieldDescription, newObj.Description);

                var parsedFilters = FilterParser.Parse<Model.Test>(filters);
                var result = await UpdateAsync(UpdateSql, updateList, parsedFilters);
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao editar o teste.");
                var returnData = result.Data;
                return DefaultResult<bool>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Ocorreu um erro na edição do teste.", ex);
            }
        }
    }
}