using DhruvikLimbasiya_0415.Common;
using DhruvikLimbasiya_0415.Model.DbContext;
using DhruvikLimbasiya_0415.Model.ViewModel;
using DhruvikLimbasiya_0415.Repository.Services;
using DhruvikLimbasiya_0415.Session.DhruvikLimbasiya_0415.Session;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DhruvikLimbasiya_0415.Controllers
{
    public class LoginController : Controller
    {
        DhruvikLimbasiya_0415Entities _DbContext = new DhruvikLimbasiya_0415Entities();
        RegistrationService _register = new RegistrationService();

        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]

        public async Task<ActionResult> Login(LoginModel db)

        {

            try

            {


                var userList = _DbContext.RegisterRide.FirstOrDefault(m => m.Email == db.Email);

                if (ModelState.IsValid && db.Email==userList.Email && db.Password==userList.password)
                {


                    if (!SeassionHelper.IsUserLoggedIn && await ApiWebHelper.Login(db) != null )

                    {

                        LoginModel isUserexist = await ApiWebHelper.Login(db);

                        var cookie = new HttpCookie("jwt", isUserexist.Token)

                        {

                            HttpOnly = true,

                            Secure = true,

                            Expires = DateTime.UtcNow.AddMinutes(1)

                        };

                        SeassionHelper.LoggedInUser = userList.Email;

                        Response.Cookies.Add(cookie);

                        Request.Headers.Add("Authorization", "Bearer " + isUserexist.Token);

                        Session["UserId"] = userList.userId;

                        Session["Username"] = SeassionHelper.LoggedInUser;

                        TempData["login"] = "Login SuccessFully";
                        Session["UserId"] = userList.userId;
                        if (userList.Email == "admin@gmail.com" && userList.password == "Admin@123")
                        {
                            return RedirectToAction("Index", "Admin");
                        }

                        return RedirectToAction("Index", "User");

                    }

                    else

                    {
                        TempData["Loginfail"] = "Login failed";

                        return View(db);
                    }
                }
                else
                {
                    TempData["Loginfail"] = "Login failed";

                    return View(db);
                }
            }

            catch (Exception ex)
            {

                throw ex;

            }

        }

         

        public ActionResult Register()
        {

            ViewBag.Country = _DbContext.Country.ToList();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegistrationModel db)
        {

            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                string a = ConvertImageTopath(file);
                db.ProfilePhoto = a;
            }

            db.Password = GenerateRandomPassword(8);

            EmailModel email = new EmailModel();
            email.To = db.Email;
            email.Body = db.Password;
            email.Subject = "This Is Your Password";


            var isEmailexist = _DbContext.RegisterRide.Any(m => m.Email == db.Email);
            if (ModelState.IsValid)
            {
                if (!isEmailexist)
                {
                    if (await ApiWebHelper.Register(db) != null)
                    {

                        var userList = _DbContext.RegisterRide.Where(m => m.Email == db.Email).FirstOrDefault();

                        SentEmail(email);
                        TempData["register"] = "Register successfully";

                        return RedirectToAction("Login");
                    }
                    else
                    {
                        TempData["registerFail"] = "Register Failed";
                        return View(db);
                    }
                }

            }

            if (isEmailexist)
            {
                ModelState.AddModelError("Email", "Email is All Ready Exist");
            }

            ViewBag.Country = _DbContext.Country.ToList();
            return View(db);
        }


        public string ConvertImageTopath(HttpPostedFileBase file)
        {
            string uniqename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            file.SaveAs(HttpContext.Server.MapPath("~/Content/Image/") + uniqename);
            return uniqename;
        }

        static string GenerateRandomPassword(int length)
        {
            const string validChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()_-+=<>?";

            StringBuilder passwordBuilder = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(validChars.Length);
                passwordBuilder.Append(validChars[index]);
            }

            return passwordBuilder.ToString();
        }


        public JsonResult GetStateByCountry(int CountryId)
        {
            List<StateModel> getState = _register.GetStateByCountry(CountryId);
            return Json(getState, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCityStateId(int Stateid)
        {
            List<CityModel> getCity = _register.GetCityStateId(Stateid);
            return Json(getCity, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetZipcodeByCityId(int Cityid)
        {
            List<ZipCodeModel> getCity = _register.GetZipcode(Cityid);
            return Json(getCity, JsonRequestBehavior.AllowGet);
        }

        public void SentEmail(EmailModel model)
        {
            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

            using (MailMessage mm = new MailMessage(smtpSection.From, model.To))
            {
                mm.Subject = model.Subject;
                mm.Body = model.Body;
                mm.IsBodyHtml = false;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = smtpSection.Network.Host;
                    smtp.EnableSsl = smtpSection.Network.EnableSsl;
                    NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    smtp.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                    smtp.Credentials = networkCred;
                    smtp.Port = smtpSection.Network.Port;
                    smtp.Send(mm);
                }
            }

        }



        public ActionResult Logout()

        {
            try

            {
                SeassionHelper.Logout();

                RemoveAllCookies();

                return RedirectToAction("Login");

            }

            catch (Exception ex)

            {

                throw ex;

            }

        }

        public ActionResult RemoveAllCookies()

        {

            foreach (string cookieName in Request.Cookies.AllKeys)

            {

                HttpCookie cookie = new HttpCookie(cookieName);

                cookie.Expires = DateTime.Now.AddDays(-1);

                Response.Cookies.Add(cookie);

            }

            return RedirectToAction("Login");

        }

    }
}