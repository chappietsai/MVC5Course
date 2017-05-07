using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        FabricsEntities1 db = new FabricsEntities1();
        // GET: EF
        public ActionResult Index()
        {
            var all = db.Product.AsQueryable();//.AsQueryable() 不會立刻執行 延遲執行
            var data = all.Where(p => p.Is刪除 == false && p.Active == true )
                        .OrderByDescending(p => p.ProductId);


            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            var data = db.Product.Find(id);

            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(int id, Product data)
        {
            if (ModelState.IsValid)
            {
                var item = db.Product.Find(id);
                item.Active = data.Active;
                item.Price = data.Price;
                item.ProductName = data.ProductName;
                item.Stock = data.Stock;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Delete(int id)
        {


            var item= db.Product.Find(id);

            //foreach (var temp in item.OrderLine.ToList())
            //{
            //    db.OrderLine.Remove(temp);
            //}

            //db.OrderLine.RemoveRange(item.OrderLine); //與上方foreach 同效果
            //db.Product.Remove(item);
            item.Is刪除 = true;
       
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
            

            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {

            var data =db.Database.SqlQuery<Product>("select * from dbo.Product where ProductId=@p0 ",id).FirstOrDefault();

            return View(data);
        }


        //Will 有新增Removeall function ...
        public void RemoveAll()
       {
           //db.Product.RemoveRange(db.Product);
           //db.SaveChanges();

           db.Database.ExecuteSqlCommand("DELETE FROM dbo.Product");
       }
}
}