using Microsoft.Extensions.Options;
using PL.Adapter.PostgreSQL.Common;
using PL.Adapter.PostgreSQL.Interface;
using PL.Domain.Model.Enum;
using PL.Infra.DefaultResult;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util;
using PL.Infra.Util.Model;
using PL.Infra.Util.Model.Paged;

namespace PL.Adapter.PostgreSQL.DataSource
{
    public class TestAppliedDataSource : BaseDataSource, ITestAppliedDataSource
    {
        public TestAppliedDataSource(IOptions<EnvironmentSettings> envOptions) : base(envOptions.Value.ConnectionString) { }

        #region SQL
        private const string _TableName = "test_applied";
        private const string _FieldId = "id";
        private const string _FieldStatusId = "status_id";
        private const string _FieldCompanyTestId = "company_test_id";
        private const string _FieldPersonId = "person_id";
        private const string _FieldDateBegin = "date_begin";
        private const string _FieldDateEnd = "date_end";
        private const string _FieldCreatedAt = "created_at";
        private const string _FieldUpdatedAt = "updated_at";

        private IEnumerable<string> _AllFields = new[]
        {
            $"\"{_TableName}\".\"{_FieldId}\"",
            $"\"{_TableName}\".\"{_FieldStatusId}\"",
            $"\"{_TableName}\".\"{_FieldCompanyTestId}\"",
            $"\"{_TableName}\".\"{_FieldPersonId}\"",
            $"\"{_TableName}\".\"{_FieldDateBegin}\"",
            $"\"{_TableName}\".\"{_FieldDateEnd}\"",
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
                ({_FieldStatusId},{_FieldCompanyTestId},{_FieldPersonId},{_FieldDateBegin},{_FieldDateEnd},{_FieldCreatedAt},{_FieldUpdatedAt})
            VALUES
                (@{_FieldStatusId},@{_FieldCompanyTestId},@{_FieldPersonId},@{_FieldDateBegin},@{_FieldDateEnd},NOW(),NOW())";
        private const string UpdateSql = $@"
            UPDATE 
                {_TableName}
            SET
                [fieldsandvalues], updated_at = NOW()
            [where]";

        #endregion

        private Domain.Model.TestApplied Convert(Model.TestApplied obj)
        {
            return new Domain.Model.TestApplied
            {
                Id = obj.id,
                StatusId = (EStatus)obj.status_id,
                CompanyTestId = obj.company_test_id,
                PersonId = obj.person_id,
                DateBegin = obj.date_begin,
                DateEnd = obj.date_end,
                CreatedAt = obj.created_at,
                UpdatedAt = obj.updated_at
            };
        }
        private Model.TestApplied Convert(Domain.Model.TestApplied obj)
        {
            return new Model.TestApplied
            {
                id = obj.Id,
                status_id = obj.StatusId.GetHashCode(),
                company_test_id = obj.CompanyTestId,
                person_id = obj.PersonId,
                date_begin = obj.DateBegin,
                date_end = obj.DateEnd
            };
        }

        public async Task<IResult<Paged<Domain.Model.TestApplied>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.TestApplied>(filters);
                var result = await Query<Model.TestApplied>(SearchSql, _AllFields, parsedFilters, skip, take);
                var countResult = await Query<int>(SearchSql, new[] { "count(1)" }, parsedFilters);
                var returnData = result.Data.Select(Convert).ToArray();
                return DefaultResult<Paged<Domain.Model.TestApplied>>.Create(new Paged<Domain.Model.TestApplied>(returnData, skip, take, countResult.Data.Single()));
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.TestApplied>>.Error("Ocorreu um erro ao procurar o(s) teste(s) aplicado(s).", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.TestApplied>>> Search(IEnumerable<Filter> filters)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.TestApplied>(filters);
                var result = await Query<Model.TestApplied>(SearchSql, _AllFields, parsedFilters);
                if (!result.Succeded)
                    return result.ChangeType<IEnumerable<Domain.Model.TestApplied>>(default).AddMessage("Ocorreu um erro ao procurar os dados de teste(s) aplicado(s).");
                if (!result.Data.Any())
                    return result.ChangeType<IEnumerable<Domain.Model.TestApplied>>(default).AddMessage("Não há dados de teste(s) aplicado(s) para serem exibidos.");
                var returnData = result.Data.Select(Convert).ToArray();
                return result.ChangeType<IEnumerable<Domain.Model.TestApplied>>(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.TestApplied>>.Error("Ocorreu um erro ao procurar os dados de teste(s) aplicado(s).", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.TestApplied obj)
        {
            try
            {
                var result = await InsertAsync(InsertSql, Convert(obj));
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao cadastrar o teste aplicado.");
                obj.Id = result.Data;
                var returnData = result.Data;
                return DefaultResult<int>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Ocorreu um erro ao cadastrar o teste aplicado.", ex);
            }
        }

        public async Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.TestApplied oldObj, Domain.Model.TestApplied newObj)
        {
            try
            {
                if (filters == null || !filters.Any(x => x._Fields.ElementAt(0) == "Id" && x._Values.Any(y => int.Parse(y.ToString()) > 0)))
                    return DefaultResult<bool>.Break("ID do teste aplicado não informado.", null);
                var updateList = new Dictionary<string, object>();
                if (oldObj.StatusId != newObj.StatusId)
                    updateList.Add(_FieldStatusId, newObj.StatusId);
                if (oldObj.CompanyTestId != newObj.CompanyTestId)
                    updateList.Add(_FieldCompanyTestId, newObj.CompanyTestId);
                if (oldObj.PersonId != newObj.PersonId)
                    updateList.Add(_FieldPersonId, newObj.PersonId);
                if (oldObj.DateBegin != newObj.DateBegin)
                    updateList.Add(_FieldDateBegin, newObj.DateBegin);
                if (oldObj.DateEnd != newObj.DateEnd)
                    updateList.Add(_FieldDateEnd, newObj.DateEnd);

                var parsedFilters = FilterParser.Parse<Model.TestApplied>(filters);
                var result = await UpdateAsync(UpdateSql, updateList, parsedFilters);
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao editar o teste aplicado.");
                var returnData = result.Data;
                return DefaultResult<bool>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Ocorreu um erro na edição do teste aplicado.", ex);
            }
        }
    }
}