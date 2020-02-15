using FBF.Service.DBService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FBF.Service.ViewModel
{
    [KnownTypeAttribute(typeof(ForumVm))]
    public class ForumVm
    {

        public Forum Forum { get; set; }
        public ForumType ForumType { get; set; }
        public tblUser tblUsers { get; set; }

    }
}
