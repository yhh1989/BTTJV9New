using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto
{
   public class TjlCusomerDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }

        /// <summary>
        /// 单位名称TjlClientInfo
        /// </summary>
        public virtual string ClientName { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }

        /// <summary>
        /// 危害因素 逗号隔开
        /// </summary>
        public virtual string RiskS { get; set; }

        /// <summary>
        /// 工种
        /// </summary>

        public virtual string TypeWork { get; set; }

       
    }
}
