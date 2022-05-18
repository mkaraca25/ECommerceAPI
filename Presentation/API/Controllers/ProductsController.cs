using Application.Repositories;
using Application.Repositories.ProductImageFile;
using Application.RequestParameters;
using Application.Services;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        readonly IFileService _fileService;
        readonly IFileWriteRepository _fileWriteRepository;
        readonly IFileReadRepository _fileReadRepository;
        readonly IProductImageFileReadRepository _productImageFileReadRepository;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;


        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, ICustomerWriteRepository customerWriteRepository, IWebHostEnvironment webHostEnvironment, IFileService fileService, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IInvoiceFileWriteRepository invoiceFileWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _customerWriteRepository = customerWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
            _fileWriteRepository = fileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            /*string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath,"resource/product-images");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            Random r=new Random();
            foreach (IFormFile file in Request.Form.Files)
            {
                string fullPath = Path.Combine(uploadPath,$"{r.Next()}" +
                    $"{Path.GetExtension(file.FileName)}");
                using FileStream fileStream=new(fullPath, FileMode.Create,FileAccess.Write,
                    FileShare.None,1024*1024,useAsync:false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }*/
            var datas= await _fileService.UploadAsync("resource/product-images", Request.Form.Files);
             await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile() {
                 FileName =d.fileName,
                 Path=d.path
             }).ToList());
            /*var datas = await _fileService.UploadAsync("resource/invoices", Request.Form.Files);
            await _invoiceFileWriteRepository.AddRangeAsync(datas.Select(d => new InvoiceFile()
            {
                FileName = d.fileName,
                Path = d.path,
                Price = new Random().Next()
            }).ToList()) ;*/
            /*var datas = await _fileService.UploadAsync("resource/files", Request.Form.Files);
            await _fileWriteRepository.AddRangeAsync(datas.Select(d =>new Domain.Entities.File()
            {
                FileName = d.fileName,
                Path = d.path,
            }).ToList());
            await _fileWriteRepository.SaveAsync();*/
            /*var d1 = _fileReadRepository.GetAll(false);
            var d2 = _invoiceFileReadRepository.GetAll(false);
            var d3 = _productImageFileReadRepository.GetAll(false);*/
            return Ok();
        }
    }
}
