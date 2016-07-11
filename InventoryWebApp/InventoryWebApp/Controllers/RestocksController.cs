using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InventoryWebApp.Models;
using Microsoft.AspNet.Identity;

namespace InventoryWebApp.Controllers
{
    public class RestocksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Restocks
        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId().ToString();
            var restocks = db.Restocks.Where(r => r.UserId == currentUserId);
            return View(restocks);
        }

        // GET: Restocks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restock restock = db.Restocks.Find(id);
            if (restock == null)
            {
                return HttpNotFound();
            }
            return View(restock);
        }

        // GET: Restocks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RestockId,QuantityPurchased,PurchasePricePerUnit,UserId,ProductId")] Restock restock)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId().ToString();
                int productId = (int)TempData["ProductId"];
                restock.UserId = currentUserId;
                restock.ProductId = productId;
                
                db.Restocks.Add(restock);
                db.SaveChanges();

                Product userProduct = db.Products.Where(p => p.ProductId == productId).FirstOrDefault();
                userProduct.Quantity += restock.QuantityPurchased;

                var restocks = db.Restocks.Where(r => r.ProductId == productId);
                decimal totalInvestment = 0;
                decimal pricePerUnit;
                int numberOfUnits = 0;

                foreach (var r in restocks)
                {
                    numberOfUnits += r.QuantityPurchased;
                    totalInvestment += (r.PurchasePricePerUnit * r.QuantityPurchased);
                }

                pricePerUnit = totalInvestment / numberOfUnits;
                userProduct.PurchasePricePerUnit = pricePerUnit;

                db.SaveChanges();

                return RedirectToAction("Index", "Products");
            }

            return View(restock);
        }

        // GET: Restocks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restock restock = db.Restocks.Find(id);
            if (restock == null)
            {
                return HttpNotFound();
            }
            return View(restock);
        }

        // POST: Restocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RestockId,QuantityPurchased,PurchasePricePerUnit,UserId,ProductId")] Restock restock)
        {
            if (ModelState.IsValid)
            {
                TempData["UserId"] = restock.UserId;
                TempData["ProductId"] = restock.ProductId;
                db.Entry(restock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restock);
        }

        // GET: Restocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restock restock = db.Restocks.Find(id);
            if (restock == null)
            {
                return HttpNotFound();
            }
            restock.ProductId =(int) TempData["ProductId"];
            restock.UserId = TempData["UserId"].ToString();
            return View(restock);
        }

        // POST: Restocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restock restock = db.Restocks.Find(id);
            db.Restocks.Remove(restock);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
