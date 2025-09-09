using PL.Adapter.PostgreSQL.Interface;
using PL.Application.Interface;
using PL.Domain.Dto.TestFull;
using PL.Domain.Model.Enum;
using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util.Model.Paged;

namespace PL.Application.Service
{
    public class TestService : ITestService
    {
        private readonly ITestDataSource _dataSource;
        private readonly ITestQuestionService _testQuestionService;
        private readonly IQuestionService _questionService;
        private readonly IQuestionResponseOptionService _questionResponseOptionService;

        public TestService(
            ITestDataSource dataSource,
            IQuestionService questionService,
            ITestQuestionService testQuestionService,
            IQuestionResponseOptionService questionResponseOptionService)
        {
            _dataSource = dataSource;
            _questionService = questionService;
            _testQuestionService = testQuestionService;
            _questionResponseOptionService = questionResponseOptionService;
        }

        private IEnumerable<Filter> GetFilters(TestFilter filter)
        {
            var filterList = new List<Filter>();
            if (filter == null)
                return filterList;
            if (filter.Id.HasValue && filter.Id > 0)
                filterList.Add(Filter.Create("Id", EOperator.Equal, new[] { filter.Id.Value }));
            if (filter.TestTypeId.HasValue && filter.TestTypeId > 0)
                filterList.Add(Filter.Create("TestTypeId", EOperator.Equal, filter.TestTypeId.Value));
            if (filter.StatusId.HasValue && filter.StatusId > 0)
                filterList.Add(Filter.Create("StatusId", EOperator.Equal, filter.StatusId.Value));
            if (!string.IsNullOrEmpty(filter.Title))
                filterList.Add(Filter.Create("Title", EOperator.Equal, filter.Title));
            if (filter.CreatedFrom != null)
                filterList.Add(Filter.Create("CreatedAt", EOperator.GreaterThanOrEqual, filter.CreatedFrom));
            if (filter.CreatedTo != null)
                filterList.Add(Filter.Create("CreatedAt", EOperator.LessThanOrEqual, filter.CreatedTo));
            if (filter.HideInactive)
                filterList.Add(Filter.Create("StatusId", EOperator.NotEqual, new[] { EStatus.Inativo }));
            if (filter.HideDeleted)
                filterList.Add(Filter.Create("StatusId", EOperator.NotEqual, new[] { EStatus.Excluido }));
            return filterList;
        }

        public async Task<IResult<Paged<Domain.Model.Test>>> SearchPaged(TestFilter filter, int skip, int take)
        {
            try
            {
                var result = await _dataSource.SearchPaged(GetFilters(filter), skip, take);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.Test>>.Error("Houve uma falha ao buscar os dados do(s) teste(s).", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.Test>>> Search(TestFilter filter)
        {
            try
            {
                var filters = GetFilters(filter);
                var result = await _dataSource.Search(filters);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.Test>>.Error("Houve uma falha ao buscar os dados do teste.", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.Test obj)
        {
            try
            {
                var result = await _dataSource.Insert(obj);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Houve uma falha ao cadastrar o teste.", ex);
            }
        }

        public async Task<IResult<bool>> Update(Domain.Model.Test obj)
        {
            try
            {
                if (!obj.Id.HasValue || obj.Id <= 0)
                    return DefaultResult<bool>.Error("Falta referenciar qual o id para editar o teste.", new Exception($"Update: newObj.Id == (null/0)"));

                var searchResult = await Search(new TestFilter { Id = obj.Id });
                if (!searchResult.Succeded || searchResult.Data == null)
                    return DefaultResult<bool>.Break("Teste não encontrada para atualização.");

                var oldObj = searchResult.Data.First();
                var newObj = (Domain.Model.Test)searchResult.Data.First().Clone();

                if (obj.TestTypeId != null && obj.TestTypeId > 0)
                    newObj.TestTypeId = obj.TestTypeId;
                if (!string.IsNullOrEmpty(obj.Title))
                    newObj.Title = obj.Title;
                if (!string.IsNullOrEmpty(obj.Description))
                    newObj.Description = obj.Description;

                var filters = GetFilters(new TestFilter() { Id = obj.Id });
                return await _dataSource.Update(filters, oldObj, newObj);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Houve uma falha ao editar o teste.", ex);
            }
        }

        public async Task<IResult<TestFullResponseDto>> GetTestFull(int testId)
        {
            try
            {
                // Pegar o teste pelo Id
                var resultSearchTest = await Search(new TestFilter { Id = testId });

                if(!resultSearchTest.Succeded || !resultSearchTest.HasData || resultSearchTest.StatusCode != System.Net.HttpStatusCode.OK)
                    return DefaultResult<TestFullResponseDto>.Break("Teste não encontrado.");

                var test = resultSearchTest.Data.First();

                // Pegar os testQuestions pelo TestId
                var resultSearchTestQuestion = await _testQuestionService.Search(new TestQuestionFilter { TestId = test.Id });

                if (!resultSearchTestQuestion.Succeded || !resultSearchTestQuestion.HasData || resultSearchTestQuestion.StatusCode != System.Net.HttpStatusCode.OK)
                    return DefaultResult<TestFullResponseDto>.Break("Vinculo teste - questões não encontradas.");

                var testQuestions = resultSearchTestQuestion.Data;

                // Pegar as questions pelo QuestionId
                var resultSearchQuestion = await _questionService.Search(new QuestionFilter { Ids = testQuestions.Select(x => x.QuestionId.Value).ToList() });

                if (!resultSearchQuestion.Succeded || !resultSearchQuestion.HasData || resultSearchQuestion.StatusCode != System.Net.HttpStatusCode.OK)
                    return DefaultResult<TestFullResponseDto>.Break("Questões do teste não encontradas.");

                var questions = resultSearchQuestion.Data;
                var questionsDto = new List<TestFullQuestionDto>();

                // Pegar as question_response_option pelo QuestionId
                foreach (var question in questions)
                {
                    var resultSearchQuestionResponseOption = await _questionResponseOptionService.Search(new QuestionResponseOptionFilter { QuestionId = question.Id });
                    if (!resultSearchQuestionResponseOption.Succeded || !resultSearchQuestionResponseOption.HasData || resultSearchQuestionResponseOption.StatusCode != System.Net.HttpStatusCode.OK)
                        return DefaultResult<TestFullResponseDto>.Break("Opções de resposta das questões do teste não encontradas.");

                    questionsDto.Add(new TestFullQuestionDto
                    {
                        Id = question.Id.Value,
                        ResponseTypeId = (EResponseType)question.ResponseTypeId,
                        QuestionText = question.QuestionText,
                        QuestionResponseOptions = resultSearchQuestionResponseOption.Data.Select(x => new TestFullQuestionResponseOptionDto
                        {
                            Id = x.Id.Value,
                            Text = x.Text,
                            Value = x.Value
                        }).ToList()
                    });
                }

                return DefaultResult<TestFullResponseDto>.Create(new TestFullResponseDto()
                {
                    Test = new TestFullTestDto
                    {
                        Id = test.Id.Value,
                        TestTypeId = test.TestTypeId,
                        Title = test.Title,
                        Description = test.Description
                    },
                    Questions = questionsDto
                });
            }
            catch (Exception ex)
            {
                return DefaultResult<TestFullResponseDto>.Error("Houve uma falha ao buscar os dados do teste.", ex);
            }
        }
    }
}