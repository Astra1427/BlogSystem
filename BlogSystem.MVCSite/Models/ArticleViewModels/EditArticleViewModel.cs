using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace BlogSystem.MVCSite.Models.ArticleViewModels
{
    public class EditArticleViewModel
    {
        
        public Guid ID { get; set; }
        [Required, StringLength(100, MinimumLength = 2)]
        [Display(Name = "标题")]
        public string Title { get; set; }

        [Required, StringLength(500, MinimumLength = 10)]
        [Display(Name = "内容")]
        public string Content { get; set; }

        [Display(Name = "文章分类")]
        public Guid[] CategoryIds { get; set; }
    }
}