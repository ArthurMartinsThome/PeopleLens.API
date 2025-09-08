using PL.Domain.Model.Enum;

namespace PL.Infra.Model.Filter
{
    public class ParsedFilter
    {
        public readonly IEnumerable<string> _Fields;
        public readonly string _NestedFieldName;
        public readonly string _Alias;
        public readonly string _OrGroup;
        public readonly EOperator _Operator;
        public IEnumerable<object>? _Values { get; set; }

        public ParsedFilter(string fieldName, EOperator @operator, params object[] values)
        {
            _Fields = [fieldName];
            _Operator = @operator;
            _Values = values;
        }
        public ParsedFilter(string alias, string fieldName, EOperator @operator, params object[] values)
        {
            _Fields = [fieldName];
            _Operator = @operator;
            _Values = values;
            _Alias = alias;
        }
        public ParsedFilter(string alias, string fieldName, string nestedFieldName, EOperator @operator, params object[] values)
        {
            _Fields = [fieldName];
            _Operator = @operator;
            _Values = values;
            _Alias = alias;
            _NestedFieldName = nestedFieldName;
        }

        public ParsedFilter(string fieldName, string nestedFieldName, EOperator @operator, object[] values, string? orGroup = default, string? alias = default)
        {
            _Fields = [fieldName];
            _Operator = @operator;
            _Values = values;
            _OrGroup = orGroup;
            _Alias = alias;
            _NestedFieldName = nestedFieldName;
        }
        public ParsedFilter(IEnumerable<string> fields, string nestedFieldName, EOperator @operator, object[] values, string? orGroup = default, string? alias = default)
        {
            _Fields = fields;
            _Operator = @operator;
            _Values = values;
            _OrGroup = orGroup;
            _Alias = alias;
            _NestedFieldName = nestedFieldName;
        }
    }
}