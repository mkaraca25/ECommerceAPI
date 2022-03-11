using Application.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }
        [HttpGet]
        public async Task Get()
        {

            /*await _productWriteRepository.AddRangeAsync(new()
            {
                new() { Id = Guid.NewGuid(), Name = "Product 1", Price = 100, CreatedDate = DateTime.UtcNow, Stock = 10 },
                new() { Id = Guid.NewGuid(), Name = "Product 2", Price = 25, CreatedDate = DateTime.UtcNow, Stock = 15 },
                new() { Id = Guid.NewGuid(), Name = "Product 3", Price = 150, CreatedDate = DateTime.UtcNow, Stock = 20 },
            });
            var count = await _productWriteRepository.SaveAsync();*/
            /*Product p = await _productReadRepository.GetByIdAsync("fc0ad718-a5c6-4f57-a4f9-7d00facb7bfd",false);//takip edilmek isteniyorsa false silinir.
            p.Name = "Mehmet";
            await _productWriteRepository.SaveAsync();*/
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            Product product = await _productReadRepository.GetByIdAsync(id);
            return Ok(product);
        }
    }
}
