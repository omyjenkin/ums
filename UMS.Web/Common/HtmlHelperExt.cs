﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using UMS.Models;

namespace System.Web.Mvc
{
    public static class HtmlHelperExt
    {
        public static HtmlString ToolButton(this HtmlHelper helper, string id, string icon, string text, List<PermModel> permissions, string action)
        {
            //if (!permissions.IsValid) return new HtmlString(string.Empty);
            return new HtmlString($"<a id='{id}' style='float:left' class='l-btn l-btn-plain'  ><span class='l-btn-left'><span class='l-btn-text {icon}' style='padding-left: 20px; '>{text}</span></span></a>");
        }

        /// <summary>
        /// 权限按钮
        /// </summary>
        /// <param name="helper">htmlhelper</param>
        /// <param name="id">控件Id</param>
        /// <param name="icon">控件icon图标class</param>
        /// <param name="text">控件的名称</param>
        /// <param name="perm">权限列表</param>
        /// <param name="keycode">操作码</param>
        /// <param name="hr">分割线</param>
        /// <returns>html</returns>
        public static MvcHtmlString ToolButton(this HtmlHelper helper, string id, string icon, string text, List<PermModel> perm, string keycode, bool hr)
        {
            if (perm.Where(a => a.KeyCode == keycode).Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<a id=\"{0}\" style=\"float: left;\" class=\"l-btn l-btn-plain\">", id);
                sb.AppendFormat("<span class=\"l-btn-left\"><span class=\"l-btn-text {0}\" style=\"padding-left: 20px;\">", icon);
                sb.AppendFormat("{0}</span></span></a>", text);
                if (hr)
                {
                    sb.Append("<div class=\"datagrid-btn-separator\" style=\"height:21px\"></div>");
                }
                return new MvcHtmlString(sb.ToString());
            }
            else
            {
                return new MvcHtmlString("");
            }
        }
        /// <summary>
        /// 普通按钮
        /// </summary>
        /// <param name="helper">htmlhelper</param>
        /// <param name="id">控件Id</param>
        /// <param name="icon">控件icon图标class</param>
        /// <param name="text">控件的名称</param>
        /// <param name="hr">分割线</param>
        /// <returns>html</returns>
        public static MvcHtmlString ToolButton(this HtmlHelper helper, string id, string icon, string text, bool hr)
        {
            
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<a id=\"{0}\" style=\"float: left;\" class=\"l-btn l-btn-plain\">", id);
            sb.AppendFormat("<span class=\"l-btn-left\"><span class=\"l-btn-text {0}\" style=\"padding-left: 20px;\">", icon);
            sb.AppendFormat("{0}</span></span></a>", text);
            if (hr)
            {
                sb.Append("<div class=\"datagrid-btn-separator\"></div>");
            }
            return new MvcHtmlString(sb.ToString());

        }


        public static MvcHtmlString EditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, Object htmlAttributes, bool extendAttributes)
        {
            string value = html.EditorFor(expression).ToString();
            
            PropertyInfo[] properties = htmlAttributes.GetType().GetProperties();
            foreach (PropertyInfo info in properties)
            {
                int index = value.ToLower().IndexOf(info.Name.ToLower() + "=");
                if (index < 0)
                    value = value.Insert(value.Length - (value.EndsWith("/>") ? 2 : 1), info.Name.ToLower() + "=\"" + info.GetValue(htmlAttributes, null) + "\"");
                else if (extendAttributes)
                    value = value.Insert(index + info.Name.Length + 2, info.GetValue(htmlAttributes, null) + " ");
            }

            return MvcHtmlString.Create(value);
        }

    }
}