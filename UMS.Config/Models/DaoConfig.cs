using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UMS.Config
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    [Serializable]
    public class DaoConfig : ConfigFileBase
    {
        public DaoConfig()
        {
        }
        #region 序列化属性
        public String Account { get; set; }
        public String Log { get; set; } 
        public String BillDocument { get; set; }
        #endregion
    }
}
