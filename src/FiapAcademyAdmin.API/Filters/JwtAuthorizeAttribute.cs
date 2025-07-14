using Microsoft.AspNetCore.Authorization;

namespace FiapAcademyAdmin.API.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JwtAuthorizeAttribute : AuthorizeAttribute
    {
        public JwtAuthorizeAttribute(string? roles = null)
        {
            if (!string.IsNullOrEmpty(roles))
            {
                Roles = roles;
            }
        }
    }
} 