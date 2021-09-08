using BlogSystem.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DAL
{
    public class ArticleToCategoryService:BaseService<Models.ArticleToCategory>,IArticleToCategoryService
    {
        public ArticleToCategoryService():base(new Models.BlogContext())
        {

        }

        public async Task RemoveArticleCategoriesAsync(Guid articleId,bool Save = true)
        {
            var removed = this.db.ArticleToCategories.Where(a => a.ArticleId == articleId);
            db.ArticleToCategories.RemoveRange(removed);
            if (Save)
            {
                await SaveChangesAsync();
            }
        }
    }
}
