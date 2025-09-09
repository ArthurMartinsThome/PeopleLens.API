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
    public class ResponseOptionService : IResponseOptionService
    {
        private readonly IResponseOptionDataSource _dataSource;

        public ResponseOptionService(IResponseOptionDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        private IEnumerable<Filter> GetFilters(ResponseOptionFilter filter)
        {
            var filterList = new List<Filter>();
            if (filter == null)
                return filterList;
            if (filter.Id.HasValue && filter.Id > 0)
                filterList.Add(Filter.Create("Id", EOperator.Equal, new[] { filter.Id.Value }));
            if (filter.QuestionId.HasValue && filter.QuestionId > 0)
                filterList.Add(Filter.Create("QuestionId", EOperator.Equal, filter.QuestionId.Value));
            if (!string.IsNullOrEmpty(filter.Text))
                filterList.Add(Filter.Create("Text", EOperator.Equal, filter.Text));
            return filterList;
        }

        public async Task<IResult<Paged<Domain.Model.ResponseOption>>> SearchPaged(ResponseOptionFilter filter, int skip, int take)
        {
            try
            {
                var result = await _dataSource.SearchPaged(GetFilters(filter), skip, take);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.ResponseOption>>.Error("Houve uma falha ao buscar os dados da(s) opção(ões) de resposta.", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.ResponseOption>>> Search(ResponseOptionFilter filter)
        {
            try
            {
                var filters = GetFilters(filter);
                var result = await _dataSource.Search(filters);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.ResponseOption>>.Error("Houve uma falha ao buscar os dados da opção de resposta.", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.ResponseOption obj)
        {
            try
            {
                var result = await _dataSource.Insert(obj);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Houve uma falha ao cadastrar a opção de resposta.", ex);
            }
        }

        public async Task<IResult<bool>> Update(Domain.Model.ResponseOption obj)
        {
            try
            {
                if (!obj.Id.HasValue || obj.Id <= 0)
                    return DefaultResult<bool>.Error("Falta referenciar qual o id para editar a opção de resposta.", new Exception($"Update: newObj.Id == (null/0)"));

                var searchResult = await Search(new ResponseOptionFilter { Id = obj.Id });
                if (!searchResult.Succeded || searchResult.Data == null)
                    return DefaultResult<bool>.Break("Opção de resposta não encontrada para atualização.");

                var oldObj = searchResult.Data.First();
                var newObj = (Domain.Model.ResponseOption)searchResult.Data.First().Clone();

                if (obj.ResponseId != null && obj.ResponseId > 0)
                    newObj.ResponseId = obj.ResponseId;
                if (obj.QuestionResponseOptionId != null && obj.QuestionResponseOptionId > 0)
                    newObj.QuestionResponseOptionId = obj.QuestionResponseOptionId;

                var filters = GetFilters(new ResponseOptionFilter() { Id = obj.Id });
                return await _dataSource.Update(filters, oldObj, newObj);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Houve uma falha ao editar a opção de resposta.", ex);
            }
        }
    }
}