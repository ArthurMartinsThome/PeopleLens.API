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
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionDataSource _dataSource;

        public QuestionService(IQuestionDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        private IEnumerable<Filter> GetFilters(QuestionFilter filter)
        {
            var filterList = new List<Filter>();
            if (filter == null)
                return filterList;
            if (filter.Id.HasValue && filter.Id > 0)
                filterList.Add(Filter.Create("Id", EOperator.Equal, new[] { filter.Id.Value }));
            if (filter.Ids != null && filter.Ids.Any())
                filterList.Add(Filter.Create("Id", EOperator.In, filter.Ids));
            if (!string.IsNullOrEmpty(filter.Text))
                filterList.Add(Filter.Create("Text", EOperator.Equal, filter.Text));
            if (filter.ResponseTypeId.HasValue && filter.ResponseTypeId > 0)
                filterList.Add(Filter.Create("ResponseTypeId", EOperator.Equal, filter.ResponseTypeId.Value));
            if (filter.TestId.HasValue && filter.TestId > 0)
                filterList.Add(Filter.Create("TestId", EOperator.Equal, filter.TestId.Value));

            return filterList;
        }

        public async Task<IResult<Paged<Domain.Model.Question>>> SearchPaged(QuestionFilter filter, int skip, int take)
        {
            try
            {
                var result = await _dataSource.SearchPaged(GetFilters(filter), skip, take);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.Question>>.Error("Houve uma falha ao buscar os dados da(s) pergunta(s).", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.Question>>> Search(QuestionFilter filter)
        {
            try
            {
                var filters = GetFilters(filter);
                var result = await _dataSource.Search(filters);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.Question>>.Error("Houve uma falha ao buscar os dados da pergunta.", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.Question obj)
        {
            try
            {
                obj.CreatedAt = obj.UpdatedAt = DateTime.UtcNow;
                var result = await _dataSource.Insert(obj);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Houve uma falha ao cadastrar a pergunta.", ex);
            }
        }

        public async Task<IResult<bool>> Update(Domain.Model.Question obj)
        {
            try
            {
                if (!obj.Id.HasValue || obj.Id <= 0)
                    return DefaultResult<bool>.Error("Falta referenciar qual o id para editar a pergunta.", new Exception($"Update: newObj.Id == (null/0)"));

                var searchResult = await Search(new QuestionFilter { Id = obj.Id });
                if (!searchResult.Succeded || searchResult.Data == null)
                    return DefaultResult<bool>.Break("Pergunta não encontrada para atualização.");

                var oldObj = searchResult.Data.First();
                var newObj = (Domain.Model.Question)searchResult.Data.First().Clone();

                if (obj.ResponseTypeId != null && obj.ResponseTypeId > 0)
                    newObj.ResponseTypeId = obj.ResponseTypeId;
                if (!string.IsNullOrEmpty(obj.QuestionText))
                    newObj.QuestionText = obj.QuestionText;

                var filters = GetFilters(new QuestionFilter() { Id = obj.Id });
                return await _dataSource.Update(filters, oldObj, newObj);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Houve uma falha ao editar a pergunta.", ex);
            }
        }
    }
}