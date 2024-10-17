using backend.Attribute.Validation;
using backend.Core.Entities.SM;
using backend.DTOs.Common.Request;
using backend.DTOs.SM.Reponse;
using backend.DTOs.SM.Request;
using backend.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity.Data;
using System.Security.Claims;
using uc.api.cms.Helper;
using uc.api.cms.Infrastructure.Authentication;
using UC.Core.Models.FormData;

namespace backend.Services.SM
{
    public class AccountServices : IAccountServices
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IJwtAuthManager _jwtAuthManager;
        public AccountServices(IRepositoryWrapper repository, IJwtAuthManager jwtAuthManager)
        {
            _repository = repository;
            _jwtAuthManager = jwtAuthManager;
        }

        public Task AddRoleToAccountAsync(int roleId, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<JwtAuthResult> ChangePasswordAsync(int userId, ChangePasswordRequest request)
        {
            ChangePasswordRequestValidation.Validation(request);
            var user = await _repository.AccountRepository.GetEntityByIdAsync(userId, default!);
            if (user is null)
            {
                throw new Exception($"Not find user have id is {userId}");
            }
            user.update_at = DateTime.Now;
            user.password_hash = SMSecurityHelper.HashPassword(request.NewPassword);
            await _repository.AccountRepository.UpdateEntityAsync(user, default!);
            var claims = await GeneratorClaimForUserAsync(user);
            var jwt = _jwtAuthManager.GenerateTokens(user.user_name, claims, DateTime.Now);
            return jwt;
        }

        public async Task<int> CreateNewAccountAsync(CreateAccountRequest request)
        {
            CreateAccountValidation.Validation(request);
            var user = new sm_accounts(request.password);
            ObjectHelpers.Mapping(request, user);
            user = await _repository.AccountRepository.InsertEntityAsync(user, default!);
            // if you want add default role and claim in here
            return user.id;
            
        }

        public async Task<PagedResponse> GetAllAccountDescriptionWithPaginationAsync(OSearch? search, int currentPage, int pageSize = 10)
        {
            var items = new List<AccountReponse>();
            var field = search?.fields;
            int count = 0;
            var account = await _repository.AccountRepository.SearchUserWithPaginationAsync((countItems) =>
            {
                count = countItems;
                return new PagedRequest(currentPage, pageSize, countItems);
            }, field is not null ? field : default!);
            foreach(var a in account)
            {
                var ar = new AccountReponse();
                ObjectHelpers.Mapping(a, ar);
                items.Add(ar);
            }
            return new PagedResponse(currentPage, count, items);
        }

        public JwtAuthResult GetTokenWhenAccessTokenHasExprise(JwtAuthResult token)
            => _jwtAuthManager.Refresh(token.RefreshToken.TokenString, token.AccessToken, DateTime.Now);

        public async Task<JwtAuthResult> LoginAsync(AccountLoginRequest request)
        {
            AccountLoginRequestValidation.Validation(request);
            var user = await _repository.AccountRepository.GetAccountByUserNameAsync(request.user_name);
            if (user is null)
            {
                throw new Exception("User name is not exits");
            }
            if(!SMSecurityHelper.VerifyPassword(user.password_hash, request.password))
            {
                throw new Exception("Password is not correct");
            }
            var claims = await GeneratorClaimForUserAsync(user);
            var jwt = _jwtAuthManager.GenerateTokens(user.user_name, claims, DateTime.Now);
            return jwt;
        }

        public void Logout(string userName) => _jwtAuthManager.RemoveRefreshTokenByUserName(userName);

        public async Task<string> ResetPasswordUserHasIdAsync(int userId)
        {
            var user = await _repository.AccountRepository.GetEntityByIdAsync(userId, default!);
            if(user is null)
            {
                throw new Exception($"Don't find user has id is {userId}");
            }
            var passwordReset = SMSecurityHelper.GenerateNewPassword();
            if (!RegularHelpersExtension.IsPassword(passwordReset))
            {
                throw new Exception("Error process reset password");
            }
            user.password_hash = SMSecurityHelper.HashPassword(passwordReset);
            user.update_at = DateTime.Now;
            // remove refresh token
            _jwtAuthManager.RemoveRefreshTokenByUserName(user.user_name);
            await _repository.AccountRepository.UpdateEntityAsync(user, default!);
            return passwordReset;
        }

        public async Task<JwtAuthResult> UpdateProfileAsync(int userId, UpdateProfileAccountRequest request)
        {
            UpdateProfileAccountRequestValidation.Validation(request);
            var user = await _repository.AccountRepository.GetEntityByIdAsync(userId, default!);
            if(user is null)
            {
                throw new Exception($"Not find user have id is {userId}");
            }
            ObjectHelpers.Mapping(request, user);
            user.update_at = DateTime.Now;  
            await _repository.AccountRepository.UpdateEntityAsync(user, default!);
            var claims = await GeneratorClaimForUserAsync(user);
            var jwt = _jwtAuthManager.GenerateTokens(user.user_name, claims, DateTime.Now);
            return jwt;

        }
        private async Task<Claim[]> GeneratorClaimForUserAsync(sm_accounts user)
        {
            var claims = new List<Claim>
            {
                new( ClaimTypes.NameIdentifier,user.id.ToString()),
                new( ClaimTypes.Name, user.user_name),
            };
            // get role
            var roles = await _repository.RoleRepository.GetRolesByUserIdAsync(user.id);
            foreach (var r in roles)
            {
                claims.Add(new(ClaimTypes.Role, r.name));
                var roleClaims = await _repository.RoleRepository.GetRoleClaimsByRoleIdAsync(r.id);
                foreach (var rClaim in roleClaims)
                {
                    claims.Add(new(rClaim.claim_type, rClaim.claim_value));
                }
            }
            // get role claim
            // get account claims
            var accountClaims = await _repository.AccountClaimRepository.GetAccountClaimByUserIdAsync(user.id);
            foreach (var claim in accountClaims)
            {
                claims.Add(new(claim.claim_type, claim.claim_value));
            }
            return [.. claims];
        }
    }
}
