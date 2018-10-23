using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using UMS.Models;
using UMS.Core;
using UMS.Utility;
using UMS.Framework.Web;
using UMS.Models.DTOs;

namespace UMS.Web.Controllers
{
    public class UserController : BaseController
    {
        public ISysUserService UserService { get; set; }

        [SupportFilter]
        public ActionResult Index()
        {
            var perms = GetPermission();
            ViewBag.Perm = perms;
            return View();
        }

        public JsonResult GetList(GridPager pager)
        {
            var user = UserService.GetPagedList(pager);
            var json = new
            {
                total = pager.totalRows,
                rows = user
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        #region 创建用户

        [SupportFilter]
        public ActionResult Create(string id)
        {
            ViewBag.Perm = GetPermission();

            return View();
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Create(SysUser model)
        {
            model.Password = "111111";
            model.CreateTime = DateTime.Now;
            model.CreateUser = "admin";
            if (model != null && ModelState.IsValid)
            {

                if (UserService.Insert(model) > 0)
                {
                    return Success();
                }
                else
                {
                    return Error("插入失败");
                }
            }
            else
            {
                return Error("插入失败");
            }
        }
        #endregion

        #region 修改用户

        [SupportFilter]
        public ActionResult Edit(string id)
        {
            ViewBag.Perm = GetPermission();
            SysUser entity = UserService.GetByKey(id);
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(string id, SysUser model)
        {
            if (model != null && ModelState.IsValid)
            {
                if (UserService.Update(model) > 0)
                {
                    return Json(new OperationResult(OperationResultType.Success, "修改成功！"));
                }
                else
                {
                    return Json(new OperationResult(OperationResultType.Error, "修改成功！"));
                }
            }
            else
            {
                return Json(new OperationResult(OperationResultType.Error, "修改成功！"));
            }
        }

        #endregion

        #region 设置角色用户

        [SupportFilter(ActionName = "Allot")]
        public ActionResult GetUserByRole(string roleId)
        {
            ViewBag.RoleId = roleId;
            ViewBag.Perm = GetPermission();
            return View();
        }

        [SupportFilter(ActionName = "Allot")]
        public ActionResult GetRoleByUser(string userId)
        {
            ViewBag.UserId = userId;
            ViewBag.Perm = GetPermission();
            return View();
        }

        [SupportFilter(ActionName = "Allot")]
        public JsonResult GetRoleListByUser(GridPager pager,string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return Json(new OperationResult(OperationResultType.Error));

            pager.sort = "Name";
            var userList = UserService.GetRoleByUserId(ref pager, userId).ToList();
            var jsonData = new
            {
                total = pager.totalRows,
                rows = userList
            };
            return Json(jsonData,JsonRequestBehavior.AllowGet);
        }

        [SupportFilter(ActionName = "Save")]
        public JsonResult UpdateUserRoleByUserId(string userId, string roleIds)
        {
            string[] arr = roleIds.Split(',');


            if (UserService.UpdateUserRole(userId, arr))
            {
                return Json(new OperationResult(OperationResultType.Success, Suggestion.SetSucceed), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new OperationResult(OperationResultType.Error, Suggestion.SetFail), JsonRequestBehavior.AllowGet);
            }

        }

        #endregion
    }
}