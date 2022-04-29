using Application.Repositories;
using Application.RequestParameters;
using Application.ViewModels.Products;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly ICustomerWriteRepository _customerWriteRepository;


        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, ICustomerWriteRepository customerWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _customerWriteRepository = customerWriteRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            await Task.Delay(1000);
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            }).ToList();

            return Ok(new
            {
                totalCount,
                products
            });

            return Ok(_productReadRepository.GetAll(false));
            /*var customerId=Guid.NewGuid();
            await _customerWriteRepository.AddAsync(new() { Id = customerId, Name = "Muiidddin" });
            await _orderWriteRepository.AddAsync(new() { Description = "bla bla bla", Address = "Erzurum", CustomerId = customerId });
            await _orderWriteRepository.AddAsync(new() { Description = "bla bla bla 2", Address = "Ankara", CustomerId = customerId });
            await _customerWriteRepository.SaveAsync();*/
            /*Order order = await _orderReadRepository.GetByIdAsync("c818ae48-4051-40d7-b671-f2df62f8abf7");
            order.Address = "Istanbul";
            await _orderWriteRepository.SaveAsync();*/

            /*await _productWriteRepository.AddAsync(new() { Name = "C Product", Price = 1.500F, Stock=10,CreatedDate=DateTime.UtcNow});
            await _productWriteRepository.SaveAsync();*/

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
            Product product = await _productReadRepository.GetByIdAsync(id,false);
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> Post( VM_Create_Product model)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name=model.Name,
                Price=model.Price,
                Stock=model.Stock,
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product=await _productReadRepository.GetByIdAsync(model.Id);
            product.Name = model.Name;
            product.Price = model.Price;
            product.Stock = model.Stock;
            await _productWriteRepository.SaveAsync();

            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
