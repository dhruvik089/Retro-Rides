﻿using System.Web;
using System.Web.Mvc;

namespace DhruvikLimbasiya_0415.Api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
