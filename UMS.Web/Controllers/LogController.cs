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
    public class LogController : BaseController
    {
        public ISysLogService LogService { get; set; }

        // GET: Log
        public ActionResult Index()
        {         
            return View();
        }

        public JsonResult GetList(GridPager pager)
        {
            var logs = LogService.List(ref pager,null);
            var json = new
            {
                total = pager.totalRows,
                rows = logs
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}