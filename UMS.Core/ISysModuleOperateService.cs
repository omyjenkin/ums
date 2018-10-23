using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models;

namespace UMS.Core
{
   public partial interface ISysModuleOperateService
    {
        List<SysModuleOperate> GetListByModuleId(string moduleId);

        bool Create(ref string error, SysModuleOperate model);
        bool Delete(ref string error, string id);
        SysModuleOperate GetById(string id);
        bool IsExist(string id);

    }
}
