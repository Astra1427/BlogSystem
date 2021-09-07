using BlogSystem.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BLL
{
    public class ArticleToCategoryManager : IArticleToCategoryManager
    {
        public async Task Create(Guid articleId, Guid categoryId)
        {
            using (IDAL.IArticleToCategoryService atcSvc = new DAL.ArticleToCategoryService())
            {
                await atcSvc.CreateAsync(new Models.ArticleToCategory { 
                    ArticleId = articleId,
                    BlogCategoryId = categoryId,
                },false);
            }
        }


    }
}
