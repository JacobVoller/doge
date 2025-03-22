
namespace DogeServer.Models
{
    public abstract class StructureEntitity : Entity
    {
        public string? Type { get; set; }
        public string? Label { get; set; }
        public string? LabelLevel { get; set; }
        public string? LabelDescription { get; set; }
        public string? Identifier { get; set; }
        public string? SectionRange { get; set; }
    }
}
