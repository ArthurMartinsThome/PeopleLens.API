using Microsoft.Extensions.Options;
using PL.Adapter.PostgreSQL.Common;
using PL.Adapter.PostgreSQL.Interface;
using PL.Adapter.PostgreSQL.Model;
using PL.Domain.Model.Enum;
using PL.Infra.DefaultResult;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util;
using PL.Infra.Util.Model;
using PL.Infra.Util.Model.Paged;

namespace PL.Adapter.PostgreSQL.DataSource
{
    public class CompanyDataSource : BaseDataSource, ICompanyDataSource
    {
        public CompanyDataSource(IOptions<EnvironmentSettings> envOptions) : base(envOptions.Value.ConnectionString) { }

        #region SQL
        private const string _TableName = "company";
        private const string _FieldId = "id";
        private const string _FieldName = "name";
        private const string _FieldCnpj = "cnpj";
        private const string _FieldCreatedAt = "created_at";
        private const string _FieldUpdatedAt = "updated_at";
        private const string _FieldStatusId = "status_id";

        private IEnumerable<string> _AllFields = new[]
        {
            $"\"{_TableName}\".\"{_FieldId}\"",
            $"\"{_TableName}\".\"{_FieldName}\"",
            $"\"{_TableName}\".\"{_FieldCnpj}\"",
            $"\"{_TableName}\".\"{_FieldCreatedAt}\"",
            $"\"{_TableName}\".\"{_FieldUpdatedAt}\"",
            $"\"{_TableName}\".\"{_FieldStatusId}\""
        };

        private const string SearchSql = $@"
            SELECT 
                [fields]
            FROM ""{_TableName}""
            [where]";
        private const string InsertSql = $@"
            INSERT INTO 
                ""{_TableName}""
                ({_FieldName},{_FieldCnpj},{_FieldCreatedAt},{_FieldUpdatedAt},{_FieldStatusId})
            VALUES
                (@{_FieldName},@{_FieldCnpj},@{_FieldCreatedAt},@{_FieldUpdatedAt},@{_FieldStatusId})";
        private const string UpdateSql = $@"
            UPDATE 
                ""{_TableName}""
            SET
                [fieldsandvalues]
            [where]";

        #endregion

        private Domain.Model.Company Convert(Company obj)
        {
            return new Domain.Model.Company
            {
                Id = obj.id,
                Name = obj.name,
                Cnpj = obj.cnpj,
                CreatedAt = obj.created_at,
                UpdatedAt = obj.updated_at,
                StatusId = (EStatus)obj.status_id
            };
        }
        private Company Convert(Domain.Model.Company obj)
        {
            return new Company
            {
                id = obj.Id,
                name = obj.Name,
                cnpj = obj.Cnpj,
                created_at = obj.CreatedAt,
                updated_at = obj.UpdatedAt,
                status_id = obj.StatusId.GetHashCode()
            };
        }

        public async Task<IResult<Paged<Domain.Model.Company>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.Company>(filters);
                var result = await Query<Model.Company>(SearchSql, _AllFields, parsedFilters, skip, take);
                var countResult = await Query<int>(SearchSql, new[] { "count(1)" }, parsedFilters);
                var returnData = result.Data.Select(Convert).ToArray();
                return DefaultResult<Paged<Domain.Model.Company>>.Create(new Paged<Domain.Model.Company>(returnData, skip, take, countResult.Data.Single()));
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.Company>>.Error("Ocorreu um erro ao procurar a(s) empresa(s).", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.Company>>> Search(IEnumerable<Filter> filters)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.Company>(filters);
                var result = await Query<Model.Company>(SearchSql, _AllFields, parsedFilters);
                if (!result.Succeded)
                    return result.ChangeType<IEnumerable<Domain.Model.Company>>(default).AddMessage("Ocorreu um erro ao procurar os dados da(s) empresa(s).");
                if (!result.Data.Any())
                    return result.ChangeType<IEnumerable<Domain.Model.Company>>(default).AddMessage("Não há dados da(s) empresa(s) para serem exibidos.");
                var returnData = result.Data.Select(Convert).ToArray();
                return result.ChangeType<IEnumerable<Domain.Model.Company>>(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.Company>>.Error("Ocorreu um erro ao procurar os dados da(s) empresa(s).", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.Company obj)
        {
            try
            {
                var result = await InsertAsync(InsertSql, Convert(obj));
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao cadastrar a empresa.");
                obj.Id = result.Data;
                var returnData = result.Data;
                return DefaultResult<int>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Ocorreu um erro ao cadastrar a empresa.", ex);
            }
        }

        public async Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.Company oldObj, Domain.Model.Company newObj)
        {
            try
            {
                if (filters == null || !filters.Any(x => x._Fields.ElementAt(0) == "Id" && x._Values.Any(y => int.Parse(y.ToString()) > 0)))
                    return DefaultResult<bool>.Break("ID da empresa não informado.", null);
                var updateList = new Dictionary<string, object>();
                if (oldObj.Name != newObj.Name)
                    updateList.Add(_FieldName, newObj.Name?.Trim());
                if (oldObj.Cnpj != newObj.Cnpj)
                    updateList.Add(_FieldCnpj, newObj.Cnpj);
                if (oldObj.StatusId != newObj.StatusId)
                    updateList.Add(_FieldStatusId, newObj.StatusId);

                updateList.Add(_FieldUpdatedAt, DateTime.UtcNow);
                var parsedFilters = FilterParser.Parse<Model.Company>(filters);
                var result = await UpdateAsync(UpdateSql, updateList, parsedFilters);
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao editar a empresa.");
                var returnData = result.Data;
                return DefaultResult<bool>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Ocorreu um erro na edição da empresa.", ex);
            }
        }
    }
}