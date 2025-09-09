using Mysqlx.Crud;
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
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyDataSource _dataSource;

        public CompanyService(ICompanyDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        private IEnumerable<Filter> GetFilters(CompanyFilter filter)
        {
            var filterList = new List<Filter>();
            if (filter == null)
                return filterList;
            if (filter.Id.HasValue && filter.Id > 0)
                filterList.Add(Filter.Create("Id", EOperator.Equal, new[] { filter.Id.Value }));
            if (!string.IsNullOrEmpty(filter.CompanyName))
                filterList.Add(Filter.Create("CompanyName", EOperator.Equal, filter.CompanyName));
            if (!string.IsNullOrEmpty(filter.CNPJ))
                filterList.Add(Filter.Create("CNPJ", EOperator.Equal, filter.CNPJ));
            if (filter.StatusId.HasValue && filter.StatusId > 0)
                filterList.Add(Filter.Create("StatusId", EOperator.Equal, filter.StatusId.Value));

            return filterList;
        }

        public async Task<IResult<Paged<Domain.Model.Company>>> SearchPaged(CompanyFilter filter, int skip, int take)
        {
            try
            {
                var result = await _dataSource.SearchPaged(GetFilters(filter), skip, take);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.Company>>.Error("Houve uma falha ao buscar os dados da(s) empresa(s).", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.Company>>> Search(CompanyFilter filter)
        {
            try
            {
                var filters = GetFilters(filter);
                var result = await _dataSource.Search(filters);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.Company>>.Error("Houve uma falha ao buscar os dados da empresa.", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.Company obj)
        {
            try
            {
                obj.CreatedAt = obj.UpdatedAt = DateTime.UtcNow;
                obj.StatusId = EStatus.Ativo;
                var result = await _dataSource.Insert(obj);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Houve uma falha ao cadastrar a empresa.", ex);
            }
        }

        public async Task<IResult<bool>> Update(Domain.Model.Company obj)
        {
            try
            {
                if (!obj.Id.HasValue || obj.Id <= 0)
                    return DefaultResult<bool>.Error("Falta referenciar qual o id para editar a empresa.", new Exception($"Update: newObj.Id == (null/0)"));

                var searchResult = await Search(new CompanyFilter { Id = obj.Id });
                if (!searchResult.Succeded || searchResult.Data == null)
                    return DefaultResult<bool>.Break("Empresa não encontrada para atualização.");

                var oldObj = searchResult.Data.First();
                var newObj = (Domain.Model.Company)searchResult.Data.First().Clone();

                if(obj.StatusId != null && obj.StatusId > 0)
                    newObj.StatusId = obj.StatusId;
                if(!string.IsNullOrEmpty(obj.Name))
                    newObj.Name = obj.Name;
                if(!string.IsNullOrEmpty(obj.Cnpj))
                    newObj.Cnpj = obj.Cnpj;

                var filters = GetFilters(new CompanyFilter() { Id = obj.Id });
                return await _dataSource.Update(filters, oldObj, newObj);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Houve uma falha ao editar a empresa.", ex);
            }
        }
    }
}