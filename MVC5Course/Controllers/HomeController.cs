using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult PartialAbout()
        {//可在console $.get('/Home/PartialAbout',function(data){alert(data)})
            ViewBag.Message = "Your application description page.";

            if (Request.IsAjaxRequest())//check Ajax
            {
                return PartialView("About");
            }else
            {

                return View("About");
            }

            
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult unknow()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult test()
        {
            return View();
        }


    }
}