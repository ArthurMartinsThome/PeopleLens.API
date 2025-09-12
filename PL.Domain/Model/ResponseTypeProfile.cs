namespace PL.Domain.Model
{
    public class ResponseTypeProfile : ICloneable
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public object Clone() => this.MemberwiseClone();
    }
}