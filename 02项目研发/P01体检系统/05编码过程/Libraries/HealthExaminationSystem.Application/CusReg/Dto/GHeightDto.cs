using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
   public  class GHeightDto
    {
        /// <summary>
        /// 设备唯一标识
        /// </summary>
        public string MacId { get; set; }
        /// <summary>
        /// 身高
        /// </summary>
        public string Height { get; set; }
        /// <summary>
        /// 体重
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// 体重指数
        /// </summary>
        public string BMI { get; set; }
       
    }
}
