
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UMS.Core;
using UMS.Models;

namespace UMS.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ISysUserService UserService { get; set; }

        
        public ISysModuleService ModuleService { get; set; }

        
        //[Authorize]
        public ActionResult Index()
        {
            //var f = service.GetList().First() ;
            //ViewBag.Message = f.UserName + f.Id;

            var user = UserService.GetByKey("admin");
            SysLoginInfo account = new SysLoginInfo();
            account.SysUserId = user.Id;
            account.LoginName = user.TrueName;
            Session["Account"] = account;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <param name="id">所属</param>
        /// <returns>树</returns>
        public JsonResult GetTree(string id)
        {
            
            List<SysModule> menus = ModuleService.GetMenuByUserId("admin", id);
            var jsonData = (
                    from m in menus
                    select new
                    {
                        id = m.Id,
                        text = m.Name,
                        value = m.Url,
                        showcheck = false,
                        complete = false,
                        isexpand = false,
                        checkstate = 0,
                        hasChildren = m.IsLast ? false : true,
                        iconCls = m.Iconic
                    }
                ).ToArray();
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
    }
}