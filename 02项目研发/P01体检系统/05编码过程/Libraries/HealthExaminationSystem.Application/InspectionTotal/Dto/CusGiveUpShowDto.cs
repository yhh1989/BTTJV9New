using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
 public   class CusGiveUpShowDto
    {
        /// <summary>
        /// 待查时间（下次检查的时间）
        /// </summary>
        public virtual DateTime? stayDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string remart { get; set; }
        /// <summary>
        /// 类型 1放弃 2待查
        /// </summary>
        public virtual int stayType { get; set; }
        /// <summary>
        /// 组合名称
        /// </summary>
        public virtual string GroupName { get; set; }
    }
}
