using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models;


namespace UMS.Core
{
    public partial class SysRoleService
    {
        /// <summary>
        /// 获取角色对应的所有用户
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        public string GetRefSysUser(string roleId)
        {
            string UserName = "";
            var userList = CurrentRepository.GetRefSysUser(roleId);
            if (userList != null)
            {
                foreach (var user in userList)
                {
                    UserName += "[" + user.UserName + "] ";
                }
            }
            return UserName;
        }

        public IQueryable<SysUser> GetUserByRoleId(ref GridPager pager, string roleId)
        {
            IQueryable<SysUser> queryData = CurrentRepository.GetUserByRoleId(roleId);
            pager.totalRows = queryData.Count();
            return queryData.Skip((pager.page - 1) * pager.rows).Take(pager.rows);
        }

        public bool UpdateSysRoleSysUser(string roleId, string[] userIds)
        {
            CurrentRepository.UpdateSysRoleSysUser(roleId, userIds);
            return true;

        }
    }
}
