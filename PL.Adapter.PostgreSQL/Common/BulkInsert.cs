using System.Collections;
using System.Globalization;
using System.Reflection;

namespace PL.Adapter.PostgreSQL.Common
{
    internal static class BulkInsert
    {
        public static string GenerateString<T>(string tableName, IEnumerable<T> data)
        {
            var properties = typeof(T).GetProperties();
            var selectedProperties = new List<PropertyInfo>();
            foreach (var prop in properties)
            {
                if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType) && !prop.PropertyType.IsPrimitive && !typeof(string).IsAssignableFrom(prop.PropertyType))
                    continue;
                selectedProperties.Add(prop);
            }
            var registryList = new List<string>();
            foreach (var item in data)
            {
                var valueList = new List<string>();
                foreach (var prop in selectedProperties)
                {
                    var value = prop.GetValue(item);
                    if (value != null)
                    {
                        if (typeof(string).IsAssignableFrom(prop.PropertyType))
                            valueList.Add($"'{Convert.ToString(value, CultureInfo.InvariantCulture).Replace("'", "''").Replace("\\", "\\\\")}'");
                        else if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
                            valueList.Add($"'{(value as DateTime?).Value.ToString("yyyy-MM-dd HH:mm:ss")}'");
                        else
                            valueList.Add(Convert.ToString(value, CultureInfo.InvariantCulture));
                    }
                    else
                        valueList.Add("null");
                }

                registryList.Add($"({string.Join(",", valueList)})");
            }

            var fieldNames = string.Join(",", selectedProperties.Select(x => x.Name).ToArray());
            var values = string.Join(",", registryList.ToArray());

            var result = $"INSERT INTO {tableName} ({fieldNames}) VALUES {values}";
            return result;
        }
    }
}