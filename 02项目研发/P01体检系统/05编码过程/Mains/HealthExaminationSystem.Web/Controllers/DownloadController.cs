using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sw.Hospital.HealthExaminationSystem.Web.Models.Downloads;

namespace Sw.Hospital.HealthExaminationSystem.Web.Controllers
{
    public class DownloadController : Controller
    {
        // GET: Downloads
        public ActionResult Index()
        {
            var hostPath = Server.MapPath("~/");
            var downloadsPath = Path.Combine(hostPath, "Downloads");
            if (!Directory.Exists(downloadsPath))
            {
                Directory.CreateDirectory(downloadsPath);
            }
            var directoryInfo = new DirectoryInfo(downloadsPath);
            var fileInfos = directoryInfo.GetFiles();
            var uriFileInfos = new List<UriFileInfo>();
            var url = new UriBuilder(Request.Url ?? throw new InvalidOperationException());
            //var sortFileInfos = fileInfos.OrderByDescending(r => r.CreationTime).ToList();
            var sortFileInfos = fileInfos.OrderBy(r => r.Name).ToList();
            //var count = fileInfos.Length > 10 ? 10 : fileInfos.Length;
            //for (int i = 0; i < count; i++)
            //{
            //    var fileInfo = sortFileInfos[i];
            //    url.Path = Path.Combine(downloadsPath, fileInfo.Name).Remove(0, hostPath.Length);
            //    uriFileInfos.Add(new UriFileInfo
            //    {
            //        Name = fileInfo.Name,
            //        Url = url.ToString(),
            //        Date = fileInfo.CreationTime
            //    });
            //}
            foreach (var fileInfo in sortFileInfos)
            {
                url.Path = Path.Combine(downloadsPath, fileInfo.Name).Remove(0, hostPath.Length);
                uriFileInfos.Add(new UriFileInfo
                {
                    Name = fileInfo.Name,
                    Url = url.ToString(),
                    Date = fileInfo.CreationTime
                });
            }

            return View(uriFileInfos);
        }

        /// <summary>
        /// 添加新版本
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 添加新版本
        /// </summary>
        /// <param name="zip">文件</param>
        /// <param name="version">版本号</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(HttpPostedFileWrapper zip, Version version)
        {
            return View();
        }
    }
}