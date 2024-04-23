using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Models
{
    /// <summary>
    /// 复查通知书参数类
    /// </summary>
    public class CheckReviewReport
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string CodeID { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string ContentText { get; set; }

        /// <summary>
        /// 医院名称
        /// </summary>
        public string HospitalName { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string DatetimeVal { get; set; }
    }
}
