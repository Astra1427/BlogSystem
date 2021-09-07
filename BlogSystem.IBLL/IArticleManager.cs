﻿using BlogSystem.DTO;
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
    }
}
