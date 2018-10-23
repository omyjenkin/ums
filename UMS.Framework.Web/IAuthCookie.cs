using System;

namespace UMS.Framework.Web
{
    public interface IAuthCookie
    {
        int UserExpiresHours { get; set; }
        
        string UserName { get; set; }

        int UserId { get; set; }

        string UserToken { get; set; }

        string VerifyCode { get; set; }

        int LoginErrorTimes { get; set; }

        bool IsNeedVerifyCode { get; }
    }
}
