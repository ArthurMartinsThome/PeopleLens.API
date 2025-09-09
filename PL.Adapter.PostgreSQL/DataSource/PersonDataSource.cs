using Microsoft.Extensions.Options;
using PL.Adapter.PostgreSQL.Common;
using PL.Adapter.PostgreSQL.Interface;
using PL.Adapter.PostgreSQL.Model;
using PL.Infra.DefaultResult;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util;
using PL.Infra.Util.Model;
using PL.Infra.Util.Model.Paged;

namespace PL.Adapter.PostgreSQL.DataSource
{
    public class PersonDataSource : BaseDataSource, IPersonDataSource
    {
        public PersonDataSource(IOptions<EnvironmentSettings> envOptions) : base(envOptions.Value.ConnectionString) { }

        #region SQL
        private const string _TableName = $"person";
        private const string _FieldId = $"id";
        private const string _FieldName = $"name";
        private const string _FieldBirthday = $"birthday";
        private const string _FieldCreatedAt = $"created_at";
        private const string _FieldUpdatedAt = $"updated_at";

        private IEnumerable<string> _AllFields = new[]
        {
            $"\"{_TableName}\".\"{_FieldId}\"",
            $"\"{_TableName}\".\"{_FieldName}\"",
            $"\"{_TableName}\".\"{_FieldBirthday}\"",
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
                ({_FieldName},{_FieldBirthday},{_FieldCreatedAt},{_FieldUpdatedAt})
            VALUES
                (@{_FieldName},@{_FieldBirthday},@{_FieldCreatedAt},@{_FieldUpdatedAt})";
        private const string UpdateSql = $@"
            UPDATE 
                {_TableName}
            SET
                [fieldsandvalues]
            [where]";

        #endregion

        private Domain.Model.Person Convert(Person obj)
        {
            return new Domain.Model.Person
            {
                Id = obj.id,
                Name = obj.name,
                Birthday = obj.birthday,
                CreatedAt = obj.created_at,
                UpdatedAt = obj.updated_at
            };
        }
        private Person Convert(Domain.Model.Person obj)
        {
            return new Person
            {
                id = obj.Id,
                name = obj.Name,
                birthday = obj.Birthday,
                created_at = obj.CreatedAt,
                updated_at = obj.UpdatedAt
            };
        }

        public async Task<IResult<Paged<Domain.Model.Person>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.Person>(filters);
                var result = await Query<Model.Person>(SearchSql, _AllFields, parsedFilters, skip, take);
                var countResult = await Query<int>(SearchSql, new[] { "count(1)" }, parsedFilters);
                var returnData = result.Data.Select(Convert).ToArray();
                return DefaultResult<Paged<Domain.Model.Person>>.Create(new Paged<Domain.Model.Person>(returnData, skip, take, countResult.Data.Single()));
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.Person>>.Error("Ocorreu um erro ao procurar a(s) pessoa(s).", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.Person>>> Search(IEnumerable<Filter> filters)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.Person>(filters);
                var result = await Query<Model.Person>(SearchSql, _AllFields, parsedFilters);
                if (!result.Succeded)
                    return result.ChangeType<IEnumerable<Domain.Model.Person>>(default).AddMessage("Ocorreu um erro ao procurar os dados da(s) pessoa(s).");
                if (!result.Data.Any())
                    return result.ChangeType<IEnumerable<Domain.Model.Person>>(default).AddMessage("Não há dados da(s) pessoa(s) para serem exibidos.");
                var returnData = result.Data.Select(Convert).ToArray();
                return result.ChangeType<IEnumerable<Domain.Model.Person>>(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.Person>>.Error("Ocorreu um erro ao procurar os dados da(s) pessoa(s).", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.Person obj)
        {
            try
            {
                var result = await InsertAsync(InsertSql, Convert(obj));
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao cadastrar a pessoa.");
                obj.Id = result.Data;
                var returnData = result.Data;
                return DefaultResult<int>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Ocorreu um erro ao cadastrar a pessoa.", ex);
            }
        }

        public async Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.Person oldObj, Domain.Model.Person newObj)
        {
            try
            {
                if (filters == null || !filters.Any(x => x._Fields.ElementAt(0) == "Id" && x._Values.Any(y => int.Parse(y.ToString()) > 0)))
                    return DefaultResult<bool>.Break("ID da pessoa não informado.", null);
                var updateList = new Dictionary<string, object>();
                if (oldObj.Name != newObj.Name)
                    updateList.Add(_FieldName, newObj.Name?.Trim());
                if (oldObj.Birthday != newObj.Birthday)
                    updateList.Add(_FieldBirthday, newObj.Birthday);

                updateList.Add(_FieldUpdatedAt, DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                var parsedFilters = FilterParser.Parse<Model.Person>(filters);
                var result = await UpdateAsync(UpdateSql, updateList, parsedFilters);
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao editar a pessoa.");
                var returnData = result.Data;
                return DefaultResult<bool>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Ocorreu um erro na edição da pessoa.", ex);
            }
        }
    }
}