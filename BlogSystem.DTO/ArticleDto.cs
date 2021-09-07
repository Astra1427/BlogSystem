using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DTO
{
    public class ArticleDto
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public int GoodCount { get; set; }
        public int BadCount { get; set; }

        public Guid[] CategoryIds { get; set; }
        public string[] CategoryNames { get; set; }

        public Guid UserId { get; set; }
        public string UserPortraitPath { get; set; }
        public string UserEmail { get; set; }

    }
}
