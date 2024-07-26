using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DhruvikLimbasiya_0415.Session
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    namespace DhruvikLimbasiya_0415.Session
    {
        public class SeassionHelper
        {
            public static string LoggedInUser

            {

                get { return HttpContext.Current.Session["Key"] as string; }

                set { HttpContext.Current.Session["Key"] = value; }

            }

            public static int GetUserId

            {
                get
                {

                    return HttpContext.Current.Session["key"] == null ? 0 : (int)HttpContext.Current.Session["Key"];

                }

                set { HttpContext.Current.Session["Key"] = value; }

            }

            public static bool IsUserLoggedIn

            {
                get
                {
                    if (HttpContext.Current.Session["key"] == null) { return false; }

                    else { return true; }
                }
            }

            public static void Logout()

            {
                HttpContext.Current.Session.Abandon();

            }

        }
    }
}