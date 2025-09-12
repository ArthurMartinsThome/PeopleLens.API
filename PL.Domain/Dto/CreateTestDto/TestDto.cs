namespace PL.Domain.Dto.CreateTestDto
{
    /// <summary>
    /// DTO que representa os dados de um teste.
    /// </summary>
    public class TestDto
    {
        public int? Id { get; set; }
        public int TestTypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}