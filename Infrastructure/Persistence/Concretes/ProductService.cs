using Application.Abstractions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Concretes
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts()
            => new()
            {
                new() { Id = Guid.NewGuid(), Name ="Prroduct 1",Price =100,Stock=15},
                new() { Id = Guid.NewGuid(),Name ="Product 2",Price=150,Stock=15},
                new() { Id = Guid.NewGuid(), Name = "Product 3", Price = 250, Stock = 15 },
                new() { Id = Guid.NewGuid(), Name = "Product 4", Price = 50, Stock = 15 },
                new() { Id = Guid.NewGuid(), Name = "Product 5", Price = 75, Stock = 15 }
            };
        
    }
}
