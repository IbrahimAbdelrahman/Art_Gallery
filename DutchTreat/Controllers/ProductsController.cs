using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        public ProductsController(IDutchRepository repository, ILogger<ProductsController> logger)
        {
            Repository = repository;
            Logger = logger;
        }

        public IDutchRepository Repository { get; }
        public ILogger<ProductsController> Logger { get; }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Product>> Get()
        {
            try
            {
                return Ok(Repository.GetAllProducts());
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"Failed to get Products: {ex}");
                return BadRequest("Failed to get products");
            }
        }

    }
}

