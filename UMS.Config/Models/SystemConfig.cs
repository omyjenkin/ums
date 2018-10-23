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
    public class SystemConfig : ConfigFileBase
    {
        public SystemConfig()
        {
        }

        #region 序列化属性
        public int UserLoginTimeoutMinutes { get; set; }

        public string ServerIp { get; set; }

        public int ListenPort { get; set; }

        #endregion
    }
}
