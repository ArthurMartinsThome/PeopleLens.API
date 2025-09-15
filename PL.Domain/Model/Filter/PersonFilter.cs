namespace PL.Domain.Model.Filter
{
    public class PersonFilter
    {
        public int? Id { get; set; }
        public List<int>? Ids { get; set; }
        public string? Name { get; set; }
        public DateTime? BirthDateFrom { get; set; }
        public DateTime? BirthDateTo { get; set; }
        public string? Type { get; set; }
    }
}