using BlogSystem.MVCSite.Filters;
using BlogSystem.MVCSite.Models.ArticleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlogSystem.MVCSite.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        [BlogSystemAuthorize]
        public ActionResult Index()
        {
            return View();
        }
        [BlogSystemAuthorize]
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [BlogSystemAuthorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCategory(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("","您输入的信息有误！");
                return View(model);
            }
            IBLL.IArticleManager articleManager = new BLL.ArticleManager();
            await articleManager.CreateBlogCategory(model.CategoryName,Guid.Parse(Session["userId"].ToString()));
            return RedirectToAction(nameof(ListCategories));
        }

        [BlogSystemAuthorize]
        public async Task<ActionResult> ListCategories()
        {
            var userId = Guid.Parse(Session["userId"].ToString());
            IBLL.IArticleManager articleManager = new BLL.ArticleManager();
            return View(await articleManager.GetAllCategories(userId));
        }
    }
}
