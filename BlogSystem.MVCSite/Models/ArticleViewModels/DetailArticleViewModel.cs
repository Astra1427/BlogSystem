﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSystem.MVCSite.Models.ArticleViewModels
{
    public class DetailArticleViewModel
    {

        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public int GoodCount { get; set; }
        public int BadCount { get; set; }

        public string[] CategoryIds { get; set; }
        public string[] CategoryNames { get; set; }

        public Guid UserId { get; set; }
        public string UserPortraitPath { get; set; }
        public string UserEmail { get; set; }
    }
}