//using FBF.Service.ClassService;
using FBF.Service.ClassService;
using FloBroAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Script.Serialization;

namespace FloBroAPI.Controllers
{
    public class ProductController : ApiController
    {
        // GET: Product
        [HttpGet]
        [Route("api/Product/addorupdateproduct")]
        [ResponseType(typeof(string))]
        public IHttpActionResult AddCate(long ID, string name)
        {
            ResponseModel response = new ResponseModel();
            Dictionary<dynamic, dynamic> dic = new Dictionary<dynamic, dynamic>();
            bool result = Categories.addupdatecategory(ID, name);
            if (result)
            {
                response.error = false;
                response.message = "category added successfully.";
                dic.Add("Category Name", name);
                response.data = dic;
            }
            else
            {
                response.error = true;
                response.message = "category already exists.";
                dic.Add("Category Name", name);
                response.data = dic;
            }
            var json = new JavaScriptSerializer().Serialize(response);
            return Ok(json);
        }

        [HttpGet]
        [Route("api/Catagory/deleteproduct")]
        [ResponseType(typeof(string))]
        public IHttpActionResult DelPro(long ID)
        {
            ResponseModel response = new ResponseModel();
            Dictionary<dynamic, dynamic> dic = new Dictionary<dynamic, dynamic>();
            bool result = Categories.deletecategory(ID);
            if (result)
            {
                response.error = false;
                response.message = "category deleted successfully.";
            }
            else
            {
                response.error = true;
                response.message = "something went wrong";
            }
            var json = new JavaScriptSerializer().Serialize(response);
            return Ok(response);
        }

        [HttpPost]
        [Route("api/Product/postproduct")]
        [ResponseType(typeof(string))]
        public IHttpActionResult PostPro([System.Web.Http.FromBody] HttpPostedFileBase files)
        {
            ResponseModel response = new ResponseModel();
            Dictionary<dynamic, dynamic> dic = new Dictionary<dynamic, dynamic>();
            bool result = true;
            if (result)
            {
                response.error = false;
                response.message = "category deleted successfully.";
            }
            else
            {
                response.error = true;
                response.message = "something went wrong";
            }
            var json = new JavaScriptSerializer().Serialize(response);
            return Ok(response);
        }
    }
}