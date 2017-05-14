using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    internal class LocalOnlyAttribute : ActionFilterAttribute
    {
        //OnActionExecuting --->Action 執行之前
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsLocal)
            {
                 //filterContext.Result = new RedirectResult("/");//直接 filterContext.Result 這樣會直接跳過Action
            }

        }



    }
}