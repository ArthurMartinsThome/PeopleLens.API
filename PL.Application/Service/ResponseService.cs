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
    public class ResponseService : IResponseService
    {
        private readonly IResponseDataSource _dataSource;

        public ResponseService(IResponseDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        private IEnumerable<Filter> GetFilters(ResponseFilter filter)
        {
            var filterList = new List<Filter>();
            if (filter == null)
                return filterList;
            if (filter.Id.HasValue && filter.Id > 0)
                filterList.Add(Filter.Create("Id", EOperator.Equal, new[] { filter.Id.Value }));
            if (filter.TestAppliedId.HasValue && filter.TestAppliedId > 0)
                filterList.Add(Filter.Create("TestAppliedId", EOperator.Equal, filter.TestAppliedId.Value));
            if (filter.QuestionId.HasValue && filter.QuestionId > 0)
                filterList.Add(Filter.Create("QuestionId", EOperator.Equal, filter.QuestionId.Value));
            return filterList;
        }

        public async Task<IResult<Paged<Domain.Model.Response>>> SearchPaged(ResponseFilter filter, int skip, int take)
        {
            try
            {
                var result = await _dataSource.SearchPaged(GetFilters(filter), skip, take);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.Response>>.Error("Houve uma falha ao buscar os dados da(s) resposta(s).", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.Response>>> Search(ResponseFilter filter)
        {
            try
            {
                var filters = GetFilters(filter);
                var result = await _dataSource.Search(filters);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.Response>>.Error("Houve uma falha ao buscar os dados da resposta.", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.Response obj)
        {
            try
            {
                var result = await _dataSource.Insert(obj);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Houve uma falha ao cadastrar a resposta.", ex);
            }
        }

        public async Task<IResult<bool>> Update(Domain.Model.Response obj)
        {
            try
            {
                if (!obj.Id.HasValue || obj.Id <= 0)
                    return DefaultResult<bool>.Error("Falta referenciar qual o id para editar a resposta.", new Exception($"Update: newObj.Id == (null/0)"));

                var searchResult = await Search(new ResponseFilter { Id = obj.Id });
                if (!searchResult.Succeded || searchResult.Data == null)
                    return DefaultResult<bool>.Break("Resposta não encontrada para atualização.");

                var oldObj = searchResult.Data.First();
                var newObj = (Domain.Model.Response)searchResult.Data.First().Clone();

                if (obj.TestAppliedId != null && obj.TestAppliedId > 0)
                    newObj.TestAppliedId = obj.TestAppliedId;
                if (obj.QuestionId != null && obj.QuestionId > 0)
                    newObj.QuestionId = obj.QuestionId;
                if (!string.IsNullOrEmpty(obj.Value))
                    newObj.Value = obj.Value;

                var filters = GetFilters(new ResponseFilter() { Id = obj.Id });
                return await _dataSource.Update(filters, oldObj, newObj);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Houve uma falha ao editar a resposta.", ex);
            }
        }
    }
}