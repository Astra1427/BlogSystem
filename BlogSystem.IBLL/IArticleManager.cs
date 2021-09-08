using BlogSystem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.IBLL
{
    public interface IArticleManager
    {

        Task CreateArticle(string title,string content,Guid userId,Guid[] categories);
        Task ChangeArticle(Guid articleId,string title,string content,Guid[] categories);
        Task CreateBlogCategory(string categoryName,Guid userId);
        Task<List<BlogCategoryDto>> GetAllCategories(Guid userId);
        Task<List<ArticleDto>> GetAllArticles(Guid userId);
        Task<List<ArticleDto>> GetArticleByPage(Guid userId,int pageIndex,int pageSize, bool asc = true);
        Task<int> GetArticleCountByUser(Guid userId);
        Task<ArticleDto> GetArticleById(Guid articleId);
        Task GoodCount(Guid articleId);
        Task BadCount(Guid articleId);

    }
}
