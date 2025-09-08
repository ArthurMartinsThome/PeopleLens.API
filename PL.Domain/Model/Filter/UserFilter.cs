namespace PL.Domain.Model.Filter
{
    public class UserFilter
    {
        public int? Id { get; set; }
        public List<int>? Ids { get; set; }
        public string? Name { get; set; }
        public string? Cellphone { get; set; }
    }
}