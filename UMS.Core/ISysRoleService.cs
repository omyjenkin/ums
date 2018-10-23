using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models;

namespace UMS.Core
{
   public partial interface ISysRoleService
    {
        string GetRefSysUser(string roleId);

        IQueryable<SysUser> GetUserByRoleId(ref GridPager pager, string roleId);

        bool UpdateSysRoleSysUser(string roleId, string[] userIds);
    }
}
