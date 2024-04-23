using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Models
{
    /// <summary>
    /// 禁忌证告知书报表参数类
    /// </summary>
    public class ContraindicationNotificationReport
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
        /// 医院名称
        /// </summary>
        public string HospitalName { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }
        /// <summary>
        /// 患者身份证号
        /// </summary>
        public string IDNumber { get; set; }
        /// <summary>
        /// 工种
        /// </summary>
        public string WorkType { get; set; }
        /// <summary>
        /// 岗位状态
        /// </summary>
        public string PostStatus { get; set; }
    }
}
