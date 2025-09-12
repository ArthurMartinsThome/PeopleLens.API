namespace PL.Domain.Model.Filter
{
    public class QuestionResponseOptionFilter
    {
        public int? Id { get; set; }
        public int? QuestionId { get; set; }
        public List<int>? QuestionIds { get; set; }
        public int? ResponseTypeProfileId { get; set; }
        public int? Weight { get; set; }
    }
}