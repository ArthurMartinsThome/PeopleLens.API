namespace PL.Infra.Util
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FilterIdentifierAttribute : System.Attribute
    {
        public readonly string FilterPropertyName;
        public readonly string Alias;
        public readonly string NestedPropertyName;
        public FilterIdentifierAttribute(string filterPropertyName, string alias = null, string nestedPropertyName = null)
        {
            NestedPropertyName = nestedPropertyName;
            FilterPropertyName = filterPropertyName;
            Alias = alias;
        }
    }
}