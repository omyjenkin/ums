using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models;

namespace UMS.Core.Data
{
    public partial interface ISysRoleRepository
    {
        //int RunError(string id);

        IQueryable<SysUser> GetRefSysUser(string roleId);

        IQueryable<SysUser> GetUserByRoleId(string roleId);

        void UpdateSysRoleSysUser(string roleId, string[] userIds);
    }
}
