using MasterHub.Data;
using MasterHub.IServices;
using MasterHub.Model;
using MasterHub.ModelDTO;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MasterHub.Services
{
    public class AuthService(ApplicationDbContext _context) : IAuthService
    {
        public async Task<APIResponse> AddUserAsync(Auth auth)
        {
            APIResponse apiResponse = new APIResponse();
            apiResponse.Sucess = false;
            try
            {
                if(await _context.User.AnyAsync(x=>x.EmailAddress == auth.EmailAddress))
                {
                    apiResponse.ErrorMessage = "User Already Registeres!!";
                    return apiResponse;
                }
                auth.Password = BCrypt.Net.BCrypt.HashPassword(auth.Password);
                await _context.AddAsync(auth);
                await _context.SaveChangesAsync();
                apiResponse.Sucess = true;
                apiResponse.Data = auth;
            }
            catch(Exception ex)
            {
                apiResponse.ErrorMessage = $"{ex.Message}";
            }
            return apiResponse;
        }
        public async Task<APIResponse> LoginAsync(LoginDTO login)
        {
            APIResponse apiResponse = new APIResponse();
            apiResponse.Sucess = false;
            try
            {
                var record = await _context.User.FirstOrDefaultAsync(x=>x.EmailAddress == login.Email);
                if (record == null)
                {
                    apiResponse.ErrorMessage = $"{login.Email} is not Valid!!";
                    return apiResponse;
                }
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(login.Password, record.Password);
                if (!isPasswordValid)
                {
                    apiResponse.ErrorMessage = $"{login.Password} is not valid";
                    return apiResponse;
                }
                apiResponse.Data = record;
                apiResponse.Sucess = true;
            }
            catch(Exception ex)
            {
                apiResponse.ErrorMessage = ex.Message;
            }
            return apiResponse;
        }
    }
}
