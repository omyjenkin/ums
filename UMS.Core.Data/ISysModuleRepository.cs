using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models;

namespace UMS.Core.Data
{
   public partial interface ISysModuleRepository
    {
        List<SysModule> GetMenuByUserId(string userId, string moduleId);

        IQueryable<SysModule> GetModuleBySystem( string parentId);
      
        void Delete(string id);
        bool IsExist(string id);

    }
}
