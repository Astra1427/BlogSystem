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
    [BlogSystemAuthorize]
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

        [BlogSystemAuthorize]
        public async Task<ActionResult> ListArticle(int pageIndex=0,int pageSize = 1)
        {
            var userId = Guid.Parse(Session["userId"].ToString());
            IBLL.IArticleManager articleManager = new BLL.ArticleManager();
            var articles = await articleManager.GetArticleByPage(userId,pageIndex,pageSize);

            int totalArticleCount = await articleManager.GetArticleCountByUser(userId);

            int pageCount = totalArticleCount % pageSize == 0 ? totalArticleCount / pageSize : (totalArticleCount / pageSize) + 1;

            ViewBag.PageCount = pageCount;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;

            return View(articles);
        }


        [HttpGet]
        [BlogSystemAuthorize]
        public async Task<ActionResult> CreateArticle()
        {
            var userId = Guid.Parse(Session["userId"].ToString());
            ViewBag.Categories = await new BLL.ArticleManager().GetAllCategories(userId);
            return View();
        }

        [BlogSystemAuthorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateArticle(CreateArticleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("","添加失败");
                return View(model);
            }

            IBLL.IArticleManager articleManager = new BLL.ArticleManager();
            await articleManager.CreateArticle(model.Title,model.Content,Guid.Parse(Session["userId"].ToString()),model.CategoryIds);
            return RedirectToAction(nameof(ListArticle));
        }

        [HttpGet]
        public async Task<ActionResult> EditArticle(Guid articleId)
        {
            IBLL.IArticleManager articleManager = new BLL.ArticleManager();
            var article = await articleManager.GetArticleById(articleId);
            var articleViewModel = new EditArticleViewModel()
            {
                ID = article.ID,
                Title = article.Title,
                Content = article.Content,
                CategoryIds = article.CategoryIds
            };
            return View(articleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditArticle(EditArticleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("","Edit Failed!Please check all data!");
                return View(model);
            }
            IBLL.IArticleManager articleManager = new BLL.ArticleManager();
            await articleManager.ChangeArticle(model.ID,model.Title,model.Content,model.CategoryIds);
            return RedirectToAction(nameof(ListArticle));

        }

        [HttpGet]
        public async Task< ActionResult> DetailArticle(Guid? articleId)
        {
            if (!articleId.HasValue)
            {
                ModelState.AddModelError("", "文章不存在！");
                return RedirectToAction(nameof(ListArticle));

            }

            IBLL.IArticleManager articleManager = new BLL.ArticleManager();
            var articleDto = await articleManager.GetArticleById(articleId.Value);
            if (articleDto == null)
            {
                ModelState.AddModelError("","文章不存在！");
                return RedirectToAction(nameof(ListArticle)) ;
            }

            ViewBag.CategoryNames = articleDto.CategoryNames;

            return View(articleDto);
        }



    }
}
