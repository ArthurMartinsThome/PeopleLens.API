namespace PL.Domain.Model.Filter
{
    public class CompanyFilter
    {
        public int? Id { get; set; }
        public string? CompanyName { get; set; }
        public string? CNPJ { get; set; }
        public int? StatusId { get; set; }
    }
}