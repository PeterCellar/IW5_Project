using Delivery.Api.BL.Facades;
using Delivery.Common.Models.Restaurant;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RevenueController : ControllerBase
    {
        private readonly RestaurantFacade _restaurantFacade;
        private readonly ILogger<RevenueController> _logger;

        public RevenueController(RestaurantFacade restaurantFacade, ILogger<RevenueController> logger)
        {
            _restaurantFacade = restaurantFacade;
            _logger = logger;
        }

        [HttpGet("{restaurantId:guid}")]
        public ActionResult<decimal> GetRestaurantRevenue(Guid restaurantId)
        {
            var restaurant = _restaurantFacade.GetById(restaurantId);
            if(restaurant == null)
            {
                return NotFound();
            }

            return restaurant.Revenue ?? Convert.ToDecimal(0);
        }
    }
}
