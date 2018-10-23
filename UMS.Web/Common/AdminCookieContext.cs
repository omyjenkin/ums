using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UMS.Cache;
using UMS.Framework.Web;
using UMS.Utility;

namespace UMS.Web.Common
{
    public class AdminCookieContext : CookieContext
    {
        public static AdminCookieContext Current
        {
            get
            {
                return CacheHelper.GetItem<AdminCookieContext>();
            }
        }

        public override string KeyPrefix
        {
            get
            {
                return Fetch.ServerDomain + "_ProjectAdminContext_";
            }
        }
    }
}