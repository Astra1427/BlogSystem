using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DTO
{
    public class UserInformationDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        public string PortraitPath { get; set; }
        public string SiteName { get; set; }
        public int FansCount { get; set; }
        public int FocusCount { get; set; }
    }
}
