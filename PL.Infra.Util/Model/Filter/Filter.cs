using System.Collections;
using PL.Domain.Model.Enum;

namespace PL.Infra.Model.Filter
{
    public class Filter
    {
        public readonly IEnumerable<string> _Fields;
        public readonly string _Alias;
        public readonly string _OrGroup;
        public readonly EOperator _Operator;
        public IEnumerable<object>? _Values { get; set; }

        private Filter(EOperator @operator, IEnumerable<object> values, string fieldName, string? alias = null, string? orGroup = null)
        {
            _Fields = [fieldName];
            _Operator = @operator;
            _Values = values;
            _Alias = alias;
            _OrGroup = orGroup;
        }
        private Filter(EOperator @operator, IEnumerable<object> values, IEnumerable<string> fields, string? alias = null, string? orGroup = null)
        {
            _Fields = fields;
            _Operator = @operator;
            _Values = values;
            _Alias = alias;
            _OrGroup = orGroup;
        }
        public static Filter Create<T>(string fieldName, EOperator @operator, T value, string? orGroup = default, string? alias = default)
        {
            var type = typeof(T);
            if (typeof(IEnumerable).IsAssignableFrom(typeof(T)) && type != typeof(string))
                return new Filter(@operator: @operator, ((IEnumerable)value).Cast<object>(), fieldName, alias, orGroup);
            else
                return new Filter(@operator: @operator, new object[] { value }, fieldName, alias, orGroup);
        }

        public static Filter Create<T>(IEnumerable<string> fields, EOperator @operator, T value, string? orGroup = default, string? alias = default)
        {
            var type = typeof(T);
            if (typeof(IEnumerable).IsAssignableFrom(typeof(T)) && type != typeof(string))
                return new Filter(@operator: @operator, ((IEnumerable)value).Cast<object>(), fields, alias, orGroup);
            else
                return new Filter(@operator: @operator, new object[] { value }, fields, alias, orGroup);
        }
    }
}