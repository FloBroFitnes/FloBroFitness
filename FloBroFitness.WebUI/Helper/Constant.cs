using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FloBroFitness.WebUI.Helper
{
    public class Constant
    {
        public static class StatusID
        {
            public static int Pending = 1;
            public static int Declined = 2;
            public static int Approved = 3;
        }
        public static string AdminRegisterCVFilePath = "~/AdminCV/";
        public static string ProductFileImagePath = "~/Product/Image";
        public static string ProductFileVideoPath = "~/Product/Video";
    }
}