using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using FBF.Service.ClassService;
using FloBroFitness.WebUI.Models;
using FBF.Service.DBService;
using FloBroFitness.WebUI.Helper;

namespace FloBroFitness.WebUI.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PostUserAsync(string name, string pic, HttpPostedFileBase files)
        {
            string Host = "http://localhost:64718/api/Product/postproduct";
            var client = new HttpClient();
            files = Request.Files[0];
            string pics = System.IO.Path.GetFileName(files.FileName);
            string path = System.IO.Path.Combine(
                                   Server.MapPath("~/images"), pics);
            // file is uploaded
            files.SaveAs(path);

            // save the image path path to the database or you can send image 
            // directly to database
            // in-case if you want to store byte[] ie. for DB
            MemoryStream ms = new MemoryStream();

            files.InputStream.CopyTo(ms);
            byte[] file = ms.GetBuffer();


            //byte[] file = File.ReadAllBytes(files.Path);
            //WebClient webClient = new WebClient();
            //var fileData = webClient.Encoding.GetString(file);
            var fileName = System.IO.Path.GetFileNameWithoutExtension(path);
            //string extension = System.IO.Path.GetExtension(fileName);

            MultipartFormDataContent form = new MultipartFormDataContent();
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            MemoryStream mss = new MemoryStream();
            fs.CopyTo(mss);

            var streamContent = new StreamContent(fs);

            var imageContent = new ByteArrayContent(streamContent.ReadAsByteArrayAsync().Result);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

            form.Add(imageContent, "Picture", Path.GetFileName(path));

            var response = client.PostAsync(Host, form).Result; ///
            string res = "";
            using (HttpContent content = response.Content)
            {
                // ... Read the string.
                var result = content.ReadAsStringAsync();
                res = result.Result;
            }
            var jsoss = JsonConvert.DeserializeObject(res);
            //var rootObj = JsonConvert.DeserializeObject<PostPicture>(File.ReadAllText(res));
            string f = jsoss.ToString();
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Category = Categories.GetCategoryList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProductModel model)
        {
            tblProduct product = new tblProduct();
            product.CategoryId = model.CategoryID;
            product.Description = model.Description;
            product.ID = model.ID;
            product.Name = model.Name;
            product.Price = model.Price;
            product.Size = model.Size;
            var filelist = Request.Files;
            List<tblProductFile> lstProductFiles = new List<tblProductFile>();
            tblProductFile productFile; 
            if (filelist.Count > 0)
            {
                var imagePath = Constant.ProductFileImagePath;
                var videoPath = Constant.ProductFileVideoPath;
                if (Directory.Exists(Server.MapPath(imagePath)))
                {
                    Directory.CreateDirectory(Server.MapPath(imagePath));
                }
                if (Directory.Exists(Server.MapPath(videoPath)))
                {
                    Directory.CreateDirectory(Server.MapPath(videoPath));
                }

                for (int i = 0; i < filelist.Count; i++)
                {
                productFile = new tblProductFile();
                    var file = filelist[i];
                    if (!string.IsNullOrEmpty(file.FileName))
                    {
                        string fileExt = (Path.GetExtension(file.FileName)).ToLower();
                        productFile.Name = file.FileName;

                    }

                }
            }
            return View();
        }
        public Boolean ValidatePictureExtention(String pictureExtension)
        {
            if (pictureExtension != ".jpg" && pictureExtension != ".png" && pictureExtension != ".gif" && pictureExtension != ".jpeg" && pictureExtension != ".bmp")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boolean ValidateVideoExtention(String videoExtension)
        {
            if (videoExtension != ".3gp" && videoExtension != ".mp4" && videoExtension != ".flv" && videoExtension != ".avi" && videoExtension != ".mpeg")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}