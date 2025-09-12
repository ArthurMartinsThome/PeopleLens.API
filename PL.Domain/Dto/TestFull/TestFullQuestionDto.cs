using PL.Domain.Model;
using PL.Domain.Model.Enum;

namespace PL.Domain.Dto.TestFull
{
    /// <summary>
    /// DTO que representa uma pergunta completa com suas opções e configurações.
    /// </summary>
    public class TestFullQuestionDto
    {
        public int Id { get; set; }
        public EResponseType ResponseTypeId { get; set; }
        public string QuestionText { get; set; }
        public IEnumerable<TestFullQuestionResponseOptionDto> QuestionResponseOptions { get; set; }
        public IEnumerable<TestFullQuestionConfigurationDto> Configurations { get; set; }
    }
}