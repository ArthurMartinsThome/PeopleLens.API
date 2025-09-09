namespace PL.Domain.Model.Filter
{
    public class TestTypeFilter
    {
        public int? Id { get; set; }
        public int? StatusId { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public bool HideInactive { get; set; }
        public bool HideDeleted { get; set; }
    }
}