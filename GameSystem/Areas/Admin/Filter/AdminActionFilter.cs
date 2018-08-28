using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GameSystem.Areas.Admin.Filter
{
	public class AdminActionFilter: IActionFilter
    {
        public AdminActionFilter()
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //context.HttpContext.Session.Get
        }
    }
}
