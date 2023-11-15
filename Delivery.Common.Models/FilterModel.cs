using Delivery.Common.Enums;

namespace Delivery.Common.Models
{
    public record FilterModel : IWithId
    {
        public Guid Id { get; init; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Image { get; set; }
        public required FilterType Type { get; set; }
    }
}
