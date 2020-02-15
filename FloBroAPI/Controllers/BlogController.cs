using FBF.Service.DBService;
using FloBroFitness.WebUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace FloBroAPI.Controllers
{
    public class BlogController : ApiController
    {

        [HttpPost]
        [Route("api/Blog/AddBlog")]
        [ResponseType(typeof(string))]
        public IHttpActionResult AddBlog([FromBody] Blog model)
        {
            tblBlog blog = new tblBlog();
            blog.Title = model.Title;
            blog.Discription = model.Content;
            blog.Date = Convert.ToDateTime(model.Date);
            blog.UserID = model.UserID;
            blog.Tags = model.Tags;
            blog.IsActive = model.IsActive;
            blog.ID = model.ID;
            var result = String.Empty;
            if (model.ID > 0)
            {
                if (FBF.Service.ClassService.Blog.Update(blog))
                {
                    result = "Blog has been updated successfully.";
                }
                else
                {
                    result = "Blog has not been updated.";
                }
            }
            else
            {
                if (FBF.Service.ClassService.Blog.Add(blog))
                {
                    result = "Blog has been added successfully.";
                }
                else
                {
                    result = "Blog has not been added.";
                }
            }
            return Content(HttpStatusCode.OK, result);
        }


        [HttpGet]
       // [Route("api/Blog/AddComment")]
        [Route("api/Blog/AddComment/{blogID}/{customerID}/{comment}")]
        public IHttpActionResult AddComment(long blogID,long customerID,string comment)
        {
            tblBlogComment blogComment = new tblBlogComment();
            blogComment.BlogID = blogID;
            blogComment.Comment = comment;
            blogComment.CustomerID = customerID;
            blogComment.Date = FloBroFitness.WebUI.Helper.Common.GetDateTime() ;
             FBF.Service.ClassService.Blog.AddComment(blogComment);
            var lstBlogComments = FBF.Service.ClassService.Blog.GetCommentsByBlogID(blogID);
            string sterializedlLtBlogComments = JsonConvert.SerializeObject(lstBlogComments, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Ok(sterializedlLtBlogComments);
        }
        [HttpGet]
        [Route("api/Blog/GetComments/{blogID}")]
        public IHttpActionResult GetComments(long blogID)
        {
            var lstBlogComments = FBF.Service.ClassService.Blog.GetCommentsByBlogID(blogID);
            string sterializedlLtBlogComments = JsonConvert.SerializeObject(lstBlogComments, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Ok(sterializedlLtBlogComments);
        }
        [HttpGet]
        [Route("api/Blog/GetBlogs")]
        public IHttpActionResult GetBlogs()
        {
            var lstBlogs = FBF.Service.ClassService.Blog.GetAllBlogs();
            string sterializedLstBlog = JsonConvert.SerializeObject(lstBlogs, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Ok(sterializedLstBlog);
        }

        [HttpGet]
        [Route("api/Blog/GetActiveBlogs")]
        public IHttpActionResult GetActiveBlogs()
        {
            var lstBlogs = FBF.Service.ClassService.Blog.GetAllActiveBlogs();
            string sterializedLstBlog = JsonConvert.SerializeObject(lstBlogs, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Ok(sterializedLstBlog);
        }

        [HttpGet]
        [Route("api/Blog/GetBlogByID/{id}")]
        public IHttpActionResult GetBlogByID(long id)
        {
            var blog = FBF.Service.ClassService.Blog.GetByID(id);
            string sterializedBlog = String.Empty;
            if (blog != null)
            {
                 sterializedBlog = JsonConvert.SerializeObject(blog, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
            return Ok(sterializedBlog);
        }

        [HttpGet]
        [Route("api/Blog/DeleteBlog/{id}")]
        public IHttpActionResult DeleteBlog(long id)
        {
            var result =String.Empty;
             if (FBF.Service.ClassService.Blog.Delete(id))
            {
                result = "Blog has been deleted successfully.";
            }
            else
            {
                result = "Blog has not deleted added.";
            }
            return Ok(result);
        }
    }
}
