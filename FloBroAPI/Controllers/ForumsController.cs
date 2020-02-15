using FBF.Service.ClassService;
using FBF.Service.DBService;
using FloBroAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Script.Serialization;

namespace FloBroAPI.Controllers
{
    public class ForumsController : BaseAPIController
    {
        // GET: Forums
        
        [HttpPost]
        [Route("api/Forums/addUpdateForums")]
        public HttpResponseMessage addUpdateForums(Forum forums)
        {
            ResponseModel response = new ResponseModel();
            Forums.AddUpdate(forums);
            var jsons = new JavaScriptSerializer().Serialize(response);
            return this.Request.CreateResponse(HttpStatusCode.OK, jsons);
        }
        [HttpPost]
        [Route("api/Forums/addUpdateComments")]
        public HttpResponseMessage addUpdateComments(Comment comment)
        {
           var response= Forums.AddUpdateComment(comment);
            return this.Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpGet]
        [Route("api/Forums/ForumList")]
        public IHttpActionResult ForumList()
        {
            var json = Forums.GetForumList();

            return Ok(json);
        }

        [HttpGet]
        [Route("api/Forums/ForumListDetail")]
        public IHttpActionResult ForumListDetail()
        {
            var json = Forums.GetForumListDetail().OrderByDescending(c=>c.ForumId);
            return Ok(json);
        }


        

        [HttpGet]
        [Route("api/Forums/UpdateView")]
        public HttpResponseMessage UpdateView(long id)
        {
            ResponseModel response = new ResponseModel();
            Forums.UpdateView(id);
            var jsons = new JavaScriptSerializer().Serialize(response);
            return this.Request.CreateResponse(HttpStatusCode.OK, jsons);
        }
        [HttpGet]
        [Route("api/Forums/detail")]
        public IHttpActionResult detail(long id)
        {
            var response= Forums.GetForumDetail(id);
            return Ok(response);
        }
        [HttpGet]
        [Route("api/Forums/comment")]
        public IHttpActionResult Comments(long id)
        {
            var response = Forums.GetForumComments(id);
            return Ok(response);
        }  
        [HttpGet]
        [Route("api/Forums/Deletecomment")]
        public IHttpActionResult Deletecomment(long id)
        {
            var response = Forums.DeleteComment(id);
            return Ok(response);
        }  
        [HttpGet]
        [Route("api/Forums/MaxCommentId")]
        public IHttpActionResult MaxCommentId()
        {
            var response = Forums.MaxCommentId();
            return Ok(response);
        }


        //@fortype
        #region FormType
        [HttpPost]
        [Route("api/Forums/addUpdateForumsType")]
        public HttpResponseMessage addUpdateForumsType(ForumType forumType)
        {
            ResponseModel response = new ResponseModel();
            Forums.AddUpdateFormType(forumType);
            var jsons = new JavaScriptSerializer().Serialize(response);
            return this.Request.CreateResponse(HttpStatusCode.OK, jsons);
        }
        [HttpGet]
        [Route("api/Forums/ForumTypeList")]
        public HttpResponseMessage ForumTypeList()
        {
            var json = Forums.GetForumTypeList().OrderByDescending(c => c.TypeId).ToList();
            //var tes = Newtonsoft.Json.JsonConvert.SerializeObject(json, Formatting.Indented,
            //    new JsonSerializerSettings
            //    {
            //        PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //    }
            //    );
            //var jsons =  new JsonConvert().SerializeObject(json,Formatting.Indented ,new JsonSerializerSettings
            //{
            //    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //});
            return this.Request.CreateResponse(HttpStatusCode.OK, json);
        }
        #endregion
    }
}