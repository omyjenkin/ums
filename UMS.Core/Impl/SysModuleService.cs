using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models;

namespace UMS.Core
{
    public partial class SysModuleService
    {
        public List<SysModule> GetMenuByUserId(string userId, string moduleId)
        {
            //queryData = LinqHelper.DataSorting(queryData,pager.sort,pager.order);
            return CurrentRepository.GetMenuByUserId(userId, moduleId);
        }

        private List<SysModule> CreateModelList(ref IQueryable<SysModule> queryData)
        {
            List<SysModule> modelList = (from r in queryData
                                         select new SysModule
                                         {
                                             Id = r.Id,
                                             Name = r.Name,
                                             EnglishName = r.EnglishName,
                                             ParentId = r.ParentId,
                                             Url = r.Url,
                                             Iconic = r.Iconic,
                                             Sort = r.Sort,
                                             Remark = r.Remark,
                                             Enable = r.Enable,
                                             CreateUser = r.CreateUser,
                                             CreateTime = r.CreateTime,
                                             IsLast = r.IsLast
                                         }).ToList();
            return modelList;
        }


        public List<SysModule> GetModuleBySystem(string parentId)
        {

            return CurrentRepository.GetModuleBySystem(parentId).ToList();
        }

        public bool Create(ref string error, SysModule model)
        {
            if (Insert(model) == 1)
            {
                //分配给角色
                string sql = @"insert into SysRight(Id, ModuleId, RoleId, Rightflag)
        select distinct b.Id + a.Id,a.Id,b.Id,0 from SysModule a,SysRole b
        where a.Id + b.Id not in(select ModuleId + RoleId from SysRight)";
                BaseRepository.ExecSql(sql);

                return true;
            }
            else
            {
                error = Suggestion.InsertFail;
                return false;
            }

        }

        public bool Delete(ref string error, string id)
        {

            //检查是否有下级
            if (CurrentRepository.Entities.Where(a => a.Id == id).Count() > 0)
            {
                error = "有下属关联，请先删除下属！";
                return false;
            }

            if (Delete(id) > 0)
            {
                //清理无用的项
                //db.P_Sys_ClearUnusedRightOperate();
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool Edit(ref string error, SysModule model)
        {

            SysModule entity = GetById(model.Id);
            if (entity == null)
            {
                error = Suggestion.Disable;

                return false;
            }
            entity.Name = model.Name;
            entity.EnglishName = model.EnglishName;
            entity.ParentId = model.ParentId;
            entity.Url = model.Url;
            entity.Iconic = model.Iconic;
            entity.Sort = model.Sort;
            entity.Remark = model.Remark;
            entity.Enable = model.Enable;
            entity.IsLast = model.IsLast;

            if (Update(entity) == 1)
            {
                return true;
            }
            else
            {
                error = Suggestion.EditFail;
                return false;
            }

        }

        public SysModule GetById(string id)
        {
            return GetById(id);
        }

        public bool IsExist(string id)
        {
            return IsExist(id);
        }
    }
}
