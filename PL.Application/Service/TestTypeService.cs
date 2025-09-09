using PL.Adapter.PostgreSQL.Interface;
using PL.Application.Interface;
using PL.Domain.Model.Enum;
using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util.Model.Paged;

namespace PL.Application.Service
{
    public class TestTypeService : ITestTypeService
    {
        private readonly ITestTypeDataSource _dataSource;

        public TestTypeService(ITestTypeDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        private IEnumerable<Filter> GetFilters(TestTypeFilter filter)
        {
            var filterList = new List<Filter>();
            if (filter == null)
                return filterList;
            if (filter.Id.HasValue && filter.Id > 0)
                filterList.Add(Filter.Create("Id", EOperator.Equal, new[] { filter.Id.Value }));
            if (filter.StatusId.HasValue && filter.StatusId > 0)
                filterList.Add(Filter.Create("StatusId", EOperator.Equal, filter.StatusId.Value));
            if (!string.IsNullOrEmpty(filter.Name))
                filterList.Add(Filter.Create("Name", EOperator.Equal, filter.Name));
            if (filter.CreatedFrom != null)
                filterList.Add(Filter.Create("CreatedAt", EOperator.GreaterThanOrEqual, filter.CreatedFrom));
            if (filter.CreatedTo != null)
                filterList.Add(Filter.Create("CreatedAt", EOperator.LessThanOrEqual, filter.CreatedTo));
            if (filter.HideInactive)
                filterList.Add(Filter.Create("StatusId", EOperator.NotEqual, new[] { EStatus.Inativo }));
            if (filter.HideDeleted)
                filterList.Add(Filter.Create("StatusId", EOperator.NotEqual, new[] { EStatus.Excluido }));
            return filterList;
        }

        public async Task<IResult<Paged<Domain.Model.TestType>>> SearchPaged(TestTypeFilter filter, int skip, int take)
        {
            try
            {
                var result = await _dataSource.SearchPaged(GetFilters(filter), skip, take);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.TestType>>.Error("Houve uma falha ao buscar os dados do(s) tipo(s) de teste.", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.TestType>>> Search(TestTypeFilter filter)
        {
            try
            {
                var filters = GetFilters(filter);
                var result = await _dataSource.Search(filters);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.TestType>>.Error("Houve uma falha ao buscar os dados do tipo de teste.", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.TestType obj)
        {
            try
            {
                var result = await _dataSource.Insert(obj);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Houve uma falha ao cadastrar o tipo de teste.", ex);
            }
        }

        public async Task<IResult<bool>> Update(Domain.Model.TestType obj)
        {
            try
            {
                if (!obj.Id.HasValue || obj.Id <= 0)
                    return DefaultResult<bool>.Error("Falta referenciar qual o id para editar o tipo de teste.", new Exception($"Update: newObj.Id == (null/0)"));

                var oldCompanyResult = await Search(new TestTypeFilter { Id = obj.Id });
                if (!oldCompanyResult.Succeded || oldCompanyResult.Data == null)
                    return DefaultResult<bool>.Break("Tipo de teste não encontrado para atualização.");

                var oldObj = oldCompanyResult.Data.First();
                var newObj = (Domain.Model.TestType)oldCompanyResult.Data.First().Clone();

                if (!string.IsNullOrEmpty(obj.Name))
                    newObj.Name = obj.Name;
                if (!string.IsNullOrEmpty(obj.Description))
                    newObj.Description = obj.Description;

                var filters = GetFilters(new TestTypeFilter() { Id = obj.Id });
                return await _dataSource.Update(filters, oldObj, newObj);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Houve uma falha ao editar o tipo de teste.", ex);
            }
        }
    }
}