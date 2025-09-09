namespace PL.Domain.Model.Filter
{
    public class CompanyTestFilter
    {
        public int? Id { get; set; }
        public int? CompanyId { get; set; }
        public int? TestId { get; set; }
        public DateTime? AssociationDateFrom { get; set; }
        public DateTime? AssociationDateTo { get; set; }
    }
}