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
    public class ForumController : Controller
    {

        public ActionResult Create()
        {
            if (AppConfig.Id > 0)
            {
                WebClient client = new WebClient();
                List<ForumType> res = new List<ForumType>();
                MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
                client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["URI"]).AbsoluteUri;
                var response = client.DownloadString("api/Forums/ForumTypeList");
                res = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ForumType>>(response);
                ViewBag.TypeId = new SelectList(res, "TypeId", "Name");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpPost]
        public ActionResult Create(ForumsModel forumsModel)
        {
            WebClient client = new WebClient();
            List<ForumType> res = new List<ForumType>();
            MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
            client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["URI"]).AbsoluteUri;
            var response = client.DownloadString("api/Forums/ForumTypeList");
            res = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ForumType>>(response);
            ViewBag.TypeId = new SelectList(res, "TypeId", "Name");
            // add form detail
            Forum forum = new Forum();
            forum.Name = forumsModel.Name;
            forum.Body = forumsModel.Body;
            forum.TypeId = forumsModel.TypeId;
            forum.UserId = AppConfig.Id;
            forum.ForumId = forumsModel.ForumId;
            forum.ForumId = forumsModel.ForumId;
            ViewBag.ForumListCount = Forums.GetForumList().Count;
            ViewBag.ForumList = Forums.GetForumList().OrderByDescending(c => c.ForumId);
            HttpClient client2 = new HttpClient();
            MediaTypeFormatter jsonFormatter2 = new JsonMediaTypeFormatter();
            HttpContent data = new ObjectContent<dynamic>(forum, jsonFormatter);
            client2.BaseAddress = new Uri(WebConfigurationManager.AppSettings["URI"]);
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response2;
            try
            {
                response2 = client2.PostAsync("api/Forums/addUpdateForums", data).Result;

            }
            catch (Exception ex)
            {
                throw;

            }
            if (response2.IsSuccessStatusCode)
            {
                return RedirectToAction("index", "Forum");
            }
            else
            {
                return View();
            }
        }
        // GET: Forum
        public ActionResult Index()
        {
            WebClient client = new WebClient();
            List<Forum> res = new List<Forum>();
            MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
            client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["URI"]).AbsoluteUri;
            var response = client.DownloadString("api/Forums/ForumListDetail");
            res = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Forum>>(response);
            ViewBag.ForumList = res.OrderByDescending(c => c.ForumId);
            return View(res);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Index(ForumsModel forumsModel)
        {
            Forum forum = new Forum();
            forum.Name = forumsModel.Name;
            forum.Body = forumsModel.Body;
            forum.ForumId = forumsModel.ForumId;
            ViewBag.ForumListCount = Forums.GetForumList().Count;
            ViewBag.ForumList = Forums.GetForumList().OrderByDescending(c => c.ForumId);
            HttpClient client = new HttpClient();
            MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
            HttpContent data = new ObjectContent<dynamic>(forum, jsonFormatter);
            client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["URI"]);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            try
            {
                response = client.PostAsync("api/Forums/addUpdateForums", data).Result;
            }
            catch (Exception ex)
            {
                throw;

            }
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index", "Forum");
            }
            else
            {
                return View();
            }


            //return PartialView("_CategoryList", Categories.GetCategoryList());
        }


        public ActionResult detail(long id)
        {
            if (AppConfig.Id > 0)
            {
                ForumsModel forum = new ForumsModel();
                WebClient client = new WebClient();
                List<ForumType> res = new List<ForumType>();
                MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
                client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["URI"]).AbsoluteUri;
                var response = client.DownloadString("api/Forums/UpdateView?id=" + id);
                //from detail

                ViewBag.id = id;
                ViewBag.sessionId = AppConfig.Id;
                var response2 = client.DownloadString("api/Forums/detail?id=" + id);
                forum = Newtonsoft.Json.JsonConvert.DeserializeObject<ForumsModel>(response2);



                //Newtonsoft.Json.JsonConvert.DeserializeObject<ForumsModel>(response);
                return View(forum);
            }
            else
                return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public JsonResult GetComments(long id)
        {
            WebClient client = new WebClient();
            MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
            client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["URI"]).AbsoluteUri;
            var response3 = client.DownloadString("api/Forums/comment?id=" + id);
            var forum3 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Comment>>(response3);
            return Json(forum3, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult CommentAdd(Comment comment)
        {
            Comment commentObj = new Comment();
            commentObj.Createon = DateTime.Now;
            commentObj.UserId = AppConfig.Id;
            commentObj.ForumId = comment.ForumId;
            commentObj.Name = comment.Name;
            HttpClient client = new HttpClient();
            MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
            HttpContent data = new ObjectContent<dynamic>(commentObj, jsonFormatter);
            client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["URI"]);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            try
            {
                response = client.PostAsync("api/Forums/addUpdateComments", data).Result;

            }
            catch (Exception ex)
            {
                throw;

            }
            if (response.IsSuccessStatusCode)
            {
                WebClient client2 = new WebClient();
                MediaTypeFormatter jsonFormatter2 = new JsonMediaTypeFormatter();
                client2.BaseAddress = new Uri(WebConfigurationManager.AppSettings["URI"]).AbsoluteUri;
                var response3 = client2.DownloadString("api/Forums/MaxCommentId");
                return Json(response3);

            }
            else
            {
                return Json(false);

            }
        }
        [HttpGet]
        public JsonResult DeleteComments(long id)
        {
            WebClient client = new WebClient();
            MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
            client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["URI"]).AbsoluteUri;
            var response3 = client.DownloadString("api/Forums/Deletecomment?id=" + id);
            return Json(response3, JsonRequestBehavior.AllowGet);

        }

        //admin area
        public ActionResult forumindex()
        {
            WebClient client = new WebClient();
            List<Forum> res = new List<Forum>();
            MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
            client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["URI"]).AbsoluteUri;
            var response = client.DownloadString("api/Forums/ForumListDetail");
            res = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Forum>>(response);
            ViewBag.ForumList = res.OrderByDescending(c => c.ForumId);
            return View(res);

        }

        public ActionResult Forumdetail(long id)
        {
            if (AppConfig.Id > 0)
            {
                ForumsModel forum = new ForumsModel();
                WebClient client = new WebClient();
                List<ForumType> res = new List<ForumType>();
                MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
                client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["URI"]).AbsoluteUri;
                var response = client.DownloadString("api/Forums/UpdateView?id=" + id);
                //from detail

                ViewBag.id = id;
                ViewBag.sessionId = AppConfig.Id;
                var response2 = client.DownloadString("api/Forums/detail?id=" + id);
                forum = Newtonsoft.Json.JsonConvert.DeserializeObject<ForumsModel>(response2);



                //Newtonsoft.Json.JsonConvert.DeserializeObject<ForumsModel>(response);
                return View(forum);
            }
            else
                return RedirectToAction("Login", "Account");
        }

    }
}