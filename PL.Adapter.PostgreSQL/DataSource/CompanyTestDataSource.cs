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
    public class CompanyTestDataSource : BaseDataSource, ICompanyTestDataSource
    {
        public CompanyTestDataSource(IOptions<EnvironmentSettings> envOptions) : base(envOptions.Value.ConnectionString) { }

        #region SQL
        private const string _TableName = $"company_test";
        private const string _FieldId = $"id";
        private const string _FieldTestId = $"test_id";
        private const string _FieldCompanyId = $"company_id";
        private const string _FieldCreatedAt = $"created_at";
        private const string _FieldUpdatedAt = $"updated_at";

        private IEnumerable<string> _AllFields = new[]
        {
            $"\"{_TableName}\".\"{_FieldId}\"",
            $"\"{_TableName}\".\"{_FieldTestId}\"",
            $"\"{_TableName}\".\"{_FieldCompanyId}\"",
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
                ({_FieldTestId},{_FieldCompanyId},{_FieldCreatedAt},{_FieldUpdatedAt})
            VALUES
                (@{_FieldTestId},@{_FieldCompanyId},@{_FieldCreatedAt},@{_FieldUpdatedAt})";
        private const string UpdateSql = $@"
            UPDATE 
                {_TableName}
            SET
                [fieldsandvalues]
            [where]";
        private const string DeleteSql = $@"
            DELETE FROM {_TableName} [where]";

        #endregion

        private Domain.Model.CompanyTest Convert(Model.CompanyTest obj)
        {
            return new Domain.Model.CompanyTest
            {
                Id = obj.id,
                TestId = obj.test_id,
                CompanyId = obj.company_id,
                CreatedAt = obj.created_at,
                UpdatedAt = obj.updated_at
            };
        }
        private Model.CompanyTest Convert(Domain.Model.CompanyTest obj)
        {
            return new Model.CompanyTest
            {
                id = obj.Id,
                test_id = obj.TestId,
                company_id = obj.CompanyId,
                created_at = obj.CreatedAt,
                updated_at = obj.UpdatedAt
            };
        }

        public async Task<IResult<Paged<Domain.Model.CompanyTest>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.CompanyTest>(filters);
                var result = await Query<Model.CompanyTest>(SearchSql, _AllFields, parsedFilters, skip, take);
                var countResult = await Query<int>(SearchSql, new[] { "count(1)" }, parsedFilters);
                var returnData = result.Data.Select(Convert).ToArray();
                return DefaultResult<Paged<Domain.Model.CompanyTest>>.Create(new Paged<Domain.Model.CompanyTest>(returnData, skip, take, countResult.Data.Single()));
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.CompanyTest>>.Error("Ocorreu um erro ao procurar o(s) teste(s) da(s) empresa(s).", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.CompanyTest>>> Search(IEnumerable<Filter> filters)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.CompanyTest>(filters);
                var result = await Query<Model.CompanyTest>(SearchSql, _AllFields, parsedFilters);
                if (!result.Succeded)
                    return result.ChangeType<IEnumerable<Domain.Model.CompanyTest>>(default).AddMessage("Ocorreu um erro ao procurar os dados de teste(s) da(s) empresa(s).");
                if (!result.Data.Any())
                    return result.ChangeType<IEnumerable<Domain.Model.CompanyTest>>(default).AddMessage("Não há dados de teste(s) da(s) empresa(s) para serem exibidos.");
                var returnData = result.Data.Select(Convert).ToArray();
                return result.ChangeType<IEnumerable<Domain.Model.CompanyTest>>(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.CompanyTest>>.Error("Ocorreu um erro ao procurar os dados de teste(s) da(s) empresa(s).", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.CompanyTest obj)
        {
            try
            {
                var result = await InsertAsync(InsertSql, Convert(obj));
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao cadastrar o teste da empresa.");
                obj.Id = result.Data;
                var returnData = result.Data;
                return DefaultResult<int>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Ocorreu um erro ao cadastrar o teste da empresa.", ex);
            }
        }

        public async Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.CompanyTest oldObj, Domain.Model.CompanyTest newObj)
        {
            try
            {
                if (filters == null || !filters.Any(x => x._Fields.ElementAt(0) == "Id" && x._Values.Any(y => int.Parse(y.ToString()) > 0)))
                    return DefaultResult<bool>.Break("ID do teste da empresa não informado.", null);
                var updateList = new Dictionary<string, object>();
                if (oldObj.TestId != newObj.TestId)
                    updateList.Add(_FieldTestId, newObj.TestId);
                if (oldObj.CompanyId != newObj.CompanyId)
                    updateList.Add(_FieldCompanyId, newObj.CompanyId);

                updateList.Add(_FieldUpdatedAt, DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                var parsedFilters = FilterParser.Parse<Model.CompanyTest>(filters);
                var result = await UpdateAsync(UpdateSql, updateList, parsedFilters);
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao editar o teste da empresa.");
                var returnData = result.Data;
                return DefaultResult<bool>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Ocorreu um erro na edição do teste da empresa.", ex);
            }
        }
    }
}
