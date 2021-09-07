using BlogSystem.DTO;
using BlogSystem.IBLL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BLL
{
    public class ArticleManager : IArticleManager
    {
        public async Task ChangeArticle(Guid articleId,string title, string content, Guid[] categories)
        {
            using (IDAL.IArticleService artSvc = new DAL.ArticleService())
            {
                var article = await artSvc.FindAsync(articleId);

                article.Title = title;
                article.Content = content;
                


                await artSvc.EditAsync(article);
            }

        }

        public async Task CreateArticle(string title, string content, Guid userId, Guid[] categories)
        {
            using (IDAL.IArticleService articleSvc = new DAL.ArticleService())
            {
                var article = new Models.Article
                {
                    Title = title,
                    Content = content,
                    UserId = userId,
                    CreateTime = DateTime.Now,
                };
                await articleSvc.CreateAsync(article);

                using (IDAL.IArticleToCategoryService atcSvc = new DAL.ArticleToCategoryService())
                {
                    for (int i = 0; i < categories.Length; i++)
                    {
                        await atcSvc.CreateAsync(new Models.ArticleToCategory { 
                            ArticleId = article.Id,
                            BlogCategoryId = categories[i]
                        });
                    }
                    await atcSvc.SaveChangesAsync();
                }
            }
        }

        public async Task CreateBlogCategory(string categoryName,Guid userId)
        {
            using (IDAL.IBlogCategoryService bcsSvc = new DAL.BlogCategoryService())
            {
                await bcsSvc.CreateAsync(new Models.BlogCategory { 
                    CategoryName = categoryName,
                    UserId = userId,
                });
            }
        }

        public async Task<List<ArticleDto>> GetAllArticles(Guid userId)
        {
            using (IDAL.IArticleService articleSvc = new DAL.ArticleService())
            {
                var articles = await articleSvc.GetAll().Include(a=>a.User).Where(a=>a.UserId == userId).Select(a=>new ArticleDto { 
                    ID = a.Id,
                    Title = a.Title,
                    CreateTime = a.CreateTime,
                    UserId = a.UserId,
                    Content = a.Content,
                    GoodCount = a.GoodCount,
                    BadCount = a.BadCount,
                    UserEmail = a.User.Email,
                    UserPortraitPath = a.User.PortraitPath,
                }).ToListAsync();
                foreach (var item in articles)
                {
                    item.CategoryIds = await GetCategoryIdsByArticleIDAsync(item.ID);
                    item.CategoryNames = await GetCategoryNamesByArticleIDAsync(item.ID);
                }

                //articles.ForEach(async a=> {
                //    a.CategoryIds = await GetCategoryIdsByArticleIDAsync(a.ID);
                //    a.CategoryNames = await GetCategoryNamesByArticleIDAsync(a.ID);
                //});
                return articles;
            }
        }



        public async Task<Guid[]> GetCategoryIdsByArticleIDAsync(Guid articleId)
        {
            using (IDAL.IArticleToCategoryService atcSvc = new DAL.ArticleToCategoryService())
            {
                return await atcSvc.GetAll().Where(a => a.ArticleId == articleId).Select(a => a.BlogCategoryId).ToArrayAsync();
            }
        }
        public async Task<string[]> GetCategoryNamesByArticleIDAsync(Guid articleId)
        {
            using (IDAL.IArticleToCategoryService atcSvc = new DAL.ArticleToCategoryService())
            {
                return await atcSvc.GetAll().Include(a=>a.BlogCategory).Where(a => a.ArticleId == articleId).Select(a => a.BlogCategory.CategoryName).ToArrayAsync();
            }
        }


        public async Task<List<BlogCategoryDto>> GetAllCategories(Guid userId)
        {
            using (IDAL.IBlogCategoryService categorySvc = new DAL.BlogCategoryService())
            {
                return await categorySvc.GetAll().Where(a=>a.UserId == userId).Select(a=>new BlogCategoryDto { 
                    Id = a.Id,
                    CategoryName = a.CategoryName
                }).ToListAsync();
            }
        }

        public async Task<ArticleDto> GetArticleById(Guid articleId)
        {
            using (IDAL.IArticleService articleSvc = new DAL.ArticleService())
            {
                var article = await articleSvc.GetAll().Include(a=>a.User).FirstOrDefaultAsync(a=>a.Id == articleId);
                if (article == null)
                {
                    return null;
                }
                var articleDto = new ArticleDto
                {
                    ID = article.Id,
                    Title = article.Title,
                    CreateTime = article.CreateTime,
                    UserId = article.UserId,
                    Content = article.Content,
                    GoodCount = article.GoodCount,
                    BadCount = article.BadCount,
                    UserEmail = article.User.Email,
                    UserPortraitPath = article.User.PortraitPath,
                };

                articleDto.CategoryNames = await GetCategoryNamesByArticleIDAsync(articleId);
                articleDto.CategoryIds = await GetCategoryIdsByArticleIDAsync(articleId);
                return articleDto;
            }
        }

        public async Task<List<ArticleDto>> GetArticleByPage(Guid userId, int pageIndex, int pageSize, bool asc = true)
        {
            using (IDAL.IArticleService articleSvc = new DAL.ArticleService())
            {
                var articles = await articleSvc.GetArticleByPage(userId,pageIndex,pageSize, asc).Include(a=>a.User).Select(a=>new ArticleDto { 
                    Title = a.Title,
                    Content = a.Content,
                    BadCount = a.BadCount,
                    GoodCount = a.GoodCount,
                    CreateTime = a.CreateTime,
                    UserEmail = a.User.Email,
                    UserId = a.UserId,
                    UserPortraitPath = a.User.PortraitPath,
                }).ToListAsync();
                foreach (var item in articles)
                {
                    item.CategoryIds = await GetCategoryIdsByArticleIDAsync(item.ID);
                    item.CategoryNames = await GetCategoryNamesByArticleIDAsync(item.ID);
                }
                return articles;
            }
        }

        public async Task<int> GetArticleCountByUser(Guid userId)
        {
            using (IDAL.IArticleService articleSvc = new DAL.ArticleService())
            {
                return await articleSvc.GetAll().CountAsync(a => a.UserId == userId);
            }
        }
    }
}
