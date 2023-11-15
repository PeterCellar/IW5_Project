using Delivery.Common.BL.Facades;
using Delivery.Common.Models;

namespace Delivery.Api.BL.Facades.Interfaces
{
    public interface IFilterFacade : IAppFacade
    {
        List<FilterModel> GetBySubstring(string substring);
    }
}
