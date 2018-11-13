using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UMS.Framework.Web;
using UMS.Models;
using UMS.Utility;
using UMS.Web.Common;

namespace UMS.Web.Controllers
{
    public class BaseController : WebControllerBase
    {

        /// <summary>
        /// 操作人，为了记录操作历史
        /// </summary>
        public override Operater Operater
        {
            get
            {
                return new Operater()
                {
                    Name = this.LoginInfo == null ? "" : this.LoginInfo.LoginName,
                    Token = this.LoginInfo == null ? Guid.Empty : this.LoginInfo.LoginToken,
                    UserId = this.LoginInfo == null ? string.Empty : this.LoginInfo.SysUserId,
                    Time = DateTime.Now,
                    IP = Fetch.UserIp
                };
            }
        }

        public AdminCookieContext CookieContext
        {
            get
            {
                return AdminCookieContext.Current;
            }
        }

        public AdminUserContext UserContext
        {
            get
            {
                return AdminUserContext.Current;
            }
        }

        /// <summary>
        /// 用户Token，每次页面都会把这个UserToken标识发送到服务端认证
        /// </summary>
        public virtual string UserToken
        {
            get
            {
                return CookieContext.UserToken;
            }
        }

        /// <summary>
        /// 登录后用户信息
        /// </summary>
        public virtual SysLoginInfo LoginInfo
        {
            get
            {
                return UserContext.LoginInfo;
            }
        }


        /// <summary>
        /// 重写基类在Action之前执行的方法
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            #region -----校验用户是否登录进入网站的-----

            //var noAuthorizeAttributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AuthorizeIgnoreAttribute), false);
            //if (noAuthorizeAttributes.Length > 0)
            //    return;

            

            //if (this.LoginInfo == null)
            //{
            //    filterContext.Result = RedirectToAction("Login", "Account");
            //    return;
            //}
            base.OnActionExecuting(filterContext);

            //bool hasPermission = true;
            //var permissionAttributes = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(PermissionAttribute), false).Cast<PermissionAttribute>();
            //permissionAttributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof(PermissionAttribute), false).Cast<PermissionAttribute>().Union(permissionAttributes);
            //var attributes = permissionAttributes as IList<PermissionAttribute> ?? permissionAttributes.ToList();
            //if (permissionAttributes != null && attributes.Count() > 0)
            //{
            //    hasPermission = true;
            //    foreach (var attr in attributes)
            //    {
            //        foreach (var permission in attr.Permissions)
            //        {
            //            if (!this.LoginInfo.BusinessPermissionList.Contains(permission))
            //            {
            //                hasPermission = false;
            //                break;
            //            }
            //        }
            //    }

            //    if (!hasPermission)
            //    {

            //        //if (Request.UrlReferrer != null)
            //        //    filterContext.Result = this.Stop("没有权限！", Request.UrlReferrer.AbsoluteUri);

            //        //else
            //        filterContext.Result = Content("<font style='color:Red'>没有权限！</font>");
            //    }


            #endregion


            #region ------//留个接口------
            //if (this.LoginInfo.UName == "admin")
            //{
            //    return;
            //}
            #endregion

        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        public void EndRequest()
        {
            Response.Redirect("/Error.html");
        }

        /// <summary>
        /// 获取当前页或操作访问权限
        /// </summary>
        /// <returns>权限列表</returns>
        public List<PermModel> GetPermission()
        {
            string filePath = HttpContext.Request.FilePath;

            List<PermModel> perm = (List<PermModel>)Session[filePath];
            return perm;
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new ToJsonResult
            {
                Data = data,
                ContentEncoding = contentEncoding,
                ContentType = contentType,
                JsonRequestBehavior = behavior,
                FormateStr = "yyyy-MM-dd HH:mm:ss"
            };
        }     

        protected JsonResult Success(string message="")
        {
            return Json(new OperationResult(OperationResultType.Success,message),JsonRequestBehavior.AllowGet);
        }

        protected JsonResult Error(string message)
        {
            return Json(new OperationResult(OperationResultType.Error, message), JsonRequestBehavior.AllowGet);
        }
    }



}
