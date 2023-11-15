using System;

namespace Delivery.Common
{
    public interface IWithId
    {
        Guid Id { get; init; }
    }
}
