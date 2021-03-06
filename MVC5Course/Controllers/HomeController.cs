﻿using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HomeController : BaseController
    {

        FabricsEntities1 db = new FabricsEntities1();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SomeAction()
        {

            return PartialView("SuccessRedirect","/");
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
        [LocalOnly]
        [SharedViewBag]///用於做trace log 好用
        public ActionResult About()
        {

            throw new ArgumentException("Error Handled!!");

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

        public ActionResult GetFile()
        {
            //return File(Server.MapPath("~/Content/ABC.png"),"image/png");
            return File(Server.MapPath("~/Content/ABC.png"), "image/png","New.png");//強迫下載
        }

        public ActionResult GetJosn()
        {
            db.Configuration.LazyLoadingEnabled = false;//close lazy loadinig
            return Json(db.Product.Take(10),JsonRequestBehavior.AllowGet);//Json 預設不能用Get 所以要加JsonRequestBehavior.AllowGet
            
        }



        public ActionResult VT()
        {

            ViewBag.IsEnable = true;
            return View();
        }

        public ActionResult RazorTest()
        {
            ViewData.Model = new int[] { 1, 2, 3, 4, 5 };
            return PartialView();
        }

    }
}