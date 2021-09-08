using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.IDAL
{
    public interface IArticleToCategoryService:IBaseService<Models.ArticleToCategory>
    {
        Task RemoveArticleCategoriesAsync(Guid articleId, bool Save = true);
    }
}
