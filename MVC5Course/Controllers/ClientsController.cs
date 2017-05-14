using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class ClientsController : Controller
    {
        private FabricsEntities1 db = new FabricsEntities1();

        // GET: Clients
        public ActionResult Index()
        {
            var client = db.Client.Include(c => c.Occupation);
            return View(client.ToList());
        }
        
    }
}
