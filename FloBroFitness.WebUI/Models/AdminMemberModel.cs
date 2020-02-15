using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FloBroFitness.WebUI.Models
{
    public class AdminMemberModel
    {
        public Int64 ID { get; set; }
        [Required(ErrorMessage = "Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Required")]

        public string LastName { get; set; }
        //[Remote("DoesEmailExist", "Home", HttpMethod = "POST", ErrorMessage = "Email address already registered.")]

        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter a valid email address.")]
        [Required(ErrorMessage = "Required")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Required")]

        public string Password { get; set; }
    }
}