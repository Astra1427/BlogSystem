using BlogSystem.MVCSite.Filters;
using BlogSystem.MVCSite.Models.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlogSystem.MVCSite.Controllers
{
    public class HomeController : Controller
    {
        [BlogSystemAuthorizeAttribute]
        public ActionResult Index()
        {
            return View();
        }

        [BlogSystemAuthorizeAttribute]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [BlogSystemAuthorizeAttribute]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Register() {
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<ActionResult> Register( RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //当数据有问题的时候，依然在这个View，把错误的数据在返回给用户
                return View(model);
            }
            IBLL.IUserManager userManager = new BLL.UserManager();
            try
            {
                await userManager.RegisterAsync(model.Email, model.Password);
                return Content("注册成功！");
            }
            catch (ArgumentException ae)
            {
                ModelState.AddModelError("",ae.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            IBLL.IUserManager userManager = new BLL.UserManager();
            Guid userId = Guid.Empty;
            if (userManager.Login(model.Email, model.Password,out userId))
            {
                if (model.RememberMe)
                {
                    //Request.Cookies.Add(new HttpCookie("loginName2") { 
                    //    Value = model.Email,
                    //    Expires = DateTime.Now.AddDays(7)
                    //});
                    Response.Cookies.Add(new HttpCookie("loginName")
                    {
                        Value = model.Email,
                        Expires = DateTime.Now.AddDays(7),
                    });
                    Response.Cookies.Add(new HttpCookie ("userID"){ 
                        Value = userId.ToString(),
                        Expires = DateTime.Now.AddDays(7)
                    });
                }
                else
                {
                    Session["loginName"] = model.Email;
                    Session["userId"] = userId;
                }

                if (Request.UrlReferrer == null)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    return Redirect(Request.UrlReferrer.AbsoluteUri);
                }

            }
            ModelState.AddModelError("","您的账号密码有误");
            return View(model);
            
        }

    }
}