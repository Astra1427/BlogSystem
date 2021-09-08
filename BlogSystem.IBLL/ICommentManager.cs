using BlogSystem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.IBLL
{
    public interface ICommentManager
    {
        Task Create(Guid userId,string content,Guid articleId);
        Task Remove(Guid commentId);
        Task<List<CommentDTO>> GetComments(Guid articleId);
    }
}
