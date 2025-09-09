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
    public class CompanyTestService : ICompanyTestService
    {
        private readonly ICompanyTestDataSource _dataSource;

        public CompanyTestService(ICompanyTestDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        private IEnumerable<Filter> GetFilters(CompanyTestFilter filter)
        {
            var filterList = new List<Filter>();
            if (filter == null)
                return filterList;
            if (filter.Id.HasValue && filter.Id > 0)
                filterList.Add(Filter.Create("Id", EOperator.Equal, new[] { filter.Id.Value }));
            if (filter.CompanyId.HasValue && filter.CompanyId > 0)
                filterList.Add(Filter.Create("CompanyId", EOperator.Equal, filter.CompanyId.Value));
            if (filter.TestId.HasValue && filter.TestId > 0)
                filterList.Add(Filter.Create("TestId", EOperator.Equal, filter.TestId.Value));
            if (filter.AssociationDateFrom.HasValue)
                filterList.Add(Filter.Create("AssociationDate", EOperator.GreaterThanOrEqual, filter.AssociationDateFrom.Value));
            if (filter.AssociationDateTo.HasValue)
                filterList.Add(Filter.Create("AssociationDate", EOperator.LessThanOrEqual, filter.AssociationDateTo.Value));

            return filterList;
        }

        public async Task<IResult<Paged<Domain.Model.CompanyTest>>> SearchPaged(CompanyTestFilter filter, int skip, int take)
        {
            try
            {
                var result = await _dataSource.SearchPaged(GetFilters(filter), skip, take);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.CompanyTest>>.Error("Houve uma falha ao buscar os dados da(s) empresa(s) - teste(s).", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.CompanyTest>>> Search(CompanyTestFilter filter)
        {
            try
            {
                var filters = GetFilters(filter);
                var result = await _dataSource.Search(filters);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.CompanyTest>>.Error("Houve uma falha ao buscar os dados da empresa - teste.", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.CompanyTest obj)
        {
            try
            {
                var result = await _dataSource.Insert(obj);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Houve uma falha ao cadastrar a empresa - teste.", ex);
            }
        }

        public async Task<IResult<bool>> Update(Domain.Model.CompanyTest obj)
        {
            try
            {
                if (!obj.Id.HasValue || obj.Id <= 0)
                    return DefaultResult<bool>.Error("Falta referenciar qual o id para editar a empresa - teste.", new Exception($"Update: newObj.Id == (null/0)"));

                var searchResult = await Search(new CompanyTestFilter { Id = obj.Id });
                if (!searchResult.Succeded || searchResult.Data == null)
                    return DefaultResult<bool>.Break("Empresa - teste não encontrada para atualização.");

                var oldObj = searchResult.Data.First();
                var newObj = (Domain.Model.CompanyTest)searchResult.Data.First().Clone();

                if (obj.TestId != null && obj.TestId > 0)
                    newObj.TestId = obj.TestId;
                if (obj.CompanyId != null && obj.CompanyId > 0)
                    newObj.CompanyId = obj.CompanyId;

                var filters = GetFilters(new CompanyTestFilter() { Id = obj.Id });
                return await _dataSource.Update(filters, oldObj, newObj);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Houve uma falha ao editar a empresa - teste.", ex);
            }
        }
    }
}