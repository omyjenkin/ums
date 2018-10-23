using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models;

namespace UMS.Core
{
    public partial class SysModuleOperateService
    {
        public List<SysModuleOperate> GetListByModuleId(string moduleId)
        {
            return CurrentRepository.GetListByModuleId(moduleId);
        }

        private List<SysModuleOperate> CreateModelList(ref IQueryable<SysModuleOperate> queryData)
        {

            List<SysModuleOperate> modelList = (from r in queryData
                                                select new SysModuleOperate
                                                {
                                                    Id = r.Id,
                                                    Name = r.Name,
                                                    KeyCode = r.KeyCode,
                                                    ModuleId = r.ModuleId,
                                                    IsValid = r.IsValid,
                                                    Sort = r.Sort
                                                }).ToList();
            return modelList;
        }

        public bool Create(ref string error, SysModuleOperate model)
        {

            if (Insert(model) == 1)
            {
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

            if (Delete(id) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool IsExists(string id)
        {
            if (BaseRepository.Entities.SingleOrDefault(a => a.Id == id) != null)
            {
                return true;
            }
            return false;
        }

        public SysModuleOperate GetById(string id)
        {
            if (IsExist(id))
            {
                SysModuleOperate entity = GetById(id);
                SysModuleOperate model = new SysModuleOperate();
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.KeyCode = entity.KeyCode;
                model.ModuleId = entity.ModuleId;
                model.IsValid = entity.IsValid;
                model.Sort = entity.Sort;
                return model;
            }
            else
            {
                return null;
            }
        }

        public bool IsExist(string id)
        {
            return IsExist(id);
        }
    }
}
