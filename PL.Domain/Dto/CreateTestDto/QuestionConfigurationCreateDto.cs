namespace PL.Domain.Dto.CreateTestDto
{
    /// <summary>
    /// DTO para a criação de uma configuração de pergunta.
    /// </summary>
    public class QuestionConfigurationCreateDto
    {
        public int KeyConfigurationQuestionId { get; set; }
        public string Value { get; set; }
    }
}