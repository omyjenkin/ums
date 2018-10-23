using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models;
using UMS.Models.DTOs;

namespace UMS.Core.Data
{
   public partial interface ISysRightRepository
    {
       
        int UpdateRight(SysRightOperate model); 

        List<RightModuleDTO> GetRightByRoleAndModule(string roleId, string moduleId);
    }
}
