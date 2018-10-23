using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models;

namespace UMS.Core.Data
{
   public partial interface ISysModuleOperateRepository
    {
        List<SysModuleOperate> GetListByModuleId(string moduleId);
         
        bool IsExist(string id);
    }
}
