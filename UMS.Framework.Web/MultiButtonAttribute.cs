using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Web.Mvc;

namespace UMS.Framework.Web
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)] 
    public class MultiButtonAttribute : ActionNameSelectorAttribute
    {

        public string MatchFormValue { get; set; }

        public override bool IsValidName(ControllerContext controllerContext, string actionName, System.Reflection.MethodInfo methodInfo)
        {

            bool state = !string.IsNullOrEmpty(MatchFormValue) && controllerContext.HttpContext.Request.Form.AllKeys.Contains(this.MatchFormValue);

            return state;

        }

    }
}
