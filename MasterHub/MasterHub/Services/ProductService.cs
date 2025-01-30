using MasterHub.Data;
using MasterHub.IServices;
using MasterHub.Model;
using Microsoft.EntityFrameworkCore;

namespace MasterHub.Services
{
    public class ProductService(ApplicationDbContext _context) : IProductService
    {
        private readonly string _uploadFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
        public async Task<APIResponse> AddProductAsync(Product product,IFormFile image)
        {
            APIResponse apiResponse = new APIResponse();
            apiResponse.Sucess = false;
            try
            {
                product.catagory = null;
                if (await _context.Product.AnyAsync(x => x.ProductCode == product.ProductCode))
                {
                    apiResponse.ErrorMessage = "Product Code Should be Unique!!";
                    return apiResponse;
                }
                if(product.Quentity <= 0)
                {
                    apiResponse.ErrorMessage = "Product Quentity should be greater then equal to 0";
                    return apiResponse;
                }
                if(product.MfgPrice > product.Price)
                {
                    apiResponse.ErrorMessage = "Profit Should not be in minus";
                    return apiResponse;
                }
                if(image != null)
                {
                    if (!Directory.Exists(_uploadFilePath))
                        Directory.CreateDirectory(_uploadFilePath);

                    var fileName = Path.Combine(_uploadFilePath,Guid.NewGuid().ToString() + Path.GetExtension(image.FileName));
                    using(var stream = new FileStream(fileName,FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    product.Image = Path.Combine("Images", Path.GetFileName(fileName));
                }
                else
                {
                    apiResponse.ErrorMessage = "Image File path Should be requried";
                    return apiResponse;
                }
                product.Profit = (product.Price - product.MfgPrice) * product.Quentity;
                product.ProfitPercentage = (product.Profit / product.Price) * 100;
                var result = await _context.Product.AddAsync(product);
                await _context.SaveChangesAsync();
                apiResponse.Sucess = true;
                apiResponse.Data = result;
            }
            catch (Exception ex)
            {
                apiResponse.ErrorMessage = $"{ex.Message}";
            }
            return apiResponse;
        }
    }
}
