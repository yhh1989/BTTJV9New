using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
   public  class reportCusReSumDto
    {
        

    
        /// <summary>
        /// 复查项目
        /// </summary>      
        public virtual string ItemGroups { get; set; }
        /// <summary>
        /// 复查日期
        /// </summary>
        public DateTime? ReviewDate { get; set; }


        /// <summary>
        /// 总检医生
        /// </summary>      
        public virtual string sumEmp { get; set; }
        /// <summary>
        /// 总检时间
        /// </summary>      
        public virtual DateTime? sumTime { get; set; }
        /// <summary>
        /// 处理意见
        /// </summary>
        [StringLength(500)]
        public virtual string Opinions { get; set; }


        /// <summary>
        /// 职业健康结论描述
        /// </summary>
        [StringLength(1000)]
        public virtual string Description { get; set; }

    }
}
