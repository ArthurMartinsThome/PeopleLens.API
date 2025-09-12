namespace PL.Domain.Dto.CreateTestDto
{
    /// <summary>
    /// DTO para a criação de uma opção de resposta de pergunta.
    /// </summary>
    public class QuestionResponseOptionCreateDto
    {
        public string Text { get; set; }
        public int ResponseTypeProfileId { get; set; }
        public int Weight { get; set; }
    }
}