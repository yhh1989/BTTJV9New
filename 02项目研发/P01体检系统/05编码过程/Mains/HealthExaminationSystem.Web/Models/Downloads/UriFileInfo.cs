using System;
using System.ComponentModel.DataAnnotations;

namespace Sw.Hospital.HealthExaminationSystem.Web.Models.Downloads
{
    public class UriFileInfo
    {
        [Display(Name = "文件名")]
        public string Name { get; set; }

        [Display(Name = "下载地址")]
        public string Url { get; set; }

        [Display(Name = "创建日期")]
        public DateTime Date { get; set; }
    }
}