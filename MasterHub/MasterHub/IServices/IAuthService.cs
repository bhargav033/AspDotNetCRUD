using MasterHub.Model;
using MasterHub.ModelDTO;

namespace MasterHub.IServices
{
    public interface IAuthService
    {
        public Task<APIResponse> AddUserAsync(Auth auth);
        public Task<APIResponse> LoginAsync(LoginDTO login);
    }
}
