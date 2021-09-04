using BlogSystem.DTO;
using BlogSystem.IBLL;
using BlogSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BLL
{
    public class UserManager : IUserManager
    {

        public async Task RegisterAsync(string email, string password)
        {
            using (IDAL.IUserService userSvc = new DAL.UserService())
            {
                await userSvc.CreateAsync(new User 
                { 
                    Email = email,
                    Password = password ,
                    SiteName = "默认的小站",
                    PortraitPath="default.png"
                });
            }
        }
        public async Task ChangePasswordAsync(string email, string oldPwd, string newPwd)
        {
            using (IDAL.IUserService userSvc = new DAL.UserService())
            {
                var user = await userSvc.GetAllAsync().FirstOrDefaultAsync(a=>a.Email == email);
                if (user == null)
                {
                    return;
                }
                user.Password = newPwd;
                await userSvc.EditAsync(user); 
            }
        }

        public async Task ChangeUserInformation(string email, string newPortraitPath, string siteName)
        {
            using (IDAL.IUserService userSvc = new DAL.UserService())
            {
                var user = await userSvc.GetAllAsync().FirstOrDefaultAsync(a=>a.Email == email) ;
                if (user == null)
                {
                    return;
                }
                user.PortraitPath = newPortraitPath;
                user.SiteName = siteName;
                await userSvc.EditAsync(user);
            }
        }

        public async Task<UserInformationDTO> GetUserByEmail(string email)
        {
            using (IDAL.IUserService userSvc = new DAL.UserService())
            {
                var user = await userSvc.GetAllAsync().FirstOrDefaultAsync(a => a.Email == email);
                if (user == null)
                {
                    throw new ArgumentException("邮箱地址不存在");
                }
                return new UserInformationDTO {
                    Id = user.Id,
                    Email = user.Email,
                    PortraitPath = user.PortraitPath,
                    FansCount = user.FansCount,
                    FocusCount = user.FocusCount,
                    SiteName = user.SiteName
                };
            }
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            using (IDAL.IUserService userSvc = new DAL.UserService())
            {
                return await userSvc.GetAllAsync().AnyAsync(a=>a.Email == email && a.Password == password);
            }
        }

    }
}
