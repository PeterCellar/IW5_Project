using System.ComponentModel.DataAnnotations;
using Delivery.Common;

namespace Delivery.Common.Models.OrderDish
{
    public record OrderDishBaseModel : IWithId
    {
        public Guid Id { get; init; }

        [Range(0, int.MaxValue)]
        public required int Amount { get; set; }
    }
}
