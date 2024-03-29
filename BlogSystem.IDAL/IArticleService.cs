﻿using BlogSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.IDAL
{
    public interface IArticleService : IBaseService<Models.Article>
    {
        IQueryable<Models.Article> GetArticleByPage(Guid userId,int pageIndex,int pageSize, bool asc = true);
    }
}
