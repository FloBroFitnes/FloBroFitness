using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FloBroFitness.WebUI.Models.ViewModel
{
    public class CommentModel
    {
        public long CommentId { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public long ForumId { get; set; }

        public DateTime Createon { get; set; }

        public string IsActive { get; set; }
    }
}