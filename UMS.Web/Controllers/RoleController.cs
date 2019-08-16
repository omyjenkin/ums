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
using AutoMapper;

namespace UMS.Web.Controllers
{
    public class RoleController : BaseController
    {

        public ISysRoleService RoleService { get; set; }

        public ISysUserService UserService { get; set; }

        // GET: Role
        public ActionResult Index()
        {
            var perms = UserService.GetPermission("admin", "Role");
            ViewBag.Perm = perms;
            return View();
        }

        public JsonResult GetList()
        {

            var roles = RoleService.List();
            var json = new
            {
                total = roles.Count(),
                rows = roles
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        #region 创建角色

        [SupportFilter]
        public ActionResult Create(string id)
        {
            ViewBag.Perm = GetPermission();

            return View();
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Create(SysRole model)
        {
            model.CreateTime = DateTime.Now;
            model.CreateUser = "admin";
            if (model != null && ModelState.IsValid)
            {

                if (RoleService.Insert(model) > 0)
                {
                    //LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name, "成功", "创建", "SysRole");
                    return Success(Suggestion.InsertSucceed);
                }
                else
                {
                    //string ErrorCol = errors.Error;
                    //LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name + "," + ErrorCol, "失败", "创建", "SysRole");
                    return Error(Suggestion.InsertFail);
                }
            }
            else
            {
                return Error(Suggestion.InsertFail);
            }
        }
        #endregion

        #region 修改角色

        [SupportFilter]
        public ActionResult Edit(string id)
        {
            ViewBag.Perm = GetPermission();
            SysRole entity = RoleService.GetByKey(id);
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(string id, SysRole model)
        {
            if (model != null && ModelState.IsValid)
            {
                if (RoleService.Update(model) > 0)
                {
                    //LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name, "成功", "修改", "系统菜单");
                    return Success(Suggestion.UpdateSucceed);
                }
                else
                {
                    //string ErrorCol = errors.Error;
                    //LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name + "," + ErrorCol, "失败", "修改", "系统菜单");
                    return Error(Suggestion.UpdateFail);
                }
            }
            else
            {
                return Error(Suggestion.UpdateFail);
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
        public JsonResult GetUserListByRole(GridPager pager, string roleId)
        {
            if (string.IsNullOrWhiteSpace(roleId))
                return Json(new OperationResult(OperationResultType.Error));

            IEnumerable<SysUser> userList = RoleService.GetUserByRoleId(ref pager, roleId).AsEnumerable();

            List<UserDTO> users = new List<UserDTO>();
            foreach (var user in userList)
            {
                var dto = Mapper.Map<UserDTO>(user);
                dto.Flag = user.Id == null ? "0" : "1";
                users.Add(dto);
            }

            var jsonData = new
            {
                total = pager.totalRows,
                rows = users
            };
            return Json(jsonData);
        }
       
        [SupportFilter(ActionName = "Save")]
        public JsonResult UpdateUserRoleByRoleId(string roleId, string userIds)
        {
            string[] arr = userIds.Split(',');

            if (RoleService.UpdateSysRoleSysUser(roleId, arr))
            {
                return Success(Suggestion.SetSucceed);
            }
            else
            {
                return Error(Suggestion.SetFail);
            }
             
        }

        #endregion

    }
}