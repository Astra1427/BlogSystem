using BlogSystem.IDAL;
using BlogSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DAL
{
    public class ArticleService : BaseService<Models.Article>, IArticleService
    {
        public ArticleService():base(new BlogContext())
        {

        }

        public IQueryable<Article> GetArticleByPage(Guid userId, int pageIndex, int pageSize,bool asc = true)
        {
            var aticles = this.GetAllOrderAsync(asc).Where(a=>a.UserId == userId) ;
            return aticles.Skip(pageIndex*pageSize).Take(pageSize);
        }
    }
}
