namespace PL.Domain.Model.Filter
{
    public class TestAppliedFilter
    {
        public int? Id { get; set; }
        public int? CompanyTestId { get; set; }
        public int? PersonId { get; set; }
        public int? StatusId { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public bool HideInactive { get; set; }
        public bool HideDeleted { get; set; }
    }
}