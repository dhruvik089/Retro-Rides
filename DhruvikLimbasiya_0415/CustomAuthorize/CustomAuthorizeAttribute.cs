using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DhruvikLimbasiya_0415.CustomAuthorize
{
    using global::DhruvikLimbasiya_0415.Model.DbContext;
    using global::DhruvikLimbasiya_0415.Session.DhruvikLimbasiya_0415.Session;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    namespace DhruvikLimbasiya_0415.CustomAuthorize
    {

        public class CustomAuthorizeAttribute : AuthorizeAttribute

        {

            protected override bool AuthorizeCore(HttpContextBase httpContext)

            {

                try

                {
                    DhruvikLimbasiya_0415Entities _dbContext = new DhruvikLimbasiya_0415Entities();

                    RegisterRide list = _dbContext.RegisterRide.FirstOrDefault(m => m.Email == SeassionHelper.LoggedInUser);

                    if (list != null)

                    {

                        return SeassionHelper.IsUserLoggedIn;

                    }

                    else

                    {

                        return false;

                    }

                }

                catch (Exception e)

                {

                    throw e;

                }

            }

            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)

            {

                try

                {

                    if (!SeassionHelper.IsUserLoggedIn)

                    {

                        filterContext.Result = new RedirectResult("~/Login/Login");

                    }

                    else

                    {

                        filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);

                    }

                }

                catch (Exception e)

                {

                    throw e;

                }

            }

        }
    }
}