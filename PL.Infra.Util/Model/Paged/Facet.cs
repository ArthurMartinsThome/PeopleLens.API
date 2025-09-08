namespace PL.Infra.Util.Model.Paged
{
    public class Facet
    {
        public string? Name { get; set; }
        public IEnumerable<FacetBucket> Buckets { get; set; }
    }
}