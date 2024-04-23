using Abp.Runtime.Caching;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sw.Hospital.HealthExaminationSystem.Web.Controllers
{
    public class UpdateController : Controller
    {
        public UpdateController(ICacheManager cacheManager)
        {
            var caches = cacheManager.GetAllCaches();
            foreach (var cache in caches)
            {
                cache.Clear();
            }
        }
        // GET: Update
        public ActionResult Index()
        {
            var migrator = new DbMigrator(new EntityFramework.Migrations.Configuration());
            migrator.Update();
            return View();
        }
    }
}