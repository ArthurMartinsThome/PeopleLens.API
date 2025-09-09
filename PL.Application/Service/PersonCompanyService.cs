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
    public class PersonCompanyService : IPersonCompanyService
    {
        private readonly IPersonCompanyDataSource _dataSource;

        public PersonCompanyService(IPersonCompanyDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        private IEnumerable<Filter> GetFilters(PersonCompanyFilter filter)
        {
            var filterList = new List<Filter>();
            if (filter == null)
                return filterList;
            if (filter.Id.HasValue && filter.Id > 0)
                filterList.Add(Filter.Create("Id", EOperator.Equal, new[] { filter.Id.Value }));
            if (filter.PersonId.HasValue && filter.PersonId > 0)
                filterList.Add(Filter.Create("PersonId", EOperator.Equal, filter.PersonId.Value));
            if (filter.CompanyId.HasValue && filter.CompanyId > 0)
                filterList.Add(Filter.Create("CompanyId", EOperator.Equal, filter.CompanyId.Value));
            if (filter.StatusId.HasValue && filter.StatusId > 0)
                filterList.Add(Filter.Create("StatusId", EOperator.Equal, filter.StatusId.Value));
            if (filter.CreatedFrom != null)
                filterList.Add(Filter.Create("CreatedAt", EOperator.GreaterThanOrEqual, filter.CreatedFrom.Value));
            if (filter.CreatedTo != null)
                filterList.Add(Filter.Create("CreatedAt", EOperator.LessThanOrEqual, filter.CreatedTo.Value));

            return filterList;
        }

        public async Task<IResult<Paged<Domain.Model.PersonCompany>>> SearchPaged(PersonCompanyFilter filter, int skip, int take)
        {
            try
            {
                var result = await _dataSource.SearchPaged(GetFilters(filter), skip, take);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.PersonCompany>>.Error("Houve uma falha ao buscar os dados do(s) vínculo(s) pessoa-empresa.", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.PersonCompany>>> Search(PersonCompanyFilter filter)
        {
            try
            {
                var filters = GetFilters(filter);
                var result = await _dataSource.Search(filters);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.PersonCompany>>.Error("Houve uma falha ao buscar os dados do vínculo pessoa-empresa.", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.PersonCompany obj)
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
                return DefaultResult<int>.Error("Houve uma falha ao cadastrar o vínculo pessoa-empresa.", ex);
            }
        }

        public async Task<IResult<bool>> Update(Domain.Model.PersonCompany obj)
        {
            try
            {
                if (!obj.Id.HasValue || obj.Id <= 0)
                    return DefaultResult<bool>.Error("Falta referenciar qual o id para editar o vínculo pessoa-empresa.", new Exception($"Update: newObj.Id == (null/0)"));

                var searchResult = await Search(new PersonCompanyFilter { Id = obj.Id });
                if (!searchResult.Succeded || searchResult.Data == null)
                    return DefaultResult<bool>.Break("Vínculo pessoa-empresa não encontrado para atualização.");

                var oldObj = searchResult.Data.First();
                var newObj = (Domain.Model.PersonCompany)searchResult.Data.First().Clone();

                if (obj.PersonId != null && obj.PersonId > 0)
                    newObj.PersonId = obj.PersonId;
                if (obj.CompanyId != null && obj.CompanyId > 0)
                    newObj.CompanyId = obj.CompanyId;
                if (obj.StatusId != null && obj.StatusId > 0)
                    newObj.StatusId = obj.StatusId;

                var filters = GetFilters(new PersonCompanyFilter() { Id = obj.Id });
                return await _dataSource.Update(filters, oldObj, newObj);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Houve uma falha ao editar o vínculo pessoa-empresa.", ex);
            }
        }
    }
}