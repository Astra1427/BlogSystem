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

    }
}
