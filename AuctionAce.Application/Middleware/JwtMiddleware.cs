using AuctionAce.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuctionAce.Application.Middleware
{
    public class JwtAuthenticationAttribute : ActionFilterAttribute
    {
        private readonly string[] _roles;

        public JwtAuthenticationAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var token = request.Cookies["jwt"];

            if (token != null)
            {
                var userInfo = AuthenticationService.ValidateToken(token);
                if (userInfo == null)
                {
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "NoUserError",
                    };
                    return;
                }

                if (_roles.Contains(userInfo[0]))
                {
                    return;
                }
                else
                {
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "Unauthorized",
                    };
                    return;
                }
            }
            else
            {
                filterContext.Result = new StatusCodeResult(401);
            }
        }
    }
}