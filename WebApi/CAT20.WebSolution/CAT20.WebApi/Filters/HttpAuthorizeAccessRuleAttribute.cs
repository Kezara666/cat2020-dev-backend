using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Net;

namespace CAT20.WebApi.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class HttpAuthorizeAccessRuleAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private string rule;

        public HttpAuthorizeAccessRuleAttribute()
        {
        }

        public string Rule
        {
            get => rule;
            set => rule = !string.IsNullOrWhiteSpace(value) ? value : "NOTDEFINED";
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userAssignedRuleList = context.HttpContext.User?.FindFirst("userAssignedRuleList")?.Value;

            if (!string.IsNullOrEmpty(userAssignedRuleList) && userAssignedRuleList.Split(',').Contains(rule))
            {
                return;
            }

            context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
        }
    }
}
