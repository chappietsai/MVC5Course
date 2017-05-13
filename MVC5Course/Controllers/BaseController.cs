using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public abstract class BaseController : Controller //abstract :抽像類別（防止有人連線）
    {
        //宣告protected 所有繼承class 才都能用，不加的話 預設private
        protected ProductRepository repo = RepositoryHelper.GetProductRepository();

        public ActionResult Debug()
        {
            return View("Hello");
        }

    }
}