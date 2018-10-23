using System;
using System.Collections.Generic;
using UMS.Cache;

namespace UMS.Framework.Web
{
    public class ServiceContext
    {
        public static ServiceContext Current
        {
            get
            {
                return CacheHelper.GetItem<ServiceContext>("ServiceContext", () => new ServiceContext());
            }
        }

        //public ISysLoginInfoBLL AccountService
        //{
        //    get
        //    {
        //        return ServiceHelper.CreateService<IAccountService>();
        //    }
        //}

    }
}
