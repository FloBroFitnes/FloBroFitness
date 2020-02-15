using FBF.Service.ClassService;
using FloBroAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Script.Serialization;

namespace FloBroAPI.Controllers
{
    public class CatagoryController : ApiController
    {
        // GET: Catagory
        [HttpGet]
        [Route("api/Catagory/addorupdatecategory")]
        [ResponseType(typeof(string))]
        public IHttpActionResult AddCate(long ID,string name)
        {
            ResponseModel response = new ResponseModel();
            Dictionary<dynamic, dynamic> dic = new Dictionary<dynamic, dynamic>();
            bool result = Categories.addupdatecategory(ID,name);
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
        [Route("api/Catagory/deletecategory")]
        [ResponseType(typeof(string))]
        public IHttpActionResult DelCate(long ID)
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

    }
}