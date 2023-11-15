using System.Web;
using Delivery.Api.BL.Facades;
using Delivery.Api.BL.Facades.Interfaces;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Common.Models.Dish;
using Delivery.Common.Models.Restaurant;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly ILogger<DishController> _logger;
        private readonly IRestaurantFacade _restaurantFacade;

        public RestaurantController(IRestaurantFacade restaurantFacade, ILogger<DishController> logger)
        {
            _logger = logger;
            _restaurantFacade = restaurantFacade;
        }

        [HttpGet]
        public IEnumerable<RestaurantListModel> GetAll()
        {
            return _restaurantFacade.GetAll();
        }

        [HttpGet("{id:guid}")]
        public ActionResult<RestaurantDetailModel> Get(Guid id)
        {
            var restaurant = _restaurantFacade.GetById(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return restaurant;
        }

        [HttpPost]
        public ActionResult<Guid> Create(RestaurantCreateModel restaurant)
        {
            return _restaurantFacade.Create(restaurant);
        }

        [HttpPatch]
        public ActionResult<Guid> Update(RestaurantCreateModel restaurant)
        {
            var updatedRestaurant = _restaurantFacade.Update(restaurant);
            if (updatedRestaurant == null)
            {
                return NotFound();
            }

            return updatedRestaurant;
        }

        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            if (_restaurantFacade.Delete(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
