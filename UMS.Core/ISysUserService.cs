using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models;
using UMS.Models.DTOs;

namespace UMS.Core
{
   public partial interface ISysUserService
    {
        IEnumerable<SysUser> GetList();

        IQueryable<SysUser> GetPagedList(GridPager pager);

        SysUser Login(string loginName, string password);

        List<PermModel> GetPermission(string accountid, string controller);

        IQueryable<RoleDTO> GetRoleByUserId(ref GridPager pager, string userId);

        bool UpdateUserRole(string userId, string[] roleIds);
    }
}
