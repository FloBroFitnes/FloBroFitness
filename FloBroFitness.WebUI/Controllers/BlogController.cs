using FBF.Service.DBService;
using FloBroFitness.WebUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace FloBroFitness.WebUI.Controllers
{
    public class BlogController : Controller
    {
        private string baseAddress = WebConfigurationManager.AppSettings["URI"].ToString();
        // GET: Blog
        public ActionResult Index(long? id)
        {
            Blog model = new Blog();
            if (id.HasValue)
            {
                tblBlog blog = new tblBlog();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    var result = client.GetAsync("api/Blog/GetBlogByID/" + id).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var deserializedResult = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                        blog = JsonConvert.DeserializeObject<tblBlog>(deserializedResult);
                    }
                }
                if (blog != null)
                {
                    model.ID = blog.ID;
                    model.Date = blog.Date.Value.ToShortDateString();
                    model.IsActive = blog.IsActive ?? false;
                    model.Tags = blog.Tags;
                    model.Title = blog.Title;
                    model.Username = blog.tblUser.FirstName + " " + blog.tblUser.LastName;
                    model.Content = blog.Discription;
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(Blog model)
        {
            model.UserID = 1;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                var result = client.PostAsync("api/Blog/AddBlog", new StringContent(
    new JavaScriptSerializer().Serialize(model), Encoding.UTF8, "application/json")).Result;
                if (result.IsSuccessStatusCode)
                {
                    var responseMessage = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                    if (responseMessage.Contains("not"))
                    {
                        TempData["Error"] = responseMessage;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Success"] = responseMessage;
                        return RedirectToAction("Blogs");
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Blogs()
        {
            List<tblBlog> lstBlogs = new List<tblBlog>();
            Blog model = new Blog();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                var result = client.GetAsync("api/Blog/GetBlogs").Result;
                if (result.IsSuccessStatusCode)
                {
                    var deserializedResult = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                    lstBlogs = JsonConvert.DeserializeObject<List<tblBlog>>(deserializedResult);

                }
            }
            if (lstBlogs.Count > 0)
            {
                Blog obj;
                List<Blog> lstBlog = new List<Blog>();
                foreach (var blog in lstBlogs)
                {
                    obj = new Blog();
                    obj.ID = blog.ID;
                    obj.Date = blog.Date.Value.ToShortDateString();
                    obj.IsActive = blog.IsActive ?? false;
                    obj.Tags = blog.Tags;
                    obj.Title = blog.Title;
                    obj.Username = blog.tblUser.FirstName + " " + blog.tblUser.LastName;
                    lstBlog.Add(obj);
                }
                model.lstBlog = lstBlog;
            }
            else
            {
                model.lstBlog = new List<Blog>();
            }
            return View(model);
        }

        public ActionResult DeleteBlog(long id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                var result = client.GetAsync("api/Blog/DeleteBlog/" + id).Result;
                if (result.IsSuccessStatusCode)
                {
                    var responseMessage = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                    if (responseMessage.Contains("not"))
                    {
                        TempData["Error"] = responseMessage;
                    }
                    else
                    {
                        TempData["Success"] = responseMessage;
                    }
                    return RedirectToAction("Blogs");
                }
                else
                {
                    return RedirectToAction("Blogs");
                }
            }
        }

        public ActionResult CustomerBlogs()
        {
            List<tblBlog> lstBlogs = new List<tblBlog>();
            Blog model = new Blog();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                var result = client.GetAsync("api/Blog/GetActiveBlogs").Result;
                if (result.IsSuccessStatusCode)
                {
                    var deserializedResult = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                    lstBlogs = JsonConvert.DeserializeObject<List<tblBlog>>(deserializedResult);

                }
            }
            if (lstBlogs.Count > 0)
            {
                Blog obj;
                List<Blog> lstBlog = new List<Blog>();
                foreach (var blog in lstBlogs)
                {
                    obj = new Blog();
                    obj.ID = blog.ID;
                    obj.Date = blog.Date.Value.ToShortDateString();
                    obj.IsActive = blog.IsActive ?? false;
                    obj.Tags = blog.Tags;
                    obj.Title = blog.Title;
                    obj.Username = blog.tblUser.FirstName + " " + blog.tblUser.LastName;
                    lstBlog.Add(obj);
                }
                model.lstBlog = lstBlog;
            }
            else
            {
                model.lstBlog = new List<Blog>();
            }
            return View(model);
        }

        public ActionResult BlogDetail(long id)
        {
            ViewBag.IsCustomerLogin = Session["ID"] != null && !Convert.ToBoolean(Session["IsAdmin"]);
            Blog model = new Blog();
            tblBlog blog = new tblBlog();
            List<tblBlogComment> lstBlogComments = new List<tblBlogComment>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                var result = client.GetAsync("api/Blog/GetBlogByID/" + id).Result;
                if (result.IsSuccessStatusCode)
                {
                    var deserializedResult = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                    blog = JsonConvert.DeserializeObject<tblBlog>(deserializedResult);
                }
                if (blog != null)
                {
                    var commentResult = client.GetAsync("api/Blog/GetComments/" + blog.ID).Result;
                    if (commentResult.IsSuccessStatusCode)
                    {
                        var deserializedResult = JsonConvert.DeserializeObject(commentResult.Content.ReadAsStringAsync().Result).ToString();
                        lstBlogComments = JsonConvert.DeserializeObject<List<tblBlogComment>>(deserializedResult);
                    }
                }
            }
            if (blog != null)
            {
                model.ID = blog.ID;
                model.Date = blog.Date.Value.ToShortDateString();
                model.IsActive = blog.IsActive ?? false;
                model.Tags = blog.Tags;
                model.Title = blog.Title;
                model.Username = blog.tblUser.FirstName + " " + blog.tblUser.LastName;
                model.Content = blog.Discription;

                if (lstBlogComments != null && lstBlogComments.Count > 0)
                {
                    BlogComment blogComment;
                    List<BlogComment> lstComments = new List<BlogComment>();
                    foreach (var comment in lstBlogComments)
                    {
                        blogComment = new BlogComment();
                        blogComment.Comment = comment.Comment;
                        blogComment.CustomerName = comment.tblUser.FirstName + " " + comment.tblUser.LastName;
                        blogComment.Date = comment.Date.Value.ToShortDateString();
                        lstComments.Add(blogComment);
                    }
                    model.lstComments = lstComments;
                }
                else
                {
                    model.lstComments = new List<BlogComment>();
                }
            }
            return View(model);
        }

        public PartialViewResult AddComment(long blogID, string comment)
        {
            Blog model = new Blog();
            List<tblBlogComment> lstBlogComments = new List<tblBlogComment>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                int customerID = Convert.ToInt32(Session["ID"]);
                DateTime date = Helper.Common.GetDateTime();
                var result = client.GetAsync("api/Blog/AddComment/" + blogID + "/" + customerID + "/" + comment).Result;
                if (result.IsSuccessStatusCode)
                {
                    var deserializedResult = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                    lstBlogComments = JsonConvert.DeserializeObject<List<tblBlogComment>>(deserializedResult);
                    if (lstBlogComments != null && lstBlogComments.Count > 0)
                    {
                        BlogComment blogComment;
                        List<BlogComment> lstComments = new List<BlogComment>();
                        foreach (var comments in lstBlogComments)
                        {
                            blogComment = new BlogComment();
                            blogComment.Comment = comments.Comment;
                            blogComment.CustomerName = comments.tblUser.FirstName + " " + comments.tblUser.LastName;
                            blogComment.Date = comments.Date.Value.ToShortDateString();
                            lstComments.Add(blogComment);
                        }
                        model.lstComments = lstComments;
                    }
                    else
                    {
                        model.lstComments = new List<BlogComment>();
                    }
                }
            }
            return PartialView("_BlogComments", model);
        }
    }
}