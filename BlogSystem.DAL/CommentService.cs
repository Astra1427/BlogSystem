using BlogSystem.IDAL;
using BlogSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DAL
{
    public class CommentService :BaseService<Models.Comment>, ICommentService
    {
        public CommentService():base(new BlogContext())
        {

        }

        public new async Task RemoveAsync(Guid id, bool saved = true)
        {
            db.Comments.Remove(await db.Comments.FindAsync(id));
            if (saved)
            {
                await db.SaveChangesAsync();
            }
        }
    }
}
