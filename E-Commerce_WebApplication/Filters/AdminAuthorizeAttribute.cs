using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace E_Commerce_WebApplication.Filters
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _requiredRole;
        public AuthorizeAttribute(string requiredRole)
        {
            _requiredRole = requiredRole;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string session = context.HttpContext.Session.GetString("usertype");
            if (session != _requiredRole)
            {
                context.Result = new RedirectToActionResult("Unauthorized", "Home",null);
            }
        }
    }
}
