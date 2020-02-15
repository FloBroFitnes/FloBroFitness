using FBF.Service.DBService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FloBroFitness.WebUI.Models
{
    public class ForumsModel
    {
        public ForumsModel()
        {

            this.Comments = new HashSet<Comment>();

        }


        public long ForumId { get; set; }

        public string Name { get; set; }

        public Nullable<System.DateTime> CreatedOn { get; set; }

        public bool IsActive { get; set; }
        [AllowHtml]
        public string Body { get; set; }

        public Nullable<long> TypeId { get; set; }

        public string Activity { get; set; }

        public Nullable<int> UserView { get; set; }

        public Nullable<long> UserId { get; set; }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ForumType ForumType { get; set; }

        public virtual tblUser tblUser { get; set; }

    }

}