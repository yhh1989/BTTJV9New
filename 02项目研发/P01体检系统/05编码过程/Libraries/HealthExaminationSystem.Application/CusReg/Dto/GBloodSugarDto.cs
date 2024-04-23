using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
  public   class GBloodSugarDto
    {
        /// <summary>
        /// 设备唯一标识
        /// </summary>
        public string MacId { get; set; }
        /// <summary>
        /// 血糖值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 血糖类型：1、餐前血糖2、餐后血糖3、随机血糖
        /// </summary>
        public string BloodsugarType { get; set; }
    }
}
