using FBF.Service.DBService;
using FloBroFitness.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBF.Service.ClassService;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Configuration;
using System.Net.Http.Headers;
using FloBroFitness.WebUI.Models.ViewModel;
using FloBroFitness.WebUI.Helper;


namespace FloBroFitness.WebUI.Controllers
{
    public class ForumTypeController : Controller
    {
        // GET: ForumType
        #region Formtype


        public ActionResult Index()
        {
            WebClient client = new WebClient();
            List<ForumTypeModel> res = new List<ForumTypeModel>();
            MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
            client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["URI"]).AbsoluteUri;
            var response = client.DownloadString("api/Forums/ForumTypeList");
            res = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ForumTypeModel>>(response);
            return View(res);
        }
        public ActionResult Create(ForumTypeModel forumsModel)
        {
            
            // add form detail
            ForumTypeModel forum = new ForumTypeModel();
            forum.Name = forumsModel.Name;
            forum.TypeId = forumsModel.TypeId;
            HttpClient client2 = new HttpClient();
            MediaTypeFormatter jsonFormatter2 = new JsonMediaTypeFormatter();
            HttpContent data = new ObjectContent<dynamic>(forum, jsonFormatter2);
            client2.BaseAddress = new Uri(WebConfigurationManager.AppSettings["URI"]);
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response2;
            try
            {
                response2 = client2.PostAsync("api/Forums/addUpdateForumsType", data).Result;

            }
            catch (Exception ex)
            {
                throw;

            }
            if (response2.IsSuccessStatusCode)
            {
                WebClient client4 = new WebClient();
                List<ForumTypeModel> res4 = new List<ForumTypeModel>();
                MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
                client4.BaseAddress = new Uri(WebConfigurationManager.AppSettings["URI"]).AbsoluteUri;
                var response4 = client4.DownloadString("api/Forums/ForumTypeList");
                res4 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ForumTypeModel>>(response4);

                return PartialView("_TypeList", res4);
            }
            else
            {
                return View();
            }
        }
        #endregion
    }
}