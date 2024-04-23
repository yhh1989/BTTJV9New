using System;
using System.Collections.Generic;

namespace Sw.Hospital.HealthExamination.Drivers.Models.LisInterface
{
    public class TdbInterfaceWhere
    {
        /// <summary>
        /// 档案号
        /// </summary>
        public string ActiveNum { get; set; }

        /// <summary>
        /// 项目标识
        /// </summary>
        public List<string> ItemIds { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public List<string> ItemNames { get; set; }
    }
}