using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UMS.Core;
using UMS.Framework.Web;
using UMS.Models;
using UMS.Models.DTOs;

namespace UMS.Web.Controllers
{
    public class RightController : BaseController
    {
        // GET: Right
        
        public ISysRightService SysRightService { get; set; }
        
        public ISysRoleService SysRoleService { get; set; }
        
        public ISysModuleService SysModuleService { get; set; }

        [SupportFilter]
        public ActionResult Index()
        {
            ViewBag.Perm = GetPermission();
            return View();
        }

        //获取角色列表
        [SupportFilter(ActionName = "Index")]
        [HttpPost]
        public JsonResult GetRoleList(GridPager pager)
        {
            pager.sort = "Id";

            IEnumerable<SysRole> list = SysRoleService.List(ref pager,null).AsEnumerable();
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new SysRole()
                        {

                            Id = r.Id,
                            Name = r.Name,
                            Description = r.Description,
                            CreateTime = r.CreateTime,
                            CreateUser = r.CreateUser

                        }).ToArray()

            };

            return Json(json);
        }

        //获取模组列表
        [SupportFilter(ActionName = "Index")]
        [HttpPost]
        public JsonResult GetModelList(string id)
        {
            if (id == null)
                id = "0";
            List<SysModule> list = SysModuleService.GetModuleBySystem(id);
            var json = from r in list
                       select new SysModule()
                       {
                           Id = r.Id,
                           Name = r.Name,
                           EnglishName = r.EnglishName,
                           ParentId = r.ParentId,
                           Url = r.Url,
                           Iconic = r.Iconic,
                           Sort = r.Sort,
                           Remark = r.Remark,
                           Enable = r.Enable,
                           CreateUser = r.CreateUser,
                           CreateTime = r.CreateTime,
                           IsLast = r.IsLast,
                           state = (SysModuleService.GetModuleBySystem(r.Id).Count > 0) ? "closed" : "open"
                       };

            return Json(json);
        }

        //根据角色与模块得出权限
        [SupportFilter(ActionName = "Index")]
        [HttpPost]
        public JsonResult GetRightByRoleAndModule(GridPager pager, string roleId, string moduleId)
        {
            pager.rows = 100000;
            var list = SysRightService.GetRightByRoleAndModule(roleId, moduleId);
            var json = new
            {
                total = list.Count,
                rows = list

            };

            return Json(json);
        }

        //保存
        [HttpPost]
        [SupportFilter(ActionName = "Save")]
        public int UpdateRight(SysRightOperate model)
        {
            return SysRightService.UpdateRight(model);
        }
    }
}