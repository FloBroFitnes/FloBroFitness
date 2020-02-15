using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FloBroFitness.WebUI.Models
{
    public class Blog
    {

        public long ID { get; set; }
        [Required(ErrorMessage ="Required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Date { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public string Tags { get; set; }
        [Required(ErrorMessage = "Required")]
        public Boolean? IsActive { get; set; }
        public long UserID { get; set; }
        public string Username { get; set; }
        public List<Blog> lstBlog { get; set; }
        public List<BlogComment> lstComments { get; set; }
    }

    public class BlogComment
    {
        public long ID { get; set; }
        public string Comment { get; set; }
        public string CustomerName { get; set; }
        public string Date { get; set; }
    }
}