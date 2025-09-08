using PL.Adapter.MySql.Interface;
using PL.Application.Interface;
using PL.Domain.Model.Enum;
using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util.Model.Paged;

namespace PL.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserDataSource _dataSource;

        public UserService(IUserDataSource? dataSource)
        {
            _dataSource = dataSource;
        }

        private IEnumerable<Filter> GetFilters(UserFilter filter)
        {
            var filterList = new List<Filter>();
            if (filter == null)
                return filterList;
            if (filter.Id.HasValue && filter.Id > 0)
                filterList.Add(Filter.Create("Id", EOperator.Equal, new[] { filter.Id.Value }));
            if (filter.Ids != null && filter.Ids.Any())
                filterList.Add(Filter.Create("Id", EOperator.In, filter.Ids));
            if (!string.IsNullOrEmpty(filter.Cellphone))
                filterList.Add(Filter.Create("Cellphone", EOperator.Equal, new[] { filter.Cellphone }));
            if (!string.IsNullOrEmpty(filter.Name))
            {
                var split = filter.Name.Split(",");
                foreach (var item in split)
                    filterList.Add(Filter.Create("Name", EOperator.Like, new[] { item.Trim() }, orGroup: "Name"));
            }
            return filterList;
        }

        public async Task<IResult<Paged<Domain.Model.User>>> SearchPaged(UserFilter filter, int skip, int take)
        {
            try
            {
                var result = await _dataSource.SearchPaged(GetFilters(filter), skip, take);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.User>>.Error("Houve uma falha ao buscar os dados do(s) usuário(s).", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.User>>> Search(UserFilter filter)
        {
            try
            {
                var filters = GetFilters(filter);
                var result = await _dataSource.Search(filters);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.User>>.Error("Houve uma falha ao buscar os dados do usuário.", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.User obj)
        {
            try
            {
                obj.StatusId = EStatus.Ativo;
                var result = await _dataSource.Insert(obj);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Houve uma falha ao cadastrar o usuário.", ex);
            }
        }
    }
}