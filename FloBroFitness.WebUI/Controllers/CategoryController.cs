using FBF.Service.ClassService;
using FBF.Service.DBService;
using FloBroFitness.WebUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace FloBroFitness.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View(Categories.GetCategoryList());
        }
        [HttpPost]
        public ActionResult AddCategory(CategoryModel categoryModel)
        {
            var client2 = new WebClient();
            var result = client2.DownloadString("http://localhost:64718" + "/api/Catagory/addcategory?ID=" + categoryModel.ID + "&=name=" + categoryModel.Name);
            return View();
        }
        public ActionResult Create(CategoryModel categoryModel)
        {
            tblCategory category = new tblCategory();
            category.Name = categoryModel.Name;
            category.ID = categoryModel.ID;
            Categories.AddUpdate(category);
            return PartialView("_CategoryList", Categories.GetCategoryList());
        }
    }
}