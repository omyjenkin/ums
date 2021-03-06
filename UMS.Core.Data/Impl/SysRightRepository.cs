﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models;
using UMS.Models.DTOs;
using UMS.Core.Data;
using System.Data.SqlClient;

namespace UMS.Core.Data
{
    public partial class SysRightRepository
    {

        public ISysRightOperateRepository RightOperateRepository { get; set; }

        public ISysModuleRepository ModuleRepository { get; set; }

        public int UpdateRight(SysRightOperate model)
        {
            //转换
            SysRightOperate rightOperate = new SysRightOperate();
            rightOperate.Id = model.Id;
            rightOperate.RightId = model.RightId;
            rightOperate.KeyCode = model.KeyCode;
            rightOperate.IsValid = model.IsValid;
            //判断rightOperate是否存在，如果存在就更新rightOperate,否则就添加一条

            SysRightOperate right = EFContext.DbContext.SysRightOperate.Where(a => a.Id == rightOperate.Id).FirstOrDefault();
            if (right != null)
            {
                right.IsValid = rightOperate.IsValid;
            }
            else
            {
                //RightOperateRepository.Insert(rightOperate);
                EFContext.DbContext.SysRightOperate.Add(rightOperate);
            }
            if (EFContext.DbContext.SaveChanges() > 0)
            {
                //更新角色--模块的有效标志RightFlag
                var sysRight = (from r in EFContext.DbContext.SysRight
                                where r.Id == rightOperate.RightId
                                select r).First();

                int rightflag = 0;
                string sql1 = " select  COUNT(*) from SysRightOperate where RightId = @p0 + @p1 and IsValid = 1";
                string sql2 = " update SysRight set Rightflag = @p0 where ModuleId = @p1 and RoleId = @p2";
                string sql3 = @" select COUNT(*) from SysRight where ModuleId in
                (select Id from SysModule where ParentId = @p0)
                and RoleId = @p1
                and Rightflag = 1";

                if (ExecSql(sql1, sysRight.RoleId, sysRight.ModuleId) > 0)
                {
                    rightflag = 1;
                }
                ExecSql(sql2, rightflag, sysRight.RoleId, sysRight.ModuleId);


                //计算下一层
                string parentId = sysRight.ModuleId;
                SysModule module = ModuleRepository.GetByKey(parentId);

                while (parentId != "0")
                {
                    parentId = module.ParentId;
                    if (parentId == null)
                        break;
                    if (ExecSql(sql3, sysRight.RoleId, sysRight.ModuleId) > 0)
                    {
                        rightflag = 1;
                    }
                    else
                    {
                        rightflag = 0;
                    }
                    ExecSql(sql2, rightflag, sysRight.RoleId, sysRight.ModuleId);
                }

                return 1;
            }

            return 0;
        }

        /// <summary>
        /// 按选择的角色及模块加载模块的权限项
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public List<RightModuleDTO> GetRightByRoleAndModule(string roleId, string moduleId)
        {
            List<RightModuleDTO> result = null;

            string sql = @" select a.Id, a.Name, a.KeyCode, a.ModuleId, ISNULL(b.IsValid,0) as IsValid,a.Sort,@p0+@p1 as RightId
             from SysModuleOperate a
             left outer join(
                 select c.Id, a.IsValid from SysRightOperate a, SysRight b, SysModuleOperate c
                 where RightId in
                 (select Id From SysRight where RoleId = @p0 and ModuleId = @p1)
                  and a.RightId=b.Id
                  and b.ModuleId= c.ModuleId
                  and a.KeyCode = c.KeyCode) b
            on a.Id = b.Id
            where a.ModuleId =@p1";

            result = QuerySql<RightModuleDTO>(sql, roleId, moduleId).ToList();


            return result;
        }
    }
}
