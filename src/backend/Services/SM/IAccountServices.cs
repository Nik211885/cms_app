using backend.DTOs.Common.Request;
using backend.DTOs.SM.Request;
using Microsoft.AspNetCore.Identity.Data;

namespace backend.Services.SM
{
    public interface IAccountServices
    {
        Task<int> CreateNewAccountAsync(CreateAccountRequest request);
        Task<JwtAuthResult> LoginAsync(AccountLoginRequest request);
        void LogoutAsync(string userName);
    }
}
