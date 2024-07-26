using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DhruvikLimbasiya_0415.Common;
using DhruvikLimbasiya_0415.CustomAuthorize.DhruvikLimbasiya_0415.CustomAuthorize;
using DhruvikLimbasiya_0415.Model.DbContext;
using DhruvikLimbasiya_0415.Model.ViewModel;

namespace DhruvikLimbasiya_0415.Controllers
{
    [CustomAuthorize]
    public class UserController : Controller
    {
        private DhruvikLimbasiya_0415Entities db = new DhruvikLimbasiya_0415Entities();

        // GET: User
        public ActionResult Index()
        {

            List<BikeDetailsModel> bikeDetailsList = db.Database.SqlQuery<BikeDetailsModel>("exec getBikeDeails").ToList();

            ViewBag.Brand = db.Database.SqlQuery<BikeDetailsModel>("exec getBrand").ToList();
            ViewBag.Model = db.Database.SqlQuery<BikeDetailsModel>("exec getModel").ToList();
            return View(bikeDetailsList);
        }

        [HttpPost]
        public async Task<ActionResult> BikeFiler(FilterBikeDetailsModel _filterBikeDetailsModel)
        {

            ViewBag.Brand = db.Database.SqlQuery<BikeDetailsModel>("exec getBrand").ToList();
            ViewBag.Model = db.Database.SqlQuery<BikeDetailsModel>("exec getModel").ToList();
            List<BikeDetailsModel> BikeDetails = await ApiWebHelper.BikeDetailsbyFilter(_filterBikeDetailsModel); 

            return View(BikeDetails);
        }

        // GET: User/Details/5
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

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BikeId,Brand,Model,Year,KilometresDriven,Price,Description,ImageUrls,SellerFirstName,SellerLastName,SellerEmail,SellerPhoneNumber")] BikeDetails bikeDetails)
        {
            if (ModelState.IsValid)
            {
                db.BikeDetails.Add(bikeDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bikeDetails);
        }

        // GET: User/Edit/5
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

        public async Task<ActionResult> AddFavourite(int UserId,int itemId)
        {
            
            var favouriteModels = await ApiWebHelper.AddToFavourite(UserId, itemId);

            TempData["AddFevourite"] = "Successfully Add";

            return RedirectToAction("Index");

           

        }

        public async Task<ActionResult> RemoveFavourite(int itemId)
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            var favouriteModels = await ApiWebHelper.RemoveFavourite(UserId, itemId);
            string urlToRedirect = Url.Action("Favourite",new { UserId = Convert.ToInt32(Session["UserId"])});

            TempData["RemoveFevourite"] = "Successfully Remove";

            return RedirectToAction("Favourite", new { UserId = Convert.ToInt32(Session["UserId"]) });
        }


        public async Task<ActionResult> Favourite()
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            var favouriteItemList = await ApiWebHelper.Favourite(UserId);
            ViewBag.itemList = favouriteItemList;
            return View();
        }

    }
}
