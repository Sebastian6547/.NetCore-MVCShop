using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hosting;

        public DutchSeeder(ApplicationDbContext db, IWebHostEnvironment hosting)
        {
            _db = db;
            _hosting = hosting;
        }

        public void Seed()
        {
            _db.Database.EnsureCreated();

            if (!_db.Products.Any())
            {
                var file = Path.Combine(_hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(file);

                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                //var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);

                _db.Products.AddRange(products);

                var order = new Order()
                {
                    OrderDate = DateTime.Today,
                    OrderNumber = "10000",
                    Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    }
                };

                _db.Orders.Add(order);

                _db.SaveChanges();
            }
        }
    }
}
