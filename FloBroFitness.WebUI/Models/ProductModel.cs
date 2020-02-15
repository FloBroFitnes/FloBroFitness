using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FloBroFitness.WebUI.Models
{
    public class ProductModel
    {
        public long ID { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        [Required(ErrorMessage = "Required")]
        public long CategoryID { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Size { get; set; }
        [Required(ErrorMessage = "Required")]
        public decimal Price { get; set; }
    }
}