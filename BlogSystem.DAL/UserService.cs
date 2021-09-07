using BlogSystem.IDAL;
using BlogSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DAL
{
    public class UserService :BaseService<Models.User>, IUserService
    {
        public UserService():base(new BlogContext())
        {


        }

        public async Task<bool> FindUserByEmailAsync(string email)
        {
            return await this.db.Users.AnyAsync(a=>a.Email ==  email);
        }
    }
}
