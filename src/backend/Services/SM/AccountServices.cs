using backend.Core.Entities.SM;
using backend.DTOs.Common.Request;
using backend.DTOs.SM.Request;
using backend.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity.Data;
using System.Security.Claims;
using uc.api.cms.Helper;

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

        public async Task<int> CreateNewAccountAsync(CreateAccountRequest request)
        {
            CreateAccountValidation.Validation(request);
            var user = new sm_accounts(request.password);
            ObjectHelpers.Mapping(request, user);
            user = await _repository.AccountRepository.InsertEntityAsync(user, default!);
            // if you want add default role and claim in here
            return user.id;
            
        }

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
            var jwtToken = _jwtAuthManager.GenerateTokens(user.user_name, claims.ToArray(), DateTime.Now);
            return jwtToken;
        }

        public void LogoutAsync(string userName)
        {
            _jwtAuthManager.RemoveRefreshTokenByUserName(userName);
        }
    }
}
