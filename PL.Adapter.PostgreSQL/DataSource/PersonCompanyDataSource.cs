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
    public class PersonCompanyDataSource : BaseDataSource, IPersonCompanyDataSource
    {
        public PersonCompanyDataSource(IOptions<EnvironmentSettings> envOptions) : base(envOptions.Value.ConnectionString) { }

        #region SQL
        private const string _TableName = $"person_company";
        private const string _FieldId = $"id";
        private const string _FieldPersonId = $"person_id";
        private const string _FieldCompanyId = $"company_id";
        private const string _FieldStatusId = $"status_id";
        private const string _FieldCreatedAt = $"created_at";
        private const string _FieldUpdatedAt = $"updated_at";

        private IEnumerable<string> _AllFields = new[]
        {
            $"\"{_TableName}\".\"{_FieldId}\"",
            $"\"{_TableName}\".\"{_FieldPersonId}\"",
            $"\"{_TableName}\".\"{_FieldCompanyId}\"",
            $"\"{_TableName}\".\"{_FieldStatusId}\"",
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
                ({_FieldPersonId},{_FieldCompanyId},{_FieldStatusId},{_FieldCreatedAt},{_FieldUpdatedAt})
            VALUES
                (@{_FieldPersonId},@{_FieldCompanyId},@{_FieldStatusId},@{_FieldCreatedAt},@{_FieldUpdatedAt})";
        private const string UpdateSql = $@"
            UPDATE 
                {_TableName}
            SET
                [fieldsandvalues]
            [where]";
        private const string DeleteSql = $@"
            DELETE FROM {_TableName} [where]";

        #endregion

        private Domain.Model.PersonCompany Convert(PersonCompany obj)
        {
            return new Domain.Model.PersonCompany
            {
                Id = obj.id,
                PersonId = obj.person_id,
                CompanyId = obj.company_id,
                StatusId = (EStatus)obj.status_id,
                CreatedAt = obj.created_at,
                UpdatedAt = obj.updated_at
            };
        }
        private PersonCompany Convert(Domain.Model.PersonCompany obj)
        {
            return new PersonCompany
            {
                id = obj.Id,
                person_id = obj.PersonId,
                company_id = obj.CompanyId,
                status_id = obj.StatusId.GetHashCode(),
                created_at = obj.CreatedAt,
                updated_at = obj.UpdatedAt
            };
        }

        public async Task<IResult<Paged<Domain.Model.PersonCompany>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.PersonCompany>(filters);
                var result = await Query<Model.PersonCompany>(SearchSql, _AllFields, parsedFilters, skip, take);
                var countResult = await Query<int>(SearchSql, new[] { "count(1)" }, parsedFilters);
                var returnData = result.Data.Select(Convert).ToArray();
                return DefaultResult<Paged<Domain.Model.PersonCompany>>.Create(new Paged<Domain.Model.PersonCompany>(returnData, skip, take, countResult.Data.Single()));
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.PersonCompany>>.Error("Ocorreu um erro ao procurar a(s) pessoa(s)/empresa(s).", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.PersonCompany>>> Search(IEnumerable<Filter> filters)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.PersonCompany>(filters);
                var result = await Query<Model.PersonCompany>(SearchSql, _AllFields, parsedFilters);
                if (!result.Succeded)
                    return result.ChangeType<IEnumerable<Domain.Model.PersonCompany>>(default).AddMessage("Ocorreu um erro ao procurar os dados da(s) pessoa(s)/empresa(s).");
                if (!result.Data.Any())
                    return result.ChangeType<IEnumerable<Domain.Model.PersonCompany>>(default).AddMessage("Não há dados da(s) pessoa(s)/empresa(s) para serem exibidos.");
                var returnData = result.Data.Select(Convert).ToArray();
                return result.ChangeType<IEnumerable<Domain.Model.PersonCompany>>(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.PersonCompany>>.Error("Ocorreu um erro ao procurar os dados da(s) pessoa(s)/empresa(s).", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.PersonCompany obj)
        {
            try
            {
                var result = await InsertAsync(InsertSql, Convert(obj));
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao cadastrar a pessoa/empresa.");
                obj.Id = result.Data;
                var returnData = result.Data;
                return DefaultResult<int>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Ocorreu um erro ao cadastrar a pessoa/empresa.", ex);
            }
        }

        public async Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.PersonCompany oldObj, Domain.Model.PersonCompany newObj)
        {
            try
            {
                if (filters == null || !filters.Any(x => x._Fields.ElementAt(0) == "Id" && x._Values.Any(y => int.Parse(y.ToString()) > 0)))
                    return DefaultResult<bool>.Break("ID da pessoa/empresa não informado.", null);
                var updateList = new Dictionary<string, object>();
                if (oldObj.PersonId != newObj.PersonId)
                    updateList.Add(_FieldPersonId, newObj.PersonId);
                if (oldObj.CompanyId != newObj.CompanyId)
                    updateList.Add(_FieldCompanyId, newObj.CompanyId);
                if (oldObj.StatusId != newObj.StatusId)
                    updateList.Add(_FieldStatusId, newObj.StatusId);

                updateList.Add(_FieldUpdatedAt, DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                var parsedFilters = FilterParser.Parse<Model.PersonCompany>(filters);
                var result = await UpdateAsync(UpdateSql, updateList, parsedFilters);
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao editar a pessoa/empresa.");
                var returnData = result.Data;
                return DefaultResult<bool>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Ocorreu um erro na edição da pessoa/empresa.", ex);
            }
        }
    }
}