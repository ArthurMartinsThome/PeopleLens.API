namespace PL.Domain.Model.Filter
{
    public class PersonCompanyFilter
    {
        public int? Id { get; set; }
        public int? PersonId { get; set; }
        public int? CompanyId { get; set; }
        public int? StatusId { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
    }
}