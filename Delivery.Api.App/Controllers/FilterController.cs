using Delivery.Api.BL.Facades;
using Delivery.Api.BL.Facades.Interfaces;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Common.Models;
using Delivery.Common.Models.Dish;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilterController : ControllerBase
    {
        private readonly ILogger<DishController> _logger;
        private readonly IFilterFacade _filterFacade;

        public FilterController(IFilterFacade filterFacade, ILogger<DishController> logger)
        {
            _logger = logger;
            _filterFacade = filterFacade;
        }

        [HttpGet("{substring}")]
        public ActionResult<List<FilterModel>> GetBySubstring(string substring)
        {
            var filter = _filterFacade.GetBySubstring(substring);
            if (filter == null)
            {
                return NotFound();
            }

            return filter;
        }
    }
}
