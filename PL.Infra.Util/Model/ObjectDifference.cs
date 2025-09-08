namespace PL.Infra.Util.Model
{
    public class ObjectDifference
    {
        public string Path { get; set; } = null!;
        public object? ObjectId { get; set; }
        public object? OldValue { get; set; }
        public object? NewValue { get; set; }
    }

    /*
     public class ObjectDiference (Mapp)
    {
        public PropertyInfo Property { get; set; } = null!;
        public object? OldValue { get; set; }
        public object? NewValue { get; set; }
    }
     */
}