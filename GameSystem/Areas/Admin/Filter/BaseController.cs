using System;
using System.Collections.Generic;
using GameSystem.Commons;
using GameSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc.Authorization;
using Newtonsoft.Json;

namespace GameSystem.Areas.Admin.Filter
{
    public class BaseController : Controller
    {
        public BaseController()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool isAllowAnonymous = false;
            IList<IFilterMetadata> lst = context.Filters;
            foreach (var obj in lst)
            {
                if (obj is AllowAnonymousFilter)
                {
                    isAllowAnonymous = true;
                    break;
                }
            }
            User user = HttpContext.Session.Get<User>("user");
            if (isAllowAnonymous || user != null)
            {
                base.OnActionExecuting(context);
            }
            else
            {
                if (context.HttpContext.Request.IsAjax())
                {
                    context.Result = new JsonResult(new {
                        Code = 403,
                        Message = "没有访问权限"
                    });
                } else {
                    context.Result = new RedirectResult("/Admin/Home/Login");
                }
            }
        }
    }
}
