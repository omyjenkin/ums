using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models;
using UMS.Models.DTOs;

namespace UMS.Core
{
    public partial class SysRightService
    {
        /// <summary>
        /// 按选择的角色及模块加载模块的权限项
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public List<RightModuleDTO> GetRightByRoleAndModule(string roleId, string moduleId)
        {
            return CurrentRepository.GetRightByRoleAndModule(roleId, moduleId);
        }

        public int UpdateRight(SysRightOperate model)
        {
            return CurrentRepository.UpdateRight(model);
        }
    }
}
