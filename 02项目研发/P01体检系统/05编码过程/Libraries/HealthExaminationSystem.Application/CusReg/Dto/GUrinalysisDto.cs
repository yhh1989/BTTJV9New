using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
  public   class GUrinalysisDto
    {
        /// <summary>
        /// 设备唯一标识
        /// </summary>
        public string MacId { get; set; }
        /// <summary>
        /// 尿胆原
        /// </summary>
        public string URO { get; set; }
        /// <summary>
        /// 潜血
        /// </summary>
        public string BLD { get; set; }
        /// <summary>
        /// 胆红素
        /// </summary>      
        public string BIL { get; set; }
        /// <summary>
        /// 酮体
        /// </summary>
        public string KET { get; set; }
        /// <summary>
        /// 白细胞
        /// </summary>
        public string LEU { get; set; }
        /// <summary>
        /// 葡萄糖
        /// </summary>
        public string GLU { get; set; }
        /// <summary>
        /// 蛋白质
        /// </summary>
        public string PRO { get; set; }
        /// <summary>
        /// 酸碱度
        /// </summary>
        public string PH { get; set; }
        /// <summary>
        /// 亚硝酸盐
        /// </summary>
        public string NIT { get; set; }
        /// <summary>
        /// 比重
        /// </summary>
        public string SG { get; set; }
        /// <summary>
        /// 微白蛋白
        /// </summary>
        public string MAL { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public string Result { get; set; }
      
    }
}
