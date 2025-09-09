namespace PL.Domain.Model.Filter
{
    public class QuestionFilter
    {
        public int? Id { get; set; }
        public List<int>? Ids { get; set; }
        public string? Text { get; set; }
        public int? ResponseTypeId { get; set; }
        public int? TestId { get; set; }
    }
}