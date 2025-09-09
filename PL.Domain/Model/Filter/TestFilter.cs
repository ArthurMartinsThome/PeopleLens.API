namespace PL.Domain.Model.Filter
{
    public class TestFilter
    {
        public int? Id { get; set; }
        public int? TestTypeId { get; set; }
        public int? StatusId { get; set; }
        public string? Title { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public bool HideInactive { get; set; }
        public bool HideDeleted { get; set; }
    }
}