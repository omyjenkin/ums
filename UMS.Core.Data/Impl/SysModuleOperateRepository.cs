using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models;

namespace UMS.Core.Data
{
    public partial class SysModuleOperateRepository
    {
        public List<SysModuleOperate> GetListByModuleId(string moduleId)
        {
            var list =
           (
               from m in EFContext.DbContext.SysModuleOperate
               where m.ModuleId == moduleId
               && m.Id != string.Empty
               select m
                     ).OrderBy(a => a.Sort).ToList();
            return list;

        }


        public bool IsExist(string id)
        {

            SysModuleOperate entity = GetByKey(id);
            if (entity != null)
                return true;
            return false;

        }
    }
}
