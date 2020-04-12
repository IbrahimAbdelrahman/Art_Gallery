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
    [Route("api/orders/{orderid}/items")]
    public class OrderItemsController : ControllerBase
    {
        public OrderItemsController(
            IDutchRepository repository,
            ILogger<OrderItemsController> logger,
            IMapper mapper)
        {
            Repository = repository;
            Logger = logger;
            Mapper = mapper;
        }

        public IDutchRepository Repository { get; }
        public ILogger<OrderItemsController> Logger { get; }
        public IMapper Mapper { get; }

        [HttpGet]
        public IActionResult Get(int orderId)
        {
            var order = Repository.GetOrderById(orderId);
            if (order != null) return Ok(Mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemsViewModel>>(order.OrderItems));
            else return NotFound();

        }

        [HttpGet("{orderItemId}")]
        public IActionResult Get(int orderId, int orderItemId)
        {
            var order = Repository.GetOrderById(orderId);
            if (order != null)
            {
                var item = order.OrderItems.Where(i => i.OrderItemId == orderItemId).FirstOrDefault();
                if (item != null)
                {
                    return Ok(Mapper.Map<OrderItem, OrderItemsViewModel>(item));
                }
                else return NotFound();
            }
            else return NotFound();

        }
    }
}
