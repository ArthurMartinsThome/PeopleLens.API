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
    public class TestAppliedService : ITestAppliedService
    {
        private readonly ITestAppliedDataSource _dataSource;

        public TestAppliedService(ITestAppliedDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        private IEnumerable<Filter> GetFilters(TestAppliedFilter filter)
        {
            var filterList = new List<Filter>();
            if (filter == null)
                return filterList;
            if (filter.Id.HasValue && filter.Id > 0)
                filterList.Add(Filter.Create("Id", EOperator.Equal, new[] { filter.Id.Value }));
            if (filter.CompanyTestId.HasValue && filter.CompanyTestId > 0)
                filterList.Add(Filter.Create("CompanyTestId", EOperator.Equal, filter.CompanyTestId.Value));
            if (filter.PersonId.HasValue && filter.PersonId > 0)
                filterList.Add(Filter.Create("PersonId", EOperator.Equal, filter.PersonId.Value));
            if (filter.StatusId.HasValue && filter.StatusId > 0)
                filterList.Add(Filter.Create("StatusId", EOperator.Equal, filter.StatusId.Value));
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

        public async Task<IResult<Paged<Domain.Model.TestApplied>>> SearchPaged(TestAppliedFilter filter, int skip, int take)
        {
            try
            {
                var result = await _dataSource.SearchPaged(GetFilters(filter), skip, take);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.TestApplied>>.Error("Houve uma falha ao buscar os dados do(s) teste(s) aplicado(s).", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.TestApplied>>> Search(TestAppliedFilter filter)
        {
            try
            {
                var filters = GetFilters(filter);
                var result = await _dataSource.Search(filters);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.TestApplied>>.Error("Houve uma falha ao buscar os dados do teste aplicado.", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.TestApplied obj)
        {
            try
            {
                obj.StatusId = EStatus.Ativo;
                var result = await _dataSource.Insert(obj);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Houve uma falha ao cadastrar o teste aplicado.", ex);
            }
        }

        public async Task<IResult<bool>> Update(Domain.Model.TestApplied obj)
        {
            try
            {
                if (!obj.Id.HasValue || obj.Id <= 0)
                    return DefaultResult<bool>.Error("Falta referenciar qual o id para editar o teste aplicado.", new Exception($"Update: newObj.Id == (null/0)"));

                var searchResult = await Search(new TestAppliedFilter { Id = obj.Id });
                if (!searchResult.Succeded || searchResult.Data == null)
                    return DefaultResult<bool>.Break("Teste aplicado não encontrada para atualização.");

                var oldObj = searchResult.Data.First();
                var newObj = (Domain.Model.TestApplied)searchResult.Data.First().Clone();

                if (obj.StatusId != null && obj.StatusId > 0)
                    newObj.StatusId = obj.StatusId;
                if (obj.CompanyTestId != null && obj.CompanyTestId > 0)
                    newObj.CompanyTestId = obj.CompanyTestId;
                if (obj.PersonId != null && obj.PersonId > 0)
                    newObj.PersonId = obj.PersonId;
                if (obj.DateBegin != null && obj.DateBegin > DateTime.MinValue)
                    newObj.DateBegin = obj.DateBegin;
                if (obj.DateEnd != null && obj.DateEnd > DateTime.MinValue)
                    newObj.DateEnd = obj.DateEnd;

                var filters = GetFilters(new TestAppliedFilter() { Id = obj.Id });
                return await _dataSource.Update(filters, oldObj, newObj);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Houve uma falha ao editar o teste aplicado.", ex);
            }
        }
    }
}