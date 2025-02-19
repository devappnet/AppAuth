using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace App.WebApi.Configuration
{
    public class PermissionAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _requiredPermission;

        public PermissionAttribute(string requiredPermission)
        {
            _requiredPermission = requiredPermission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            // Check if user is authenticated
            if (!user.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Check if the user has the required permission
            var permissions = user.FindAll("Permission").Select(p => p.Value);
            if (!permissions.Contains(_requiredPermission))
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
