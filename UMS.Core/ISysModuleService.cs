using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models;

namespace UMS.Core
{
    public partial interface ISysModuleService
    {
        List<SysModule> GetMenuByUserId(string userId, string moduleId);
        List<SysModule> GetModuleBySystem(string parentId);
        bool Create(ref string error, SysModule model);
        bool Delete(ref string error, string id);
        bool Edit(ref string error, SysModule model);
        SysModule GetById(string id);
        bool IsExist(string id);
    }
}
