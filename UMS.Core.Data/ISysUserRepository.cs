using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models;
using UMS.Models.DTOs;

namespace UMS.Core.Data
{
   public partial interface ISysUserRepository
    {
        List<PermModel> GetPermission(string accountid, string controller);

        SysUser Login(string username, string password);

        int RunError(string id);

        IQueryable<RoleDTO> GetRoleByUserId(string userId);

        void UpdateUserRole(string userId, string[] roleIds);
    }
}
