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
    public class ResponseTypeService : IResponseTypeService
    {
        private readonly IResponseTypeDataSource _dataSource;

        public ResponseTypeService(IResponseTypeDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        private IEnumerable<Filter> GetFilters(ResponseTypeFilter filter)
        {
            var filterList = new List<Filter>();
            if (filter == null)
                return filterList;
            if (filter.Id.HasValue && filter.Id > 0)
                filterList.Add(Filter.Create("Id", EOperator.Equal, new[] { filter.Id.Value }));
            if (!string.IsNullOrEmpty(filter.Name))
                filterList.Add(Filter.Create("Name", EOperator.Equal, filter.Name));
            if (!string.IsNullOrEmpty(filter.Description))
                filterList.Add(Filter.Create("Description", EOperator.Equal, filter.Description));
            return filterList;
        }

        public async Task<IResult<Paged<Domain.Model.ResponseType>>> SearchPaged(ResponseTypeFilter filter, int skip, int take)
        {
            try
            {
                var result = await _dataSource.SearchPaged(GetFilters(filter), skip, take);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.ResponseType>>.Error("Houve uma falha ao buscar os dados do(s) tipo(s) de resposta.", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.ResponseType>>> Search(ResponseTypeFilter filter)
        {
            try
            {
                var filters = GetFilters(filter);
                var result = await _dataSource.Search(filters);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.ResponseType>>.Error("Houve uma falha ao buscar os dados do tipo de resposta.", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.ResponseType obj)
        {
            try
            {
                var result = await _dataSource.Insert(obj);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Houve uma falha ao cadastrar o tipo de resposta.", ex);
            }
        }

        public async Task<IResult<bool>> Update(Domain.Model.ResponseType obj)
        {
            try
            {
                if (!obj.Id.HasValue || obj.Id <= 0)
                    return DefaultResult<bool>.Error("Falta referenciar qual o id para editar o tipo de resposta.", new Exception($"Update: newObj.Id == (null/0)"));

                var searchResult = await Search(new ResponseTypeFilter { Id = obj.Id });
                if (!searchResult.Succeded || searchResult.Data == null)
                    return DefaultResult<bool>.Break("Tipo de resposta não encontrada para atualização.");

                var oldObj = searchResult.Data.First();
                var newObj = (Domain.Model.ResponseType)searchResult.Data.First().Clone();

                if (!string.IsNullOrEmpty(obj.Name))
                    newObj.Name = obj.Name;
                if (!string.IsNullOrEmpty(obj.Description))
                    newObj.Description = obj.Description;

                var filters = GetFilters(new ResponseTypeFilter() { Id = obj.Id });
                return await _dataSource.Update(filters, oldObj, newObj);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Houve uma falha ao editar o tipo de resposta.", ex);
            }
        }
    }
}