using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using PL.Infra.Model.Filter;

namespace PL.Infra.Util
{
    public static class FilterParser
    {
        private static ConcurrentDictionary<Type, IEnumerable<PropertyInfo>> _MetaData = new ConcurrentDictionary<Type, IEnumerable<PropertyInfo>>();

        public static IEnumerable<ParsedFilter> Parse<T>(IEnumerable<Filter> filters)
        {
            var metadata = GetMetadata<T>();

            var alias = string.Empty;
            var tableAttribute = typeof(T).GetCustomAttribute<TableAttribute>();
            if (tableAttribute != null)
                alias = tableAttribute.Name;

            var parsedFilters = new List<ParsedFilter>();
            foreach (var item in filters)
            {
                var filterIdentifier = default(FilterIdentifierAttribute);
                var propertyList = new List<PropertyInfo>();
                foreach (var property in metadata)
                {
                    filterIdentifier = property.GetCustomAttribute<FilterIdentifierAttribute>();
                    if (filterIdentifier == null)
                        continue;

                    if (item._Fields.Any(x => filterIdentifier.FilterPropertyName == x))
                    {
                        propertyList.Add(property);
                        break;
                    }
                }

                if (propertyList.Any())
                {
                    if (item._Fields.Count() > 1)
                        parsedFilters.Add(new ParsedFilter(propertyList.Select(x => x.Name).ToArray(), null, item._Operator, item._Values.ToArray(), null, null));
                    else
                    {
                        if (!string.IsNullOrEmpty(filterIdentifier.Alias))
                            alias = filterIdentifier.Alias;

                        parsedFilters.Add(new ParsedFilter(propertyList.ElementAt(0).Name, filterIdentifier.NestedPropertyName, item._Operator, item._Values.ToArray(), item._OrGroup, alias));
                    }
                }
                else
                    throw new Exception($"There is no property with attribute [FilterIdentifier(\"{item._Fields.ElementAt(0)}\")]");

            }
            return parsedFilters;
        }

        private static IEnumerable<PropertyInfo> GetMetadata<T>()
        {
            var type = typeof(T);
            if (_MetaData.ContainsKey(type))
                return _MetaData[type];

            var metadata = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            _MetaData.TryAdd(type, metadata);
            return metadata;
        }
    }
}