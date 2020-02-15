using Facebook;
using FBF.Service.ClassService;
using FBF.Service.DBService;
using FloBroFitness.WebUI.Helper;
using FloBroFitness.WebUI.Models;
using System;
using System.IO;
using System.Web.Mvc;

namespace FloBroFitness.WebUI.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            try
            {
                var user = Accounts.Login(model.Email, model.Passworrd);
                if (user != null)
                {
                    if (user.StatusID == Constant.StatusID.Approved)
                    {
                        SetSession(user);
                        if (user.IsAdmin == true)
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else if (user.StatusID == Constant.StatusID.Pending)
                    {
                        TempData["Pending"] = "Your account is pending on admin.";
                    }
                }
                else
                {
                    TempData["Invalid"] = "Inavlid";
                }

            }
            catch (Exception ee)
            {

                throw;
            }

            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            
            RegisterationModel model = new RegisterationModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Register(RegisterationModel model)
        {
            tblUser user = new tblUser();
            if (model != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.UserID = model.UserID;
                user.Passworrd = model.Passworrd;
                user.Email = model.Email;
                user.StatusID = model.StatusID;
                user.IsAthlete = model.IsAthlete;
                user.IsAdmin = model.IsAdmin;
                user.CreatedDate = DateTime.UtcNow;
                if (model.IsAdmin == true)
                {
                    var path = Constant.AdminRegisterCVFilePath;
                    if (!Directory.Exists(Server.MapPath(path)))
                    {
                        Directory.CreateDirectory(Server.MapPath(path));
                    }
                    var file = Request.Files["CV"];
                    if (file != null & file.ContentLength > 0)
                    {
                        user.DocumentFile = Common.AdminCVFile(path, file);
                    }
                }
                try
                {
                    Accounts.Register(user);
                    if (model.IsAdmin == true)
                    {
                        return RedirectToAction("Verify", "Account");
                    }
                }
                catch (Exception exx)
                {

                    throw;
                }
                return RedirectToAction("Login", "Account");

            }
            return View();
        }
        public JsonResult Verification(Int64 ID,int StatusId)
        {
            Accounts.AccountVerification(ID, StatusId);

            return Json("", JsonRequestBehavior.AllowGet);
        }
        #region FB_Login

        private Uri RediredtUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        [AllowAnonymous]
        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            // Fazeel
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "2533780036870162",
                client_secret = "eba354f7eee01838cd374530d2c5b2cd",
                redirect_uri = RediredtUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });
            //var loginUrl = fb.GetLoginUrl(new
            //{
            //    client_id = "2533780036870162",
            //    client_secret = "eba354f7eee01838cd374530d2c5b2cd",
            //    redirect_uri = RediredtUri.AbsoluteUri,
            //    response_type = "code",
            //    scope = "email"
            //});
            // Azid
            //var loginUrl = fb.GetLoginUrl(new
            //{
            //    client_id = "1918290168475652",
            //    client_secret = "2600becf94b2ab5dec9fc3ac5a251e40",
            //    redirect_uri = RediredtUri.AbsoluteUri,
            //    response_type = "code",
            //    scope = "email"
            //});
            //if (string.IsNullOrEmpty(Login))
            //{
            return Json(loginUrl.AbsoluteUri, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return Redirect(loginUrl.AbsoluteUri);
            //}
        }
        public ActionResult FacebookCallback(string code)
        {
            try
            {
                var fb = new FacebookClient();

                //Fazeel
                dynamic result = fb.Post("oauth/access_token", new
                {
                    client_id = "2533780036870162",
                    client_secret = "eba354f7eee01838cd374530d2c5b2cd",
                    redirect_uri = RediredtUri.AbsoluteUri,
                    code = code
                });
                //dynamic result = fb.Post("oauth/access_token", new
                //{
                //    client_id = "2533780036870162",
                //    client_secret = "eba354f7eee01838cd374530d2c5b2cd",
                //    redirect_uri = RediredtUri.AbsoluteUri,
                //    code = code
                //});
                // Azid
                //dynamic result = fb.Post("oauth/access_token", new
                //{
                //    client_id = "1918290168475652",
                //    client_secret = "2600becf94b2ab5dec9fc3ac5a251e40",
                //    redirect_uri = RediredtUri.AbsoluteUri,
                //    code = code
                //});

                var accessToken = result.access_token;
                Session["AccessToken"] = accessToken;
                fb.AccessToken = accessToken;
                dynamic me = fb.Get("me?fields=link,first_name,currency,last_name,email,gender,locale,timezone,verified,picture,age_range");
                string email = me.email;
                tblUser user = new tblUser();
                user.FirstName = me.first_name;
                user.LastName = me.last_name;
                user.Email = email;
                user.CreatedDate = Common.GetDateTime();
            }
            catch (Exception es)
            {

            }
            return RedirectToAction("Login", "Home");
        }


        #endregion

        [HttpPost]
        public JsonResult DoesEmailExist(string Email)
        {
            if (!string.IsNullOrEmpty(Email))
            {
                return Json(Accounts.EmailExist(Email.ToLower()), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
        public ActionResult Verify()
        {
            return View();
        }
        public void SetSession(tblUser user)
        {
            Session["Id"] = user.ID;
            Session["UserId"] = user.UserID;
            Session["Name"] = user.FirstName + " " + user.LastName;
        }
    }
}