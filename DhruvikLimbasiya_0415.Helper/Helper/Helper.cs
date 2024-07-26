using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DhruvikLimbasiya_0415.Helper.Helper
{
    class Helper
    {
        public async Task<ActionResult> Login(LoginModel db)

        {

            try

            {


                var userList = _DbContext.Users.FirstOrDefault(m => m.email == db.email);

                if (ModelState.IsValid)
                {


                    if (!SeassionHelper.IsUserLoggedIn && await ApiWebHelper.Login(db) != null)

                    {

                        LoginModel isUserexist = await ApiWebHelper.Login(db);

                        var cookie = new HttpCookie("jwt", isUserexist.Token)

                        {

                            HttpOnly = true,

                            Secure = true,

                            Expires = DateTime.UtcNow.AddMinutes(1)

                        };

                        SeassionHelper.LoggedInUser = userList.username;

                        Response.Cookies.Add(cookie);

                        Request.Headers.Add("Authorization", "Bearer " + isUserexist.Token);

                        Session["UserId"] = userList.userid;

                        Session["Username"] = SeassionHelper.LoggedInUser;

                        TempData["login"] = "Login SuccessFully";

                        if (userList.email == "admin@gmail.com" && userList.password == "Admin@123")
                        {
                            return RedirectToAction("Dashboard", "Dashboard");
                        }

                        return RedirectToAction("Product", "User");

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
        public async Task<ActionResult> Registration(RegistrationModel db)

        {
            try

            {

                var IsEmailExist = _DbContext.Users.Any(m => m.email == db.email);

                if (IsEmailExist)

                {
                    TempData["Registerfail"] = "Email Is Already Exist";
                    return View(db);
                }

                else if (ModelState.IsValid)

                {
                    // if (!_DbContext.Users.Any(m => m.email == db.email) && (db.email!= "admin123@gmail.com"))
                    if (await ApiWebHelper.Register(db) != null)
                    {
                        //  await ApiWebHelper.Register(db);

                        var userList = _DbContext.Users.Where(m => m.email == db.email).FirstOrDefault();

                        TempData["register"] = "Success Fully Register..!!";

                        return RedirectToAction("Login");
                    }
                    else
                    {

                        TempData["faieldRegister"] = "register fail";
                        //ModelState.AddModelError("email", "You can not register by this email");
                        return View(db);
                    }
                }

                else

                {

                    TempData["faieldRegister"] = "register fail";

                    return View(db);

                }



            }

            catch (Exception ex)

            {

                throw ex;

            }

        }

    }
}
