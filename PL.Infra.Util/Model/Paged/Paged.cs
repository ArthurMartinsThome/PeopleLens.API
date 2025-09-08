namespace PL.Infra.Util.Model.Paged
{
    public class Paged<T> where T : class
    {
        public readonly int Page;
        public readonly int PageCount;
        public readonly int ItemsPerPage;
        public readonly int TotalCount;
        public readonly string ScrollId;
        public readonly IEnumerable<T> Items;
        public IEnumerable<Facet>? Facets;

        public Paged(IEnumerable<T> items, int skip, int take, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
            ItemsPerPage = take;
            Page = take > 0 ? skip / take + 1 : 1;
            PageCount = take > 0 ?
             int.Parse(Math.Ceiling(totalCount /
             Convert.ToDecimal(take)).ToString()) : 1;
            ScrollId = default;
        }
        public Paged(IEnumerable<T> items, int skip, int take, int totalCount, string scrollId)
        {
            Items = items;
            TotalCount = totalCount;
            ItemsPerPage = take;
            Page = take > 0 ? skip / take + 1 : 1;
            PageCount = take > 0 ?
             int.Parse(Math.Ceiling(totalCount /
             Convert.ToDecimal(take)).ToString()) : 1;
            ScrollId = scrollId;
        }

        public Paged()
        {
            Items = new T[0];
            Page = 1;
            PageCount = 1;
            ItemsPerPage = 0;
            TotalCount = 0;
            ScrollId = default;
        }
    }
}