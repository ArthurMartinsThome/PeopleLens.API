namespace PL.Domain.Dto.CreateTestDto
{
    /// <summary>
    /// DTO de requisição para criar um teste completo.
    /// </summary>
    public class TestCreateRequestDto
    {
        public TestDto Test { get; set; }
        public IEnumerable<QuestionCreateDto> Questions { get; set; }
    }
}