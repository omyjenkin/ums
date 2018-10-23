using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using UMS.Models;
using UMS.Core;
using UMS.Utility;
using UMS.Web.Common;
using UMS.Framework.Web;

namespace UMS.Web.Controllers
{
    public class ModuleController : BaseController
    {

        public ISysModuleService ModuleService { get; set; }

        public ISysModuleOperateService ModuleOptService { get; set; }

        public ISysUserService UserService { get; set; }

        public ActionResult Index()
        {
            var perms = UserService.GetPermission("admin", "Module");
            ViewBag.Perm = perms;
            return View();
        }

        public JsonResult GetList()
        {
            var modules = ModuleService.List();
           var moduleDTO= modules.Select(m => new
            {
               
               Id=m.Id,
               Name=m.Name,
               EnglishName = m.EnglishName,
               ParentId = m.ParentId,
               Url = m.Url,
               Iconic = m.Iconic,
               Sort = m.Sort,
               Remark = m.Remark,
               Enable=m.Enable,
               CreateUser=m.CreateUser,
               CreateTime=m.CreateTime,
               IsLast=m.IsLast,
               state = "open" ,// m.IsLast ? "open" : "closed",
               _parentId=m.ParentId=="-1"?null: m.ParentId
           });
            var json = new
            {
                total = modules.Count(),
                rows = moduleDTO
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        [SupportFilter(ActionName = "Index")]
        [HttpPost]
        public JsonResult GetList(string id)
        {

            List<SysModule> list = ModuleService.GetMenuByUserId("admin", id);
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
                           state = (ModuleService.GetMenuByUserId("admin", r.Id).Count > 0) ? "closed" : "open"
                       };


            return Json(json);
        }

        [HttpPost]
        //[SupportFilter(ActionName = "Create")]
        public JsonResult GetOptListByModule(GridPager pager, string mid)
        {

            List<SysModuleOperate> list = ModuleOptService.GetListByModuleId(mid);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new SysModuleOperate()
                        {
                            Id = r.Id,
                            Name = r.Name,
                            KeyCode = r.KeyCode,
                            ModuleId = r.ModuleId,
                            IsValid = r.IsValid,
                            Sort = r.Sort

                        }).ToArray()

            };

            return Json(json);
        }


        #region 创建模块

        [SupportFilter]
        public ActionResult Create(string id)
        {
            ViewBag.Perm = GetPermission();
            SysModule entity = new SysModule()
            {
                ParentId = id,
                Enable = true,
                Sort = 0
            };
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Create(SysModule model)
        {
            model.CreateTime = DateTime.Now;
            model.CreateUser = "admin";
            if (model != null && ModelState.IsValid)
            {

                if (ModuleService.Insert(model) > 0)
                {
                    //LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name, "成功", "创建", "SysModule");
                    return Json(new OperationResult(OperationResultType.Success));
                }
                else
                {
                    //string ErrorCol = errors.Error;
                    //LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name + "," + ErrorCol, "失败", "创建", "SysModule");
                    return Json(new OperationResult(OperationResultType.Error, "插入失败"));
                }
            }
            else
            {
                return Json(new OperationResult(OperationResultType.Error, "插入失败"));
            }
        }
        #endregion


        #region 创建
        [SupportFilter(ActionName = "Create")]
        public ActionResult CreateOpt(string moduleId)
        {
            ViewBag.Perm = GetPermission();
            SysModuleOperate sysModuleOptModel = new SysModuleOperate();
            sysModuleOptModel.ModuleId = moduleId;
            sysModuleOptModel.IsValid = true;
            return View(sysModuleOptModel);
        }


        [HttpPost]
        [SupportFilter(ActionName = "Create")]
        public JsonResult CreateOpt(SysModuleOperate info)
        {
            if (info != null && ModelState.IsValid)
            {
                SysModuleOperate entity = ModuleOptService.GetByKey(info.Id??string.Empty);
                if (entity != null)
                    return Json(new OperationResult(OperationResultType.Error), JsonRequestBehavior.AllowGet);
                entity = new SysModuleOperate();
                entity.Id = info.ModuleId + info.KeyCode;
                entity.Name = info.Name;
                entity.KeyCode = info.KeyCode;
                entity.ModuleId = info.ModuleId;
                entity.IsValid = info.IsValid;
                entity.Sort = info.Sort;

                if (ModuleOptService.Insert(entity) > 0)
                {
                    //LogHandler.WriteServiceLog("admin", "Id:" + info.Id + ",Name:" + info.Name, "成功", "创建", "模块设置");
                    return Json(new OperationResult(OperationResultType.Success));
                }
                else
                {
                    //string ErrorCol = errors.Error;
                    //LogHandler.WriteServiceLog(GetUserId(), "Id:" + info.Id + ",Name:" + info.Name + "," + ErrorCol, "失败", "创建", "模块设置");
                    return Json(new OperationResult(OperationResultType.Error));
                }
            }
            else
            {
                return Json(new OperationResult(OperationResultType.Error));
            }
        }
        #endregion

        #region 修改模块

        [SupportFilter]
        public ActionResult Edit(string id)
        {
            ViewBag.Perm = GetPermission();
            SysModule entity = ModuleService.GetByKey(id);
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(string id,SysModule model)
        {
            if (model != null && ModelState.IsValid)
            {
                if (ModuleService.Update(model) > 0)
                {
                    //LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name, "成功", "修改", "系统菜单");
                    return Json(new OperationResult(OperationResultType.Success,"修改成功！"));
                }
                else
                {
                    //string ErrorCol = errors.Error;
                    //LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name + "," + ErrorCol, "失败", "修改", "系统菜单");
                    return Json(new OperationResult(OperationResultType.Error, "修改成功！"));
                }
            }
            else
            {
                return Json(new OperationResult(OperationResultType.Error, "修改成功！"));
            }
        }
        #endregion
         

        #region 删除

        [HttpPost]
        [SupportFilter]
        public JsonResult Delete(string id)
        {
            if (id != null)
            {
                if (ModuleService.Delete(id) > 0)
                {
                    //LogHandler.WriteServiceLog(GetUserId(), "Ids:" + id, "成功", "删除", "模块设置");
                    return Json(new OperationResult(OperationResultType.Success), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //string ErrorCol = errors.Error;
                    //LogHandler.WriteServiceLog(GetUserId(), "Id:" + id + "," + ErrorCol, "失败", "删除", "模块设置");
                    return Json(new OperationResult(OperationResultType.Error), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new OperationResult(OperationResultType.Error), JsonRequestBehavior.AllowGet);
            }


        }


        [HttpPost]
        [SupportFilter(ActionName = "Delete")]
        public JsonResult DeleteOpt(string id)
        {
            if (id != null)
            {
                if (ModuleOptService.Delete(id) > 0)
                {
                    //LogHandler.WriteServiceLog(GetUserId(), "Id:" + id, "成功", "删除", "模块设置KeyCode");
                    return Json(new OperationResult(OperationResultType.Error), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //string ErrorCol = errors.Error;
                    //LogHandler.WriteServiceLog(GetUserId(), "Id:" + id + "," + ErrorCol, "失败", "删除", "模块设置KeyCode");
                    return Json(new OperationResult(OperationResultType.Error), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new OperationResult(OperationResultType.Error) { Message = "0" }, JsonRequestBehavior.AllowGet);
            }


        }

        #endregion
    }
}
