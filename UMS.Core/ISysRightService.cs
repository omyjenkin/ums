using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models;
using UMS.Models.DTOs;

namespace UMS.Core
{
   public partial interface ISysRightService
    {
        List<RightModuleDTO> GetRightByRoleAndModule(string roleId, string moduleId);

        int UpdateRight(SysRightOperate model);
    }
}
