using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
   public  class GPEEcgDto
    {
        /// <summary>
        /// 设备唯一标识
        /// </summary>
        public string MacId { get; set; }
        /// <summary>
        /// RR间期
        /// </summary>
        public string RR { get; set; }
        /// <summary>
        /// PR 间期
        /// </summary>
        public string PR { get; set; }
        /// <summary>
        /// QRS 时限
        /// </summary>
        public string QRS { get; set; }
        /// <summary>
        /// QT 间期
        /// </summary>
        public string QT { get; set; }
        /// <summary>
        /// QTc 间期
        /// </summary>
        public string QTc { get; set; }
        /// <summary>
        /// P轴
        /// </summary>
        public string P_A { get; set; }
        /// <summary>
        /// T轴
        /// </summary>
        public string T_A { get; set; }
        /// <summary>
        /// QRS轴
        /// </summary>
        public string Q_A { get; set; }
        /// <summary>
        /// RV5 幅度
        /// </summary>
        public string RV5 { get; set; }
        /// <summary>
        /// SV1 幅度
        /// </summary>
        public string SV1 { get; set; }
        /// <summary>
        /// RV5_SV1
        /// </summary>
        public string RV5_SV1 { get; set; }
        /// <summary>
        /// Hr
        /// </summary>
        public string Hr { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public string Analysis { get; set; }
        /// <summary>
        /// 心电波形图图片
        /// </summary>
        public string EcgImg { get; set; }
    }
}
