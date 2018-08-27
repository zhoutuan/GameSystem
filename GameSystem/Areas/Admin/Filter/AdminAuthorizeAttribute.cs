using System;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace GameSystem.Areas.Admin.Filter
{
    public class AdminAuthorizeAttribute: Attribute, IAuthorizationFilter
    {
        public AdminAuthorizeAttribute()
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            byte[] bt = Encoding.UTF8.GetBytes("");
            bool isLogin= context.HttpContext.Session.TryGetValue("user",out bt);
            var user = "";
            if (isLogin)
            {
                user = Encoding.UTF8.GetString(bt);
            }
            Console.WriteLine("-------------------" + user);
        }
    }
}
