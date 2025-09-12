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
    public class ResponseTypeProfileService : IResponseTypeProfileService
    {
        private readonly IResponseTypeProfileDataSource _dataSource;

        public ResponseTypeProfileService(IResponseTypeProfileDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        private IEnumerable<Filter> GetFilters(ResponseTypeProfileFilter filter)
        {
            var filterList = new List<Filter>();
            if (filter == null)
                return filterList;
            if (filter.Id.HasValue && filter.Id > 0)
                filterList.Add(Filter.Create("Id", EOperator.Equal, new[] { filter.Id.Value }));
            if (!string.IsNullOrEmpty(filter.Name))
                filterList.Add(Filter.Create("Name", EOperator.Equal, filter.Name));
            if (!string.IsNullOrEmpty(filter.Code))
                filterList.Add(Filter.Create("Code", EOperator.Equal, filter.Code));
            return filterList;
        }

        public async Task<IResult<Paged<Domain.Model.ResponseTypeProfile>>> SearchPaged(ResponseTypeProfileFilter filter, int skip, int take)
        {
            try
            {
                var result = await _dataSource.SearchPaged(GetFilters(filter), skip, take);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.ResponseTypeProfile>>.Error("Houve uma falha ao buscar os dados do(s) perfil(is) de tipo de resposta.", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.ResponseTypeProfile>>> Search(ResponseTypeProfileFilter filter)
        {
            try
            {
                var filters = GetFilters(filter);
                var result = await _dataSource.Search(filters);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.ResponseTypeProfile>>.Error("Houve uma falha ao buscar os dados do perfil de tipo de resposta.", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.ResponseTypeProfile obj)
        {
            try
            {
                var result = await _dataSource.Insert(obj);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Houve uma falha ao cadastrar o perfil de tipo de resposta.", ex);
            }
        }

        public async Task<IResult<bool>> Update(Domain.Model.ResponseTypeProfile obj)
        {
            try
            {
                if (!obj.Id.HasValue || obj.Id <= 0)
                    return DefaultResult<bool>.Error("Falta referenciar qual o id para editar o perfil de tipo de resposta.", new Exception($"Update: newObj.Id == (null/0)"));

                var searchResult = await Search(new ResponseTypeProfileFilter { Id = obj.Id });
                if (!searchResult.Succeded || searchResult.Data == null)
                    return DefaultResult<bool>.Break("Perfil de tipo de resposta não encontrado para atualização.");

                var oldObj = searchResult.Data.First();
                var newObj = (Domain.Model.ResponseTypeProfile)searchResult.Data.First().Clone();

                if (!string.IsNullOrEmpty(obj.Name))
                    newObj.Name = obj.Name;
                if (!string.IsNullOrEmpty(obj.Code))
                    newObj.Code = obj.Code;
                if (!string.IsNullOrEmpty(obj.Description))
                    newObj.Description = obj.Description;

                var filters = GetFilters(new ResponseTypeProfileFilter() { Id = obj.Id });
                return await _dataSource.Update(filters, oldObj, newObj);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Houve uma falha ao editar o perfil de tipo de resposta.", ex);
            }
        }
    }
}