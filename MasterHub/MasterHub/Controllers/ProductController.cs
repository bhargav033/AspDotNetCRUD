using MasterHub.IServices;
using MasterHub.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService _productService) : ControllerBase
    {
        [HttpPost("ProductAdd")]
        public async Task<IActionResult> AddProduct([FromForm]Product product)
        {
            var apiresponse = await _productService.AddProductAsync(product, product.File);
            if (apiresponse.Sucess)
                return Ok("Insert Sucessfully");
            return BadRequest(apiresponse);
        }
    }
}
