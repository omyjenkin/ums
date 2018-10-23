using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Models
{
    public class LoginInfoViewModel
    {
        public int Id { get; set; }
        public System.DateTime CreateTime { get; set; }
        public System.Guid LoginToken { get; set; }
        public System.DateTime LastAccessTime { get; set; }
        public int SysUserId { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string BusinessPermissionString { get; set; }
        public string ClientIP { get; set; }
        public int EnumLoginAccountType { get; set; }
        public bool RememberMe { get; set; }
    }
}
