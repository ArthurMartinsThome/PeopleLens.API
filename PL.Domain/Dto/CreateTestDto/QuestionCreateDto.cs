using PL.Domain.Model.Enum;

namespace PL.Domain.Dto.CreateTestDto
{
    /// <summary>
    /// DTO para a criação de uma pergunta.
    /// </summary>
    public class QuestionCreateDto
    {
        public EResponseType ResponseTypeId { get; set; }
        public string QuestionText { get; set; }
        public IEnumerable<QuestionResponseOptionCreateDto> ResponseOptions { get; set; }
        public IEnumerable<QuestionConfigurationCreateDto> Configurations { get; set; }
    }
}