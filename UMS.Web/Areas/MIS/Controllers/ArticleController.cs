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
using UMS.Web.Controllers;

namespace UMS.Web.Areas.MIS.Controllers
{
    public class ArticleController : BaseController
    {
        // GET: MIS/Article
        public ActionResult Index()
        {
            var perms = GetPermission();
            ViewBag.Perm = perms;

            return View();
        }
    }
}