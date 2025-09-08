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

namespace PL.Adapter.MySql.DataSource
{
    public class AuthDataSource : BaseDataSource, IAuthDataSource
    {
        public AuthDataSource(IOptions<EnvironmentSettings> envOptions) : base(envOptions.Value.ConnectionString) { }

        #region SQL
        private const string _TableName = $"auth";
        private const string _FieldId = $"id";
        private const string _FieldStatusId = $"status_id";
        private const string _FieldRoleId = $"role_id";
        private const string _FieldUserId = $"user_id";
        private const string _FieldUsername = $"username";
        private const string _FieldPassword = $"password";
        private const string _FieldCreatedAt = $"created_at";
        private const string _FieldUpdatedAt = $"updated_at";

        private IEnumerable<string> _AllFields = new[]
        {
            $"{_TableName}.{_FieldId}",
            $"{_TableName}.{_FieldStatusId}",
            $"{_TableName}.{_FieldRoleId}",
            $"{_TableName}.{_FieldUserId}",
            $"{_TableName}.{_FieldUsername}",
            $"{_TableName}.{_FieldPassword}",
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
                ({_FieldStatusId},{_FieldRoleId},{_FieldUsername},{_FieldPassword},{_FieldCreatedAt})
            VALUES
                (@{_FieldStatusId},@{_FieldRoleId},@{_FieldUsername},@{_FieldPassword},@{_FieldCreatedAt})";
        private const string UpdateSql = $@"
            UPDATE 
                {_TableName}
                [fieldsandvalues]
                [where]";
        #endregion

        private Domain.Model.Auth Convert(Auth obj)
        {
            return new Domain.Model.Auth
            {
                Id = obj.id,
                StatusId = (EStatus)obj.status_id,
                Role = (ERole)obj.role_id,
                UserId = obj.user_id,
                Username = obj.username,
                Password = obj.password,
                CreatedAt = obj.created_at,
                UpdatedAt = obj.updated_at
            };
        }

        private Auth Convert(Domain.Model.Auth obj)
        {
            return new Auth
            {
                id = obj.Id,
                status_id = obj.StatusId.GetHashCode(),
                role_id = obj.Role.GetHashCode(),
                user_id = obj.UserId,
                username = obj.Username,
                password = obj.Password,
                created_at = obj.CreatedAt,
                updated_at = obj.UpdatedAt
            };
        }

        public async Task<IResult<IEnumerable<Domain.Model.Auth>>> Search(IEnumerable<Filter> filters)
        {
            try
            {
                var parsedFilters = FilterParser.Parse<Model.Auth>(filters);
                var result = await Query<Model.Auth>(SearchSql, _AllFields, parsedFilters);
                if (!result.Succeded)
                    return result.ChangeType<IEnumerable<Domain.Model.Auth>>(default).AddMessage("Ocorreu um erro ao procurar os dados da(s) autenticação(ões).");
                if (!result.Data.Any())
                    return result.ChangeType<IEnumerable<Domain.Model.Auth>>(default).AddMessage("Não há dados da(s) autenticação(ões) para serem exibidos.");
                var returnData = result.Data.Select(Convert).ToArray();
                return result.ChangeType<IEnumerable<Domain.Model.Auth>>(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.Auth>>.Error("Ocorreu um erro ao procurar os dados da(s) autenticação(ões).", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.Auth obj)
        {
            try
            {
                var result = await InsertAsync(InsertSql, Convert(obj));
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao cadastrar a autenticação.");
                obj.Id = result.Data;
                var returnData = result.Data;
                return DefaultResult<int>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Ocorreu um erro ao cadastrar a autenticação.", ex);
            }
        }

        public async Task<IResult<bool>> Update(IEnumerable<Filter> filters, Domain.Model.Auth oldObj, Domain.Model.Auth newObj)
        {
            try
            {
                if (filters == null || !filters.Any(x => x._Fields.ElementAt(0) == "Id" && x._Values.Any(y => int.Parse(y.ToString()) > 0)))
                    return DefaultResult<bool>.Break("ID da autenticação não informado.", null);
                var updateList = new Dictionary<string, object>();
                if (oldObj.StatusId != newObj.StatusId)
                    updateList.Add(_FieldStatusId, newObj.StatusId);
                if (oldObj.Role != newObj.Role)
                    updateList.Add(_FieldRoleId, newObj.Role);
                if (oldObj.UserId != newObj.UserId)
                    updateList.Add(_FieldUserId, newObj.UserId);
                if (oldObj.Username != newObj.Username)
                    updateList.Add(_FieldUsername, newObj.Username);
                if (oldObj.Password != newObj.Password)
                    updateList.Add(_FieldPassword, newObj.Password);

                updateList.Add(_FieldUpdatedAt, DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                var parsedFilters = FilterParser.Parse<Model.Auth>(filters);
                var result = await UpdateAsync(UpdateSql, updateList, parsedFilters);
                if (!result.Succeded)
                    return result.AddMessage("Ocorreu um erro ao editar a autenticação.");
                var returnData = result.Data;
                return DefaultResult<bool>.Create(returnData);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Ocorreu um erro na edição da autenticação.", ex);
            }
        }
    }
}