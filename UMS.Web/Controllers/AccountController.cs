using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using UMS.Models;

using UMS.Core;
using UMS.Utility;

namespace UMS.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        
        public ISysUserService AccountService { get; set; }



        public AccountController()
        {
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            //ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string userName,string password, string code, string returnUrl)
        {
            //if (Session["Code"] == null)
            //    return Json(new OperationResult(OperationResultType.Error, "请重新刷新验证码"), JsonRequestBehavior.AllowGet);

            //if (Session["Code"].ToString().ToLower() != code.ToLower())
            //    return Json(new OperationResult(OperationResultType.Error, "验证码错误"), JsonRequestBehavior.AllowGet);
             
            var user = AccountService.Login(userName, Encrypt.MD5(password));
            if (user == null)
            {
                return Json(new OperationResult(OperationResultType.Error, "用户名或密码错误"), JsonRequestBehavior.AllowGet);
            }
            else if (!Convert.ToBoolean(user.State))//被禁用
            {
                return Json(new OperationResult(OperationResultType.Error, "账户被系统禁用"), JsonRequestBehavior.AllowGet);
            }

            SysLoginInfo account = new SysLoginInfo();
            account.LoginName = userName;

            Session["LoginInfo"] = account;

            return Json(new OperationResult(OperationResultType.Success, ""), JsonRequestBehavior.AllowGet);

        }

      
        ////
        //// GET: /Account/Manage
        //public ActionResult Manage(ManageMessageId? message)
        //{
        //    ViewBag.StatusMessage =
        //        message == ManageMessageId.ChangePasswordSuccess ? "你的密码已更改。"
        //        : message == ManageMessageId.SetPasswordSuccess ? "已设置你的密码。"
        //        : message == ManageMessageId.RemoveLoginSuccess ? "已删除外部登录名。"
        //        : message == ManageMessageId.Error ? "出现错误。"
        //        : "";
        //    ViewBag.HasLocalPassword = HasPassword();
        //    ViewBag.ReturnUrl = Url.Action("Manage");
        //    return View();
        //}

        ////
        //// POST: /Account/Manage
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Manage(ManageUserViewModel model)
        //{
        //    bool hasPassword = HasPassword();
        //    ViewBag.HasLocalPassword = hasPassword;
        //    ViewBag.ReturnUrl = Url.Action("Manage");
        //    if (hasPassword)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
        //            if (result.Succeeded)
        //            {
        //                return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
        //            }
        //            else
        //            {
        //                AddErrors(result);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        // User does not have a password so remove any validation errors caused by a missing OldPassword field
        //        ModelState state = ModelState["OldPassword"];
        //        if (state != null)
        //        {
        //            state.Errors.Clear();
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
        //            if (result.Succeeded)
        //            {
        //                return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
        //            }
        //            else
        //            {
        //                AddErrors(result);
        //            }
        //        }
        //    }

        //    // 如果我们进行到这一步时某个地方出错，则重新显示表单
        //    return View(model);
        //}


        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }


        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }



        #region 帮助程序
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        //private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        //{
        //    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
        //    var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
        //    AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        //}

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }



        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}