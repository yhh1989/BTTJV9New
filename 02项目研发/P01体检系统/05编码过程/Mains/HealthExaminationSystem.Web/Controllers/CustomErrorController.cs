using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.Runtime.Caching;
using Sw.Hospital.HealthExaminationSystem.EntityFramework.EntityFramework;

namespace Sw.Hospital.HealthExaminationSystem.Web.Controllers
{
    public class CustomErrorController : Controller
    {
        public CustomErrorController(ICacheManager cacheManager)
        {
            var caches = cacheManager.GetAllCaches();
            foreach (var cache in caches)
            {
                cache.Clear();
            }
        }

        // GET: CustomError
        public ActionResult Index()
        {
            var migrator = new DbMigrator(new EntityFramework.Migrations.Configuration());
            migrator.Update();
            return View();
        }
    }
}