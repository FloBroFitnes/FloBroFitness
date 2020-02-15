using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FloBroFitness.WebUI.Models
{
    public class CategoryModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
    }
}