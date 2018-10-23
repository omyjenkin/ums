using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models;
using UMS.Models.DTOs;

namespace UMS.Core.Data
{
    public partial class SysRoleRepository
    {
        public ISysUserRoleRepository UserRoleRepository { get; set; }

        public int RunError(string id)
        {
            return 0;
        }

        public IQueryable<SysUser> GetRefSysUser(string roleId)
        {
            if (!string.IsNullOrEmpty(roleId))
            {
                return from m in EFContext.DbContext.SysUserRole
                       from f in EFContext.DbContext.SysUser
                       where m.SysUserId == f.Id && m.SysRoleId == roleId
                       select f;
            }
            return null;
        }

        public IQueryable<SysUser> GetUserByRoleId(string roleId)
        {

            var result = from u in EFContext.DbContext.SysUser
                         join ur in EFContext.DbContext.SysUserRole on u.Id equals ur.SysUserId
                         into joined
                         from m in joined.DefaultIfEmpty()
                         where m.SysRoleId == roleId
                         orderby m.SysRoleId descending
                         select u;

            return result;
            ;
        }

        public void UpdateSysRoleSysUser(string roleId, string[] userIds)
        {
            UserRoleRepository.Delete(m => m.SysRoleId == roleId, false); 
            foreach (string userid in userIds)
            {
                if (!string.IsNullOrWhiteSpace(userid))
                {

                    UserRoleRepository.Insert(new SysUserRole { Id = Guid.NewGuid(), SysRoleId = roleId, SysUserId = userid }, false);
                }
            }
            EFContext.Commit();

        }
    }
}
