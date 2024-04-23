using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;


namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
   public class ChargeCustmoerItemGroupDto
    {
        /// <summary>
        /// 科室名称
        /// </summary>
        public virtual string 科室名称 { get; set; }
        /// <summary>
        /// 组合名称
        /// </summary>
        public virtual string 组合名称 { get; set; }

        public virtual string 项目加减状态 { get; set; }
        public virtual string 收费状态 { get; set; }
        public virtual string 项目检查状态 { get; set; }
                /// <summary>
        /// 折后价格
        /// </summary>
        public virtual decimal 折后价格 { get; set; }


    }
}
