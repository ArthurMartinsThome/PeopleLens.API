using Microsoft.Extensions.Options;
using PL.Adapter.MySql.Common;
using PL.Adapter.MySql.Interface;
using PL.Adapter.MySql.Model;
using PL.Domain.Model.Enum;
using PL.Infra.DefaultResult;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util;
using PL.Infra.Util.Model;
using PL.Infra.Util.Model.Paged;

namespace PL.Adapter.MySql.DataSource
{
    public class UserDataSource : BaseDataSource, IUserDataSource
    {
        public UserDataSource(IOptions<EnvironmentSettings> envOptions) : base(envOptions.Value.ConnectionString) { }

        #region SQL
        private const string _TableName = $"user";
        private const string _FieldId = $"id";
        private const string _FieldStatusId = $"status_id";
        private const string _FieldName = $"name";
        private const string _FieldCellphone = $"cellphone";
        private const string _FieldCreatedAt = $"created_at";
        private const string _FieldUpdatedAt = $"updated_at";

        private IEnumerable<string> _AllFields = new[]
        {
            $"{_TableName}.{_FieldId}",
            $"{_TableName}.{_FieldStatusId}",
            $"{_TableName}.{_FieldName}",
            $"{_TableName}.{_FieldCellphone}",
            $"{_TableName}.{_FieldCreatedAt}",
            $"{_TableName}.{_FieldUpdatedAt}"
        };
        private const string SearchSql = $@"
            SELECT 
                [fields]
            FROM {_TableName}
                [where]
                [limit]";
        private const string InsertSql = $@"
            INSERT INTO 
                {_TableName}
                ({_FieldStatusId},{_FieldName},{_FieldCellphone},{_FieldCreatedAt},{_FieldUpdatedAt})
            VALUES
                (@{_FieldStatusId},@{_FieldName},@{_FieldCellphone},@{_FieldCreatedAt},@{_FieldUpdatedAt})";
        private const string UpdateSql = $@"
            UPDATE 
                {_TableName}
                [fieldsandvalues]
                [where]";

        #endregion

        private Domain.Model.User Convert(User obj)
        {
            return new Domain.Model.User
            {
                Id = obj.id,
                StatusId = (EStatus)obj.status_id,
                Name = obj.name,
                Cellphone = obj.cellphone,
                CreatedAt = obj.created_at,
                UpdatedAt = obj.updated_at
            };
        }
        private User Convert(Domain.Model.User obj)
        {
            return new User
            {
                id = obj.Id,
                status_id = obj.StatusId.GetHashCode(),
                name = obj.Name,
                cellphone = obj.Cellphone,
                created_at = obj.CreatedAt,
                updated_at = obj.UpdatedAt
            };
        }

        public async Task<IResult<Paged<Domain.Model.User>>> SearchPaged(IEnumerable<Filter> filters, int skip, int take)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.User>(filters);
                var result = await Query<Model.User>(SearchSql, _AllFields, parsedFilters, skip, take);
                var countResult = await Query<int>(SearchSql, new[] { "count(1)" }, parsedFilters);
                var returnData = result.Data.Select(Convert).ToArray();
                return DefaultResult<Paged<Domain.Model.User>>.Create(new Paged<Domain.Model.User>(returnData, skip, take, countResult.Data.Single()));
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.User>>.Error("Ocorreu um erro ao procurar o(s) usuário(s).", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.User>>> Search(IEnumerable<Filter> filters)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.User>(filters);
                var result = await Query<Model.User>(SearchSql, _AllFields, parsedFilters);
                if (!result.Succeded)
                    return result.ChangeType<IEnumerable<Domain.Model.User>>(default).AddMessage("Ocorreu um erro ao procurar os dados de usuário(s).");
                if (!result.Data.Any())
                    return result.ChangeType<IEnumerable<Domain.Model.User>>(default).AddMessage("Não há dados de usuário(s) para serem exibidos.");
                var returnData = result.Data.Select(Convert).ToArray();
                return result.ChangeType<IEnumerable<Domain.Model.User>>(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.User>>.Error("Ocorreu um erro ao procurar os dados de usuário(s).", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.User obj)
        {
            try
            {
                var result = await InsertAsync(InsertSql, Convert(obj));
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao cadastrar o usuário.");
                obj.Id = result.Data;
                var returnData = result.Data;
                return DefaultResult<int>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Ocorreu um erro ao cadastrar o usuário.", ex);
            }
        }

        public async Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.User oldObj, Domain.Model.User newObj)
        {
            try
            {
                if (filters == null || !filters.Any(x => x._Fields.ElementAt(0) == "Id" && x._Values.Any(y => int.Parse(y.ToString()) > 0)))
                    return DefaultResult<bool>.Break("ID do usuário não informado.", null);
                var updateList = new Dictionary<string, object>();
                if (oldObj.StatusId != newObj.StatusId)
                    updateList.Add(_FieldStatusId, newObj.StatusId);
                if (oldObj.Name != newObj.Name)
                    updateList.Add(_FieldName, newObj.Name?.Trim());
                if (oldObj.Cellphone != newObj.Cellphone)
                    updateList.Add(_FieldCellphone, newObj.Cellphone?.Trim());

                updateList.Add(_FieldUpdatedAt, DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                var parsedFilters = FilterParser.Parse<Model.User>(filters);
                var result = await UpdateAsync(UpdateSql, updateList, parsedFilters);
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao editar o usuário.");
                var returnData = result.Data;
                return DefaultResult<bool>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Ocorreu um erro na edição do usuário.", ex);
            }
        }
    }
}