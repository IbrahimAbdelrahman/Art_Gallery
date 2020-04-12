using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]

    public class OrdersController : ControllerBase
    {
        public OrdersController(
            IDutchRepository repository,
            ILogger<OrdersController> logger,
            IMapper mapper)
        {
            Repository = repository;
            Logger = logger;
            Mapper = mapper;
        }

        public IDutchRepository Repository { get; }
        public ILogger<OrdersController> Logger { get; }
        public IMapper Mapper { get; }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(Repository.GetAllOrders()));
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to get orders: {ex}");
                return BadRequest($"Failed to get orders");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = Repository.GetOrderById(id);
                if (order != null) return Ok(Mapper.Map<Order, OrderViewModel>(order));
                else return NotFound();

            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to get defined order: {ex}");
                return BadRequest($"Failed to get defined order");
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = Mapper.Map<OrderViewModel, Order>(model);
                    if (newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }
                    Repository.AddEntity(newOrder);
                    if (Repository.Save())
                    {
                        

                        return Created($"api/orders/{newOrder.OrderId}", Mapper.Map<Order,OrderViewModel>(newOrder));
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {

                Logger.LogError($"Failed to add a new order: {ex}");
                return BadRequest($"Failed to add a new order");
            }
            return BadRequest("Failed to add a new order");
        }
    }
}
