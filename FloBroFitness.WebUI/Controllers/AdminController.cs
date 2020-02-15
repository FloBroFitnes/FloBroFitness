using FBF.Service.ClassService;
using FloBroFitness.WebUI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FloBroFitness.WebUI.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (AppConfig.Id > 0)
            {
                return View();
            }
            else
                return RedirectToAction("Login", "Account");

        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult ManageAthlete()
        {
            if (AppConfig.Id > 0)
            {
            return View(Accounts.GetAthleteList());
            }
            else
                return RedirectToAction("Login", "Account");
        }
        public ActionResult MangeUser()
        {
            if (AppConfig.Id > 0)
            {
            return View(Accounts.GetAdminUserList());
            }
            else
                return RedirectToAction("Login", "Account");
        }
    }
}