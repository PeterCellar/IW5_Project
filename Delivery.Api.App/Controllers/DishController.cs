using Delivery.Api.BL.Facades;
using Delivery.Api.BL.Facades.Interfaces;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Common.Models.Dish;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DishController : ControllerBase
    {
        private readonly ILogger<DishController> _logger;
        private readonly IDishFacade _dishFacade;

        public DishController(IDishFacade dishFacade, ILogger<DishController> logger)
        {
            _logger = logger;
            _dishFacade = dishFacade;
        }

        [HttpGet]
        public IEnumerable<DishListModel> GetAll()
        {
            return _dishFacade.GetAll();
        }

        [HttpGet("{id:guid}")]
        public ActionResult<DishDetailModel> Get(Guid id)
        {
            var dish = _dishFacade.GetById(id);
            if (dish == null)
            {
                return NotFound();
            }

            return dish;
        }

        [HttpPost]
        public ActionResult<Guid> Create(DishCreateModel dish)
        {
            return _dishFacade.Create(dish);
        }

        [HttpPatch]
        public ActionResult<Guid> Update(DishCreateModel dish)
        {
            var updatedDish = _dishFacade.Update(dish);
            if (updatedDish == null)
            {
                return NotFound();
            }

            return updatedDish;
        }

        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            if (_dishFacade.Delete(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
