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
    public class PersonService : IPersonService
    {
        private readonly IPersonDataSource _dataSource;

        public PersonService(IPersonDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        private IEnumerable<Filter> GetFilters(PersonFilter filter)
        {
            var filterList = new List<Filter>();
            if (filter == null)
                return filterList;
            if (filter.Id.HasValue && filter.Id > 0)
                filterList.Add(Filter.Create("Id", EOperator.Equal, new[] { filter.Id.Value }));
            if (!string.IsNullOrEmpty(filter.Name))
                filterList.Add(Filter.Create("Name", EOperator.Equal, filter.Name));
            if (filter.BirthDateFrom != null)
                filterList.Add(Filter.Create("BirthDate", EOperator.GreaterThanOrEqual, filter.BirthDateFrom.Value));
            if (filter.BirthDateTo != null)
                filterList.Add(Filter.Create("BirthDate", EOperator.LessThanOrEqual, filter.BirthDateTo.Value));
            if (filter.Type != null)
                filterList.Add(Filter.Create("Type", EOperator.Equal, filter.Type));

            return filterList;
        }

        public async Task<IResult<Paged<Domain.Model.Person>>> SearchPaged(PersonFilter filter, int skip, int take)
        {
            try
            {
                var result = await _dataSource.SearchPaged(GetFilters(filter), skip, take);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.Person>>.Error("Houve uma falha ao buscar os dados da(s) pessoa(s).", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.Person>>> Search(PersonFilter filter)
        {
            try
            {
                var filters = GetFilters(filter);
                var result = await _dataSource.Search(filters);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.Person>>.Error("Houve uma falha ao buscar os dados da pessoa.", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.Person obj)
        {
            try
            {
                obj.CreatedAt = obj.UpdatedAt = DateTime.UtcNow;
                var result = await _dataSource.Insert(obj);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Houve uma falha ao cadastrar a pessoa.", ex);
            }
        }

        public async Task<IResult<bool>> Update(Domain.Model.Person obj)
        {
            try
            {
                if (!obj.Id.HasValue || obj.Id <= 0)
                    return DefaultResult<bool>.Error("Falta referenciar qual o id para editar a pessoa.", new Exception($"Update: newObj.Id == (null/0)"));

                var searchResult = await Search(new PersonFilter { Id = obj.Id });
                if (!searchResult.Succeded || searchResult.Data == null)
                    return DefaultResult<bool>.Break("Pessoa não encontrada para atualização.");

                var oldObj = searchResult.Data.First();
                var newObj = (Domain.Model.Person)searchResult.Data.First().Clone();

                if (!string.IsNullOrEmpty(obj.Name))
                    newObj.Name = obj.Name;
                if (obj.Birthday != null && obj.Birthday > DateTime.MinValue)
                    newObj.Birthday = obj.Birthday;

                var filters = GetFilters(new PersonFilter() { Id = obj.Id });
                return await _dataSource.Update(filters, oldObj, newObj);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Houve uma falha ao editar a pessoa.", ex);
            }
        }
    }
}