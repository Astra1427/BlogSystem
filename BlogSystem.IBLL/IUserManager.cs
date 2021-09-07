using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.IBLL
{
    public interface IUserManager
    {

        Task RegisterAsync(string email, string password);
        Task<bool> LoginAsync(string email, string password);
        bool Login(string email, string password,out Guid userId);
        Task ChangePasswordAsync(string email, string oldPwd, string newPwd);
        Task ChangeUserInformation(string email, string newPortraitPath, string siteName);

        Task<DTO.UserInformationDTO> GetUserByEmail(string email);
    }
}
