using PL.Domain.Model.Enum;
using PL.Infra.Model.Filter;

namespace PL.Adapter.PostgreSQL.Common
{
    internal class FilterHandler
    {
        public readonly string SqlFilters;
        public readonly Dictionary<string, object> Parameters;

        public FilterHandler(IEnumerable<ParsedFilter> filters)
        {
            Parameters = new Dictionary<string, object>();

            var listFiterSql = new List<string>();
            var index = 0;
            var groupedFilters = filters.GroupBy(x => x._OrGroup);
            foreach (var filterGroup in groupedFilters)
            {
                var tempFilterSql = new List<string>();
                foreach (var item in filterGroup)
                {
                    string fieldName = item._Fields.ElementAt(0);
                    if (!string.IsNullOrEmpty(item._NestedFieldName))
                        fieldName = item._NestedFieldName;
                    string left;
                    if (string.IsNullOrEmpty(item._Alias))
                        left = $"\"{fieldName}\"";
                    else
                        left = $"\"{item._Alias}\".\"{fieldName}\"";
                    index++;
                    switch (item._Operator)
                    {
                        case EOperator.Equal:
                            tempFilterSql.Add($"{left} = @{item._Fields.ElementAt(0)}{index}");
                            Parameters.Add($"{item._Fields.ElementAt(0)}{index}", item._Values.ElementAt(0));
                            break;
                        case EOperator.Like:
                            tempFilterSql.Add($"{left} like @{item._Fields.ElementAt(0)}{index}");
                            Parameters.Add($"{item._Fields.ElementAt(0)}{index}", $"%{item._Values.ElementAt(0)}%");
                            break;
                        case EOperator.StartsWith:
                            tempFilterSql.Add($"{left} like @{item._Fields.ElementAt(0)}{index}");
                            Parameters.Add($"{item._Fields.ElementAt(0)}{index}", $"{item._Values.ElementAt(0)}%");
                            break;
                        case EOperator.In:
                            tempFilterSql.Add($"{left} IN @{item._Fields.ElementAt(0)}{index}");
                            Parameters.Add($"{item._Fields.ElementAt(0)}{index}", item._Values);
                            break;
                        case EOperator.NotEqual:
                            tempFilterSql.Add($"{left} <> @{item._Fields.ElementAt(0)}{index}");
                            Parameters.Add($"{item._Fields.ElementAt(0)}{index}", item._Values.ElementAt(0));
                            break;
                        case EOperator.NotIn:
                            tempFilterSql.Add($"{left} NOT IN @{item._Fields.ElementAt(0)}{index}");
                            Parameters.Add($"{item._Fields.ElementAt(0)}{index}", item._Values);
                            break;
                        case EOperator.GreaterThan:
                            tempFilterSql.Add($"{left} > @{item._Fields.ElementAt(0)}{index}");
                            Parameters.Add($"{item._Fields.ElementAt(0)}{index}", item._Values.ElementAt(0));
                            break;
                        case EOperator.GreaterThanOrEqual:
                            tempFilterSql.Add($"{left} >= @{item._Fields.ElementAt(0)}{index}");
                            Parameters.Add($"{item._Fields.ElementAt(0)}{index}", item._Values.ElementAt(0));
                            break;
                        case EOperator.LessThan:
                            tempFilterSql.Add($"{left} < @{item._Fields.ElementAt(0)}{index}");
                            Parameters.Add($"{item._Fields.ElementAt(0)}{index}", item._Values.ElementAt(0));
                            break;
                        case EOperator.LessThanOrEqual:
                            tempFilterSql.Add($"{left} <= @{item._Fields.ElementAt(0)}{index}");
                            Parameters.Add($"{item._Fields.ElementAt(0)}{index}", item._Values.ElementAt(0));
                            break;
                        case EOperator.IsNull:
                            tempFilterSql.Add($"{left} is null");
                            break;
                        case EOperator.IsNotNull:
                            tempFilterSql.Add($"{left} is not null");
                            break;
                        case EOperator.Between:
                            tempFilterSql.Add($"{left} between @{item._Fields.ElementAt(0)}{index}s and @{item._Fields.ElementAt(0)}{index}e");
                            Parameters.Add($"{item._Fields.ElementAt(0)}{index}s", item._Values.ElementAt(0));
                            Parameters.Add($"{item._Fields.ElementAt(0)}{index}e", item._Values.ElementAt(1));
                            break;
                        default: throw new Exception("Operator not suported");
                    }
                }

                if (string.IsNullOrEmpty(filterGroup.Key))
                    listFiterSql.AddRange(tempFilterSql);
                else
                {
                    listFiterSql.Add($"({string.Join(" OR ", tempFilterSql)})");
                }
            }

            SqlFilters = string.Join(" AND ", listFiterSql);
        }
    }
}