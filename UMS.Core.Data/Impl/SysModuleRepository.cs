using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models;
using UMS.Core.Data;


namespace UMS.Core.Data
{
    public partial class SysModuleRepository
    {
        
        public ISysRightRepository RightRepository { get; set; }
        
        public ISysRightOperateRepository RightOperateRepository { get; set; }
        
        public ISysModuleOperateRepository ModuleOperateRepository { get; set; }

        public List<SysModule> GetMenuByUserId(string userId, string moduleId)
        {
            var ma = from m in EFContext.DbContext.SysModule
                     join rl in RightRepository.Entities
                     on m.Id equals rl.ModuleId
                     join r in
                         (from r in EFContext.DbContext.SysRole
                          join ur in EFContext.DbContext.SysUserRole
                          on r.Id equals ur.SysRoleId
                          where ur.SysUserId == userId
                          select r)
                     on rl.RoleId equals r.Id
                     where rl.Rightflag == true
                     where m.ParentId == moduleId
                     where m.Id != "0"
                     select m;
            var menus =
            (
                from m in base.Entities
                join rl in EFContext.DbContext.SysRight
                on m.Id equals rl.ModuleId
                join r in
                    (from r in EFContext.DbContext.SysRole
                     join ur in EFContext.DbContext.SysUserRole
                     on r.Id equals ur.SysRoleId
                     where ur.SysUserId == userId
                     select r)
                on rl.RoleId equals r.Id
                where rl.Rightflag == true
                where m.ParentId == moduleId
                where m.Id != "0"
                select m
                      ).Distinct().OrderBy(m => m.Sort).ToList();
            return menus;
        }


        public IQueryable<SysModule> GetModuleBySystem(string parentId)
        {
            return EFContext.DbContext.SysModule.Where(a => a.ParentId == parentId).AsQueryable();
        }

        public void Delete(string id)
        {
            SysModule entity = EFContext.DbContext.SysModule.SingleOrDefault(a => a.Id == id);
            if (entity != null)
            {
                //删除SysRight表数据
                var sr = RightRepository.Entities.Where(a => a.ModuleId == id);
                foreach (var o in sr)
                {
                    //删除SysRightOperate表数据
                    var sro = RightOperateRepository.Entities.Where(a => a.RightId == o.Id);
                    foreach (var o2 in sro)
                    {
                        RightOperateRepository.Delete(o2);
                    }
                    RightRepository.Delete(o);
                }
                //删除SysModuleOperate数据
                var smo = ModuleOperateRepository.Entities.Where(a => a.ModuleId == id);
                foreach (var o3 in smo)
                {
                    ModuleOperateRepository.Delete(o3);
                }
               base.Delete(entity);
            }
        }


        public bool IsExist(string id)
        {

            SysModule entity = GetByKey(id);
            if (entity != null)
                return true;
            return false;

        }

    }
}
