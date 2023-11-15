using System.ComponentModel.DataAnnotations;
using Delivery.Common;

namespace Delivery.Common.Models.DishAllergen
{
    public record DishAllergenBaseModel : IWithId
    {
        public Guid Id { get; init; }
    }
}
