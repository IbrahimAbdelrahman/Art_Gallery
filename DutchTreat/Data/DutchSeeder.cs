using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        public DutchSeeder(DutchContext context,IWebHostEnvironment hosting)
        {
            Context = context;
            Hosting = hosting;
        }

        public DutchContext Context { get; }
        public IWebHostEnvironment Hosting { get; }

        public void Seed()
        {
            Context.Database.EnsureCreated();
            if (!Context.Products.Any())
            {
                // Need to create sample data

                var filePath = Path.Combine(Hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filePath);
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);
                Context.AddRange(products);

                var order = Context.Orders.FirstOrDefault(o => o.OrderId == 1);
                if (order != null)
                {
                    order.OrderItems = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 4,
                            ItemPrice = products.First().Price,

                        },

                    };
                }

                Context.SaveChanges();
            }
        }
    }
}
