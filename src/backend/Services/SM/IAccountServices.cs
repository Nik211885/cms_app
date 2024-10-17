using backend.DTOs.Common.Request;
using backend.DTOs.SM.Request;
using Microsoft.AspNetCore.Identity.Data;
using UC.Core.Models.FormData;

namespace backend.Services.SM
{
    public interface IAccountServices
    {
        Task<int> CreateNewAccountAsync(CreateAccountRequest request);
        Task<JwtAuthResult> LoginAsync(AccountLoginRequest request);
        Task<JwtAuthResult> ChangePasswordAsync(int userId, ChangePasswordRequest request);
        Task<JwtAuthResult> UpdateProfileAsync(int userId, UpdateProfileAccountRequest request);
        void Logout(string userName);
        JwtAuthResult GetTokenWhenAccessTokenHasExprise(JwtAuthResult token);
        Task<string> ResetPasswordUserHasIdAsync(int userId);
        Task<IEnumerator<AccountDetailRequest>> GetAllAccountDetailAsync(OSearch search);
        Task<PagedResponse> GetAllAccountDetailWithPaginationAsync(OSearch search, int currentPage, int pageSize);
    }
}
