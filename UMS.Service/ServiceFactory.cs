﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Cache;
using UMS.Utility;

namespace UMS.Service
{
    public abstract class ServiceFactory
    {
        public abstract T CreateService<T>() where T : class;
    }

    //public class UnityServiceFactory:ServiceFactory
    //{
    //    public override T CreateService<T>()
    //    {
           
    //    }
    //}

    /// <summary>
    /// 直接引用提供服务
    /// </summary>
    public class RefServiceFactory : ServiceFactory
    {
        public override T CreateService<T>()
        {
            //第一次通过反射创建服务实例，然后缓存住
            var interfaceName = typeof(T).Name;
            return CacheHelper.Get<T>(string.Format("Service_{0}", interfaceName), () =>
            {
                return AssemblyHelper.FindTypeByInterface<T>();
            });
        }
    }

    /// <summary>
    /// 通过Wcf提供服务
    /// </summary>
    public class WcfServiceFactory : ServiceFactory
    {
        public override T CreateService<T>()
        {
            //TODO
            //需实现WCF Uri来自配置文件
            var uri = string.Empty;
            var proxy = WcfServiceProxy.CreateServiceProxy<T>(uri);
            return proxy;
        }
    }
}
