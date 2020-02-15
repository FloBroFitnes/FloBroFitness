﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FloBroFitness.WebUI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Required")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Required")]

        public string Passworrd { get; set; }
    }
}