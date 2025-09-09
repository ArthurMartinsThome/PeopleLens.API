using PL.Domain.Model;
using PL.Domain.Model.Enum;

namespace PL.Domain.Dto.TestFull
{
    public class TestFullQuestionDto
    {
        public int? Id { get; set; }
        public EResponseType? ResponseTypeId { get; set; }
        public string? QuestionText { get; set; }
        public List<TestFullQuestionResponseOptionDto> QuestionResponseOptions { get; set; }
    }
}