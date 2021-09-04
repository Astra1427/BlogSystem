using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Models
{
    public class Article:BaseEntity
    {

        [Required]
        public string Title { get; set; }
        [Required,Column(TypeName = "nvarchar(max)")]
        public string Content { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }

        public int GoodCount { get; set; }
        public int BadCount { get; set; }



    }
}
