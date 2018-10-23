using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UMS.Models;

namespace UMS.Web.Controllers
{
    public class ExceptionController : Controller
    {
        // GET: Exception
        public ActionResult Error(string ErrorUrl)
        {

            Exception lastError =(Exception) HttpContext.Application["LastError"] ;
            SysLog message = new SysLog
            { 
                Message = lastError.Message,
                Exception = lastError.StackTrace,
                 ErrorUrl= ErrorUrl,
                  Method=lastError.TargetSite.Name, 
                CreateTime = DateTime.Now
            };

            return View(message);
        }
    }
}