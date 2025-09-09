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
    public class TestQuestionService : ITestQuestionService
    {
        private readonly ITestQuestionDataSource _dataSource;

        public TestQuestionService(ITestQuestionDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        private IEnumerable<Filter> GetFilters(TestQuestionFilter filter)
        {
            var filterList = new List<Filter>();
            if (filter == null)
                return filterList;
            if (filter.Id.HasValue && filter.Id > 0)
                filterList.Add(Filter.Create("Id", EOperator.Equal, new[] { filter.Id.Value }));
            if (filter.TestId.HasValue && filter.TestId > 0)
                filterList.Add(Filter.Create("TestId", EOperator.Equal, filter.TestId.Value));
            if (filter.QuestionId.HasValue && filter.QuestionId > 0)
                filterList.Add(Filter.Create("QuestionId", EOperator.Equal, filter.QuestionId.Value));
            return filterList;
        }

        public async Task<IResult<Paged<Domain.Model.TestQuestion>>> SearchPaged(TestQuestionFilter filter, int skip, int take)
        {
            try
            {
                var result = await _dataSource.SearchPaged(GetFilters(filter), skip, take);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.TestQuestion>>.Error("Houve uma falha ao buscar os dados do(s) teste(s) de questão.", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.TestQuestion>>> Search(TestQuestionFilter filter)
        {
            try
            {
                var filters = GetFilters(filter);
                var result = await _dataSource.Search(filters);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.TestQuestion>>.Error("Houve uma falha ao buscar os dados do teste de questão.", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.TestQuestion obj)
        {
            try
            {
                var result = await _dataSource.Insert(obj);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Houve uma falha ao cadastrar o teste de questão.", ex);
            }
        }

        public async Task<IResult<bool>> Update(Domain.Model.TestQuestion obj)
        {
            try
            {
                if (!obj.Id.HasValue || obj.Id <= 0)
                    return DefaultResult<bool>.Error("Falta referenciar qual o id para editar o teste de questão.", new Exception($"Update: newObj.Id == (null/0)"));

                var searchResult = await Search(new TestQuestionFilter { Id = obj.Id });
                if (!searchResult.Succeded || searchResult.Data == null)
                    return DefaultResult<bool>.Break("Empresa não encontrada para atualização.");

                var oldObj = searchResult.Data.First();
                var newObj = (Domain.Model.TestQuestion)searchResult.Data.First().Clone();

                if (obj.TestId != null && obj.TestId > 0)
                    newObj.TestId = obj.TestId;
                if (obj.QuestionId != null && obj.QuestionId > 0)
                    newObj.QuestionId = obj.QuestionId;

                var filters = GetFilters(new TestQuestionFilter() { Id = obj.Id });
                return await _dataSource.Update(filters, oldObj, newObj);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Houve uma falha ao editar o teste de questão.", ex);
            }
        }
    }
}