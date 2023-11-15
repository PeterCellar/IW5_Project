using Delivery.Api.BL.Facades;
using Delivery.Api.BL.Facades.Interfaces;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Common.Models.Order;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<DishController> _logger;
        private readonly IOrderFacade _orderFacade;

        public OrderController(IOrderFacade orderFacade, ILogger<DishController> logger)
        {
            _logger = logger;
            _orderFacade = orderFacade;
        }

        [HttpGet]
        public IEnumerable<OrderListModel> GetAll()
        {
            return _orderFacade.GetAll();
        }

        [HttpGet("{id:guid}")]
        public ActionResult<OrderDetailModel> Get(Guid id)
        {
            var order = _orderFacade.GetById(id);
            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpPost]
        public ActionResult<Guid> Create(OrderCreateModel order)
        {
            return _orderFacade.Create(order);
        }

        [HttpPatch]
        public ActionResult<Guid> Update(OrderCreateModel order)
        {
            var updatedOrder = _orderFacade.Update(order);
            if (updatedOrder == null)
            {
                return NotFound();
            }

            return updatedOrder;
        }

        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            if (_orderFacade.Delete(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }

}
