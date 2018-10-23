using System;
using UMS.Cache;
using UMS.Models; 

namespace UMS.Framework.Web
{
    public class UserContext
    {
        protected IAuthCookie authCookie;

        public UserContext(IAuthCookie authCookie)
        {
            this.authCookie = authCookie;
        }

        public SysLoginInfo LoginInfo
        {
            get
            {
                return CacheHelper.GetItem<SysLoginInfo>("LoginInfo", () =>
                {
                    if (string.IsNullOrEmpty(authCookie.UserToken))
                        return null;

                    var loginInfo = new SysLoginInfo();
                    //var loginInfo = ServiceContext.Current.AccountService.GetLoginInfo(authCookie.UserToken);

                    //if (loginInfo != null && loginInfo.USERID > 0 && loginInfo.USERID != this.authCookie.UserId)
                    //    throw new Exception("非法操作，试图通过网站修改Cookie取得用户信息！");

                    return loginInfo;
                });
            }
        }
    }
}
