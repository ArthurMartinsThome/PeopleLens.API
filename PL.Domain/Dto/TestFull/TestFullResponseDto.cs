using PL.Domain.Model;

namespace PL.Domain.Dto.TestFull
{
    public class TestFullResponseDto
    {
        public TestFullTestDto Test { get; set; }
        public List<TestFullQuestionDto> Questions { get; set; }
    }
}