using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.DynamicProxy;
using UMS.Log;
using UMS.Models;

namespace UMS.Web.Interceptor
{
    public class ExceptionInterceptor : IInterceptor
    {
        public ExceptionInterceptor()
        {

        }

        /// <summary>
        /// 拦截方法
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception exception)
            {

                SysLog message = new SysLog
                {
                    Category = LoggerType.SystemLog.ToString(),
                    Message = exception.Message,
                    Exception = exception.StackTrace,
                    Method = exception.TargetSite.Name,
                    Params = string.Join(",", (invocation.Arguments.Length > 0) ? invocation.Arguments[0].ToString() : null),
                    CreateTime = DateTime.Now
                };

                Logger.Debug(LoggerType.SystemLog, message, exception);

            }
        }
    }
}