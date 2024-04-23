using System;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Web;
using Abp;
using Abp.Castle.Logging.Log4Net;
using Abp.Web;
using Castle.Facilities.Logging;

namespace Sw.Hospital.HealthExaminationSystem.Web
{
    public class MvcApplication : AbpWebApplication<MyProjectWebModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
                f => f.UseAbpLog4Net().WithConfig(Server.MapPath("log4net.config"))
            );

            base.Application_Start(sender, e);
        }

        protected override void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Cache.SetNoStore();
        }
        protected override void Application_Error(object sender, EventArgs e)
        {
            var error = Server.GetLastError();
            if (error is EntityCommandExecutionException entityCommandExecutionException)
            {
                if (entityCommandExecutionException.InnerException is SqlException sqlException)
                {
                    if (sqlException.Number == 208)
                    {
                        // 数据库尚未执行迁移操作。
                        // 尚不确定
                        Server.ClearError();
                        Response.Redirect("~/CustomError/Index");
                    }
                }
            }
            else if (error is AbpException abpException)
            {
                if (abpException.Message.Equals("No language defined!"))
                {
                    // 数据库迁移完毕但没有运行种子数据
                    Server.ClearError();
                    Response.Redirect("~/CustomError/Index");
                }
            }

            base.Application_Error(sender, e);
        }
    }
}