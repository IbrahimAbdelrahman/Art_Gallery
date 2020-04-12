using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        public DutchRepository(DutchContext context, ILogger<DutchRepository> logger)
        {
            Context = context;
            Logger = logger;
        }

        public DutchContext Context { get; }
        public ILogger<DutchRepository> Logger { get; }

        /////////////////////////////////////////////////////////////////////////
        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                Logger.LogInformation("get all products called");
                return Context.Products.OrderBy(p => p.Title).ToList();
            }
            catch (Exception ex)
            {

                Logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            try
            {
                return Context.Products.Where(p => p.Category == category).ToList();
            }
            catch (Exception ex)
            {

                Logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }

        /////////////////////////////////////////////////////////////////////////
        public IEnumerable<Order> GetAllOrders()
        {
            try
            {
                Logger.LogInformation("get all orders called");
                return Context.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(i => i.Product)
                    .ToList();
            }
            catch (Exception ex)
            {

                Logger.LogError($"Failed to get all orders: {ex}");
                return null;
            }
        }
        public Order GetOrderById(int id)
        {
            try
            {
                Logger.LogInformation("get the defined orders called");
                return Context.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(i => i.Product)
                    .Where( o => o.OrderId == id)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {

                Logger.LogError($"Failed to get the defined order: {ex}");
                return null;
            }
        }
        public object AddEntity(object model)
        {
            Context.Add(model);
            return model;
        }
        /////////////////////////////////////////////////////////////////////////

        public bool Save()
        {
            try
            {
                return Context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to get all products: {ex}");
                return false;
            }
        }

        
    }
}
