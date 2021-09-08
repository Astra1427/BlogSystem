using BlogSystem.DTO;
using BlogSystem.IBLL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BLL
{
    public class CommentManager : ICommentManager
    {
        public async Task Create(Guid userId, string content, Guid articleId)
        {
            using (IDAL.ICommentService commentSvc = new DAL.CommentService())
            {
                await commentSvc.CreateAsync(new Models.Comment { 
                    UserId = userId,
                    ArticleId = articleId,
                    Content = content
                });
            }
        }

        public async Task<List<CommentDTO>> GetComments(Guid articleId)
        {
            using (IDAL.ICommentService commentSvc = new DAL.CommentService())
            {
                return await commentSvc.GetAllOrderAsync(false).Where(a => a.ArticleId == articleId).Include(a => a.User).Select(a=>new CommentDTO { 
                    ArticleId = a.ArticleId,
                    UserId = a.UserId,
                    Content = a.Content,
                    Id = a.Id,
                    UserPortraitPath = a.User.PortraitPath
                }).ToListAsync();
            }
        }

        public async Task Remove(Guid commentId)
        {
            using (IDAL.ICommentService commentSvc = new DAL.CommentService())
            {
                await commentSvc.RemoveAsync(commentId);
            }
        }
    }
}
