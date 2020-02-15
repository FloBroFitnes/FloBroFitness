using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FloBroAPI.Models
{
    public class ResponseModel
    {
        public bool error { get; set; }
        public string message { get; set; }
        public dynamic data { get; set; }

    }
    public class ResponseMessageModel
    {
        public string response { get; set; }
        public string StatusDescription { get; set; }
        public dynamic data { get; set; }
    }

}