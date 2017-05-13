using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModels;

namespace MVC5Course.Controllers
{
    public class ProductsController : BaseController
    {

   

        //private FabricsEntities1 db = new FabricsEntities1();

        // GET: Products
        public ActionResult Index(bool Active = true)
        {

            //var repository = new ProductRepository();
            //repository.UnitOfWork = GetUnitOfWork();
            //ProductRepository repo = RepositoryHelper.GetProductRepository();如同上面兩行

            //var data = repo.All();
            var data = repo.GetProduct列表頁所有資料(Active, showAll: false);
            //var data = db.Product.Where(p => p.Active.HasValue && p.Active.Value == Active).OrderByDescending(p => p.ProductId).Take(10);

            //return View(data);
            ViewData.Model = data; //與上方相同
            return View();
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product=repo.Get單筆資料ByProductId(id.Value); ;
            //Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                //db.Product.Add(product);
                //db.SaveChanges();
                repo.Add(product);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repo.Get單筆資料ByProductId(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(product).State = EntityState.Modified;
                //db.SaveChanges();
                repo.Update(product);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repo.Get單筆資料ByProductId(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Product product = db.Product.Find(id);
            //db.Product.Remove(product);
            //db.SaveChanges();
            Product product = repo.Get單筆資料ByProductId(id);
            repo.Delete(product);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}



        public ActionResult ListProducts(MultiSearchVM _MultiSearchVM)//只要前後端名稱一樣(q) 就是ModelBinding-->有modelbinding 就有model state
        {

            var data = repo.GetProduct列表頁所有資料(true);

            if (!String.IsNullOrEmpty(_MultiSearchVM.q))
            {
                data = data.Where(p => p.ProductName.Contains(_MultiSearchVM.q));
            }

            data = data.Where(p => p.Stock > _MultiSearchVM.Stock_S && p.Stock < _MultiSearchVM.Stock_E);

            ViewData.Model = data
                .Select(p => new productLiteVM()
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Stock = p.Stock
                });

            return View();
            //var data = repo.GetProduct列表頁所有資料(Active :true)
            //   .Select(p => new productLiteVM
            //   {
            //       ProductId = p.ProductId,
            //       ProductName = p.ProductName,
            //       Price = p.Price,
            //       Stock = p.Stock
            //   });
            //return View(data);

            //var data = db.Product.Where(p => p.Active == true)
            //    .Select(p => new productLiteVM
            //{
            //    ProductId=  p.ProductId,
            //    ProductName=  p.ProductName,
            //    Price=  p.Price,
            //    Stock =  p.Stock
            //}).Take(10);
            //return View(data);
        }

        public ActionResult CreateProduct()//create 表單
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(productLiteVM data)//接表單傳來的訊息
        {

            if (ModelState.IsValid)
            {
                TempData["showData"] = "新增成功";

               //儲存資料進資料庫
                return RedirectToAction("ListProducts");
            }
            //驗證失敗
            return View();
        }

    }
}
