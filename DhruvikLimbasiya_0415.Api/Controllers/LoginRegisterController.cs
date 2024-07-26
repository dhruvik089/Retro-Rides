using DhruvikLimbasiya_0415.Api.JwtToken;
using DhruvikLimbasiya_0415.Helper.Helper;
using DhruvikLimbasiya_0415.Model.DbContext;
using DhruvikLimbasiya_0415.Model.ViewModel;
using DhruvikLimbasiya_0415.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DhruvikLimbasiya_0415.Api.Controllers
{
    public class LoginRegisterController : ApiController
    {
        DhruvikLimbasiya_0415Entities _context = new DhruvikLimbasiya_0415Entities();
        RegistrationService _register = new RegistrationService();


        [Route("api/Registration/Registers")]
        [HttpPost]
        public RegisterRide Registers(RegistrationModel db)

        {

            RegisterRide _registerUser = new RegisterRide();

            _registerUser = LoginConvert.ConvertModelToDbModel(db);

            var isEmailExist = _context.RegisterRide.Any(x => x.Email == db.Email);

            if (!isEmailExist)

            {
                if ((db.Email != "admin123@gmail.com"))
                {

                    _register.addUser(db);

                    return _registerUser;
                }
                else
                {
                    ModelState.AddModelError("Email", "You can not register this email");

                    return null;
                }

            }

            else if (isEmailExist)
            {

                ModelState.AddModelError("Email", "Email is Already Exist");

                return null;

            }

            else

            {

                return null;

            }

        }

        [Route("api/Registration/Login")]
        [HttpPost]
        public LoginModel Login(LoginModel user)

        {

            string email = user.Email;

            string password = user.Password;

            var keepLogin = true;

            bool keepLoginSession;

            keepLoginSession = keepLogin == true;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))

            {

                ModelState.AddModelError("", "Please enter valid username and password");

                return null;

            }

            RegisterRide appUserInfo = _context.RegisterRide.FirstOrDefault(u => u.Email == email);

            if (appUserInfo != null && ModelState.IsValid && appUserInfo.Email == user.Email && appUserInfo.password == user.Password)

            {

                string encryptedPwd = password;

                var userPassword = appUserInfo.password;

                var username = appUserInfo.Email;

                if (encryptedPwd.Equals(userPassword) && username.Equals(email))

                {
                     

                    var jwtToken = Authentication.GenerateJWTAuthetication(email);

                    var validUserName = Authentication.ValidateToken(jwtToken);

                    if (string.IsNullOrEmpty(validUserName))

                    {

                        ModelState.AddModelError("", "Unauthorized login attempt ");

                        return null;

                    }

                    var cookie = new HttpCookie("jwt", jwtToken)

                    {

                        HttpOnly = true,

                        Secure = true,  

                        Expires = DateTime.UtcNow.AddMinutes(1)

                    };

                    HttpContext.Current.Response.Cookies.Add(cookie);

                    user.Token = jwtToken;

                    return user;

                }

            }

            return null;    

        }

    }
}