using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    internal class SharedViewBagAttribute : ActionFilterAttribute
    {
        //OnActionExecuting --->Action 執行之前
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.Message= "Your application description page. -->chappie";

            base.OnActionExecuting(filterContext);
        }
        //OnActionExecuted -->Action 執行之後
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }


    }
}