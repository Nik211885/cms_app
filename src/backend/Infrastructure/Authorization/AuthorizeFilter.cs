using backend.Core.ValueObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace uc.api.cms.Infrastructure.Authorization
{
    public class AuthorizeFilter : AuthorizeAttribute, IAuthorizationFilter
        
    {
        public AuthorizeFilter(Policy policy) : base(policy.ToString())
        {            
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = context.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}
