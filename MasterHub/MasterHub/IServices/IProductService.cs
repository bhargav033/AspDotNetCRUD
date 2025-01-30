using MasterHub.Model;

namespace MasterHub.IServices
{
    public interface IProductService
    {
        Task<APIResponse> AddProductAsync(Product product, IFormFile image);
    }
}
