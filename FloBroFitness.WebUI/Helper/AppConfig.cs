using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FloBroFitness.WebUI.Helper
{
    public class AppConfig
    {
        public static Int64 Id
        {
            get
            {
                return System.Web.HttpContext.Current.Session["Id"] != null ? Convert.ToInt32(System.Web.HttpContext.Current.Session["Id"]) : 0;
            }
        }
    }
}