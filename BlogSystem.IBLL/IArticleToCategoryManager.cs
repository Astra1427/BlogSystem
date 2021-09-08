using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.IBLL
{
    public interface IArticleToCategoryManager
    {
        Task Create(Guid articleId,Guid categoryId);
    }
}
