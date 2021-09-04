using BlogSystem.MVCSite.Models.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlogSystem.MVCSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

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
            await userManager.RegisterAsync(model.Email, model.Password);
            return Content("注册成功！");
        }

    }
}