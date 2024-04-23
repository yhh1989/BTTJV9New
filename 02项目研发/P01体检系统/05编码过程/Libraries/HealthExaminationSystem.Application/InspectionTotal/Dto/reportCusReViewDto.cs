using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
   public  class reportCusReViewDto
    {
        

        /// <summary>
        /// 复查阳性
        /// </summary>      
        public virtual string SumName{ get; set; }
        /// <summary>
        /// 复查项目
        /// </summary>      
        public virtual string ItemGroups { get; set; }
        /// <summary>
        /// 复查日期
        /// </summary>
        public DateTime ReviewDate { get; set; }

        /// <summary>
        /// 复查备注
        /// </summary>
        public string Remart { get; set; }
    }
}
