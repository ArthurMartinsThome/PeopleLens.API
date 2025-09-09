namespace PL.Domain.Model
{
    public class KeyConfigurationQuestion : ICloneable
    {
        public int? Id { get; set; }
        public string? KeyName { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public object Clone() => this.MemberwiseClone();
    }
}