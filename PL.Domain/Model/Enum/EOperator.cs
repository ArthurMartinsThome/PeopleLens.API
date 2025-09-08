namespace PL.Domain.Model.Enum
{
    public enum EOperator
    {
        Equal,
        NotEqual,
        Like,
        Fuzzy,
        In,
        NotIn,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual,
        IsNull,
        IsNotNull,
        Between,
        StartsWith,
        GeoShapeContainsPoint,
        QueryString,
        Semantic
    }
}