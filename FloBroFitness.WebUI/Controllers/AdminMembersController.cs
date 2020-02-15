using FloBroFitness.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FloBroFitness.WebUI.Controllers
{
    public class AdminMembersController : Controller
    {
        // GET: AdminMembers
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddEdit(Int64? ID)
        {
            AdminMemberModel model = new AdminMemberModel();
            return View(model);
        }
    }
}