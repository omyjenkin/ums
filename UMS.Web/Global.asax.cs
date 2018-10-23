
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Reflection;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using UMS.Component.Data;
using UMS.Web.Interceptor;
using UMS.Web.Controllers;
using UMS.Log;
using UMS.Models;

namespace UMS.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //启用压缩
            BundleTable.EnableOptimizations = true;
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Mapper.Initialize(config => config.AddProfile<AutoMapperConfig>());

            //注入 Ioc
            IContainer container=null;
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(EFRepositoryBase<,>)).As(typeof(IRepository<,>));
            Type baseType = typeof(IDependency);

            // 获取所有相关类库的程序集
            Assembly core = Assembly.Load("UMS.Core");
            Assembly coreData = Assembly.Load("UMS.Core.Data");
            Assembly component = Assembly.Load("UMS.Component.Data");
            Assembly[] assemblies = new[] { core, coreData,component };
            // Assembly[] assemblies = System.IO.Directory.GetFiles(AppDomain.CurrentDomain.RelativeSearchPath, "*.dll").Select(Assembly.LoadFrom).ToArray();

            builder.RegisterType(typeof(ExceptionInterceptor));

            builder.RegisterAssemblyTypes(assemblies)
                .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract)
                .AsSelf().AsImplementedInterfaces().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies).InstancePerLifetimeScope();//.EnableClassInterceptors().InterceptedBy(typeof(ExceptionInterceptor));//InstancePerLifetimeScope 保证对象生命周期基于请求

            Type baseCtrl = typeof(BaseController);
            builder.RegisterControllers(Assembly.GetExecutingAssembly())
                .Where(type=> baseCtrl.IsAssignableFrom(type)).PropertiesAutowired().EnableClassInterceptors().InterceptedBy(typeof(ExceptionInterceptor));

            builder.RegisterFilterProvider();

            container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
           
        }

        /// <summary>
        /// 全局的异常处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            string s = HttpContext.Current.Request.Url.ToString();
            HttpServerUtility server = HttpContext.Current.Server;
            if (server.GetLastError() != null)
            {
                Exception lastError = server.GetLastError();
                SysLog message = new SysLog
                {
                    Category = LoggerType.SystemLog.ToString(),
                    Message = lastError.Message,
                    Exception = lastError.StackTrace,
                    ErrorUrl = server.UrlEncode(s),
                    Method = lastError.TargetSite.Name,
                    CreateTime = DateTime.Now
                };

                Logger.Debug(LoggerType.SystemLog, message, lastError);

                Application["LastError"] = lastError;
                int statusCode = HttpContext.Current.Response.StatusCode;
                string exceptionOperator = "/Exception/Error";
                try
                {
                    if (!String.IsNullOrEmpty(exceptionOperator))
                    {
                        exceptionOperator = new System.Web.UI.Control().ResolveUrl(exceptionOperator);
                        string url = string.Format("{0}?ErrorUrl={1}", exceptionOperator, server.UrlEncode(s));
                        string script = String.Format("<script language='javascript' type='text/javascript'>window.top.location='{0}';</script>", url);
                        Response.Write(script);
                        Response.End();
                    }
                }
                catch { }
            }
        }
    }
}
