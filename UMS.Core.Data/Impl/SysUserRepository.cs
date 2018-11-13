using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models;
using UMS.Models.DTOs;

namespace UMS.Core.Data
{
    public partial class SysUserRepository
    {
        public List<PermModel> GetPermission(string accountid, string controller)
        {
            var perms =
          (
              from a in EFContext.DbContext.SysRightOperate
              join b in EFContext.DbContext.SysRight on a.RightId equals b.Id
              join c in EFContext.DbContext.SysModule on b.ModuleId equals c.Id
              join d in EFContext.DbContext.SysUserRole on b.RoleId equals d.SysRoleId
              join e in EFContext.DbContext.SysUser on d.SysUserId equals e.Id
              where e.Id == accountid && c.Url == controller

              select new PermModel
              {
                  KeyCode = a.KeyCode,
                  IsValid = a.IsValid
              }
                    ).ToList();
            return perms;
        }

        public SysUser Login(string userName, string password)
        {

            SysUser user = EFContext.DbContext.SysUser.SingleOrDefault(a => a.Id == userName && a.Password == password);
            return user;

        }

        public int RunError(string id)
        {
            return 0;
        }

        public IQueryable<RoleDTO> GetRoleByUserId(string userId)
        {
            var roles = from a in EFContext.DbContext.SysRole
                        join b in EFContext.DbContext.SysUserRole on a.Id equals b.SysRoleId
                        into temp
                        from t in temp.DefaultIfEmpty()
                            //where t.SysUserId == userId
                        select new RoleDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Description = a.Description,
                            Flag = t != null
                        };
            return roles;
        }

        public void UpdateUserRole(string userId, string[] roleIds)
        {
            string sql = " delete SysUserRole where SysUserId = @p0 ";
            string updateSql = " insert into SysUserRole(Id,SysRoleId,SysUserId) values(newid(),@p0, @p1)";

            ExecSql(sql, userId);

            foreach (string roleid in roleIds)
            {
                if (!string.IsNullOrWhiteSpace(roleid))
                {
                    ExecSql(updateSql, roleid, userId);
                }
            }
        }
    }
}
