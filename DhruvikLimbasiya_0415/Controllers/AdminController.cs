using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DhruvikLimbasiya_0415.CustomAuthorize.DhruvikLimbasiya_0415.CustomAuthorize;
using DhruvikLimbasiya_0415.Model.DbContext;

namespace DhruvikLimbasiya_0415.Controllers
{
    [CustomAuthorizeAttribute]
    public class AdminController : Controller
    {
        private DhruvikLimbasiya_0415Entities db = new DhruvikLimbasiya_0415Entities();

        // GET: Admin
        public ActionResult Index()
        {
            return View(db.BikeDetails.ToList());
        }
         
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BikeDetails bikeDetails = db.BikeDetails.Find(id);
            if (bikeDetails == null)
            {
                return HttpNotFound();
            }
            return View(bikeDetails);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BikeId,Brand,Model,Year,KilometresDriven,Price,Description,ImageUrls,SellerFirstName,SellerLastName,SellerEmail,SellerPhoneNumber")] BikeDetails bikeDetails)
        {
            if (Request.Files.Count > 0)
            {
                string a = "";
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    a += ConvertImageTopath(file);
                    if (i < Request.Files.Count - 1)
                    {
                        a += ",";
                    }
                }
                bikeDetails.ImageUrls = a;
            }
            if (ModelState.IsValid)
            {
                db.BikeDetails.Add(bikeDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bikeDetails);
        }


        public string ConvertImageTopath(HttpPostedFileBase file)
        {
            string uniqename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            file.SaveAs(HttpContext.Server.MapPath("~/Content/BikeImage/") + uniqename);
            return uniqename;
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BikeDetails bikeDetails = db.BikeDetails.Find(id);
            if (bikeDetails == null)
            {
                return HttpNotFound();
            }
            return View(bikeDetails);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BikeId,Brand,Model,Year,KilometresDriven,Price,Description,ImageUrls,SellerFirstName,SellerLastName,SellerEmail,SellerPhoneNumber")] BikeDetails bikeDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bikeDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bikeDetails);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BikeDetails bikeDetails = db.BikeDetails.Find(id);
            if (bikeDetails == null)
            {
                return HttpNotFound();
            }
            return View(bikeDetails);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BikeDetails bikeDetails = db.BikeDetails.Find(id);
            db.BikeDetails.Remove(bikeDetails);
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
