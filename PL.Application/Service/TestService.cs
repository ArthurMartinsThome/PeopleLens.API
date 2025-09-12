using PL.Adapter.PostgreSQL.Interface;
using PL.Application.Interface;
using PL.Domain.Dto.CreateTestDto;
using PL.Domain.Dto.TestFull;
using PL.Domain.Model;
using PL.Domain.Model.Enum;
using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util.Model.Paged;
using System.Net;

namespace PL.Application.Service
{
    public class TestService : ITestService
    {
        private readonly ITestDataSource _dataSource;
        private readonly ITestQuestionService _testQuestionService;
        private readonly IQuestionService _questionService;
        private readonly IQuestionResponseOptionService _questionResponseOptionService;
        private readonly IQuestionConfigurationService _questionConfigurationService;
        private readonly IKeyConfigurationQuestionService _keyConfigurationQuestionService;

        public TestService(
            ITestDataSource dataSource,
            IQuestionService questionService,
            ITestQuestionService testQuestionService,
            IQuestionResponseOptionService questionResponseOptionService,
            IQuestionConfigurationService questionConfigurationService,
            IKeyConfigurationQuestionService keyConfigurationQuestionService)
        {
            _dataSource = dataSource;
            _questionService = questionService;
            _testQuestionService = testQuestionService;
            _questionResponseOptionService = questionResponseOptionService;
            _questionConfigurationService = questionConfigurationService;
            _keyConfigurationQuestionService = keyConfigurationQuestionService;
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
                // Busca o teste pelo Id.
                var resultSearchTest = await Search(new TestFilter { Id = testId });

                if (!resultSearchTest.Succeded || !resultSearchTest.HasData || resultSearchTest.StatusCode != HttpStatusCode.OK)
                    return DefaultResult<TestFullResponseDto>.Break("Teste não encontrado.");

                var test = resultSearchTest.Data.First();

                // Busca os vínculos teste-questão para o TestId.
                var resultSearchTestQuestion = await _testQuestionService.Search(new TestQuestionFilter { TestId = test.Id });

                if (!resultSearchTestQuestion.Succeded || !resultSearchTestQuestion.HasData || resultSearchTestQuestion.StatusCode != HttpStatusCode.OK)
                    return DefaultResult<TestFullResponseDto>.Break("Vínculo teste - questões não encontradas.");

                var testQuestions = resultSearchTestQuestion.Data;
                var questionIds = testQuestions.Select(x => x.QuestionId.Value).ToList();

                // Busca todas as perguntas do teste.
                var resultSearchQuestion = await _questionService.Search(new QuestionFilter { Ids = questionIds });

                if (!resultSearchQuestion.Succeded || !resultSearchQuestion.HasData || resultSearchQuestion.StatusCode != HttpStatusCode.OK)
                    return DefaultResult<TestFullResponseDto>.Break("Questões do teste não encontradas.");

                var questions = resultSearchQuestion.Data;

                // Busca todas as configurações de perguntas de uma vez para otimização.
                var resultSearchQuestionConfig = await _questionConfigurationService.Search(new QuestionConfigurationFilter { QuestionIds = questionIds });
                var questionConfigurations = resultSearchQuestionConfig.Data ?? new List<Domain.Model.QuestionConfiguration>();

                // Busca todos os nomes de chaves de configuração de uma vez.
                var configKeyIds = questionConfigurations.Select(qc => qc.KeyConfigurationQuestionId.Value).Distinct().ToList();
                var resultSearchKeyConfig = await _keyConfigurationQuestionService.Search(new KeyConfigurationQuestionFilter { Ids = configKeyIds });
                var keyConfigurations = resultSearchKeyConfig.Data ?? new List<Domain.Model.KeyConfigurationQuestion>();

                // Busca todas as opções de resposta de uma vez para otimização.
                var resultSearchResponseOption = await _questionResponseOptionService.Search(new QuestionResponseOptionFilter { QuestionIds = questionIds });
                var questionResponseOptions = resultSearchResponseOption.Data ?? new List<Domain.Model.QuestionResponseOption>();

                var questionsDto = new List<TestFullQuestionDto>();

                // Popula os DTOs em memória com os dados já buscados.
                foreach (var question in questions)
                {
                    var responseOptions = questionResponseOptions
                        .Where(o => o.QuestionId == question.Id)
                        .Select(x => new TestFullQuestionResponseOptionDto
                        {
                            Id = x.Id.Value,
                            Text = x.Text,
                            Value = x.Weight
                        }).ToList();

                    var configurations = questionConfigurations
                        .Where(qc => qc.QuestionId == question.Id)
                        .Join(keyConfigurations,
                            qc => qc.KeyConfigurationQuestionId,
                            kc => kc.Id,
                            (qc, kc) => new TestFullQuestionConfigurationDto
                            {
                                KeyName = kc.KeyName,
                                Value = qc.Value
                            }).ToList();

                    questionsDto.Add(new TestFullQuestionDto
                    {
                        Id = question.Id.Value,
                        ResponseTypeId = (EResponseType)question.ResponseTypeId,
                        QuestionText = question.QuestionText,
                        QuestionResponseOptions = responseOptions,
                        Configurations = configurations
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

        public async Task<IResult<bool>> CreateTest(TestCreateRequestDto obj)
        {
            try
            {
                if (obj == null || obj.Test == null)
                    return DefaultResult<bool>.Break("Dados do teste não fornecidos.");

                // 1. Inserir o Teste
                var testDomain = new Test
                {
                    TestTypeId = obj.Test.TestTypeId,
                    Title = obj.Test.Title,
                    Description = obj.Test.Description
                };
                var resultTest = await Insert(testDomain);
                if (!resultTest.Succeded)
                    return DefaultResult<bool>.Break("Ocorreu um erro ao criar o teste.");

                var newTestId = resultTest.Data;

                if (obj.Questions != null && obj.Questions.Any())
                {
                    foreach (var questionDto in obj.Questions)
                    {
                        // 2. Inserir a Pergunta
                        var questionDomain = new Question
                        {
                            ResponseTypeId = (int)questionDto.ResponseTypeId,
                            QuestionText = questionDto.QuestionText,
                        };
                        var resultQuestion = await _questionService.Insert(questionDomain);
                        if (!resultQuestion.Succeded)
                            return DefaultResult<bool>.Break($"Ocorreu um erro ao criar a pergunta: {questionDto.QuestionText}");

                        var newQuestionId = resultQuestion.Data;

                        // 3. Criar o Vínculo entre Teste e Pergunta
                        var testQuestionDomain = new TestQuestion
                        {
                            TestId = newTestId,
                            QuestionId = newQuestionId,
                        };
                        var resultTestQuestion = await _testQuestionService.Insert(testQuestionDomain);
                        if (!resultTestQuestion.Succeded)
                            return DefaultResult<bool>.Break("Ocorreu um erro ao criar o vínculo entre o teste e a pergunta.");

                        // 4. Inserir as Opções de Resposta
                        if (questionDto.ResponseOptions != null && questionDto.ResponseOptions.Any())
                        {
                            foreach (var optionDto in questionDto.ResponseOptions)
                            {
                                var optionDomain = new QuestionResponseOption
                                {
                                    QuestionId = newQuestionId,
                                    Text = optionDto.Text,
                                    ResponseTypeProfileId = optionDto.ResponseTypeProfileId,
                                    Weight = optionDto.Weight
                                };
                                var resultOption = await _questionResponseOptionService.Insert(optionDomain);
                                if (!resultOption.Succeded)
                                    return DefaultResult<bool>.Break($"Ocorreu um erro ao criar a opção de resposta para a pergunta: {questionDto.QuestionText}");
                            }
                        }

                        // 5. Inserir as Configurações da Pergunta
                        if (questionDto.Configurations != null && questionDto.Configurations.Any())
                        {
                            foreach (var configDto in questionDto.Configurations)
                            {
                                var configDomain = new QuestionConfiguration
                                {
                                    QuestionId = newQuestionId,
                                    KeyConfigurationQuestionId = configDto.KeyConfigurationQuestionId,
                                    Value = configDto.Value
                                };
                                var resultConfig = await _questionConfigurationService.Insert(configDomain);
                                if (!resultConfig.Succeded)
                                    return DefaultResult<bool>.Break($"Ocorreu um erro ao criar a configuração para a pergunta: {questionDto.QuestionText}");
                            }
                        }
                    }
                }

                return DefaultResult<bool>.Create(true);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Houve uma falha ao criar o teste.", ex);
            }
        }
    }
}