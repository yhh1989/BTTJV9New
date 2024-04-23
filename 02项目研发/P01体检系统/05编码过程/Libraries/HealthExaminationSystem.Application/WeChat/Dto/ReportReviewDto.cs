using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
    /// <summary>
    /// 复查信息
    /// </summary>
   public  class ReportReviewDto
    {
        
        
        /// <summary>
        /// 疾病名称
        /// </summary>
        [StringLength(256)]
        public virtual string IllName { get; set; }

        /// <summary>
        /// 复查时间
        /// </summary>       
        public virtual DateTime? ReviewDate { get; set; }

        /// <summary>
        /// 复查建议
        /// </summary>      
        [StringLength(256)]
        public virtual string Remark { get; set; }


        /// <summary>
        /// 复查项目
        /// </summary>
        [StringLength(100)]
        public virtual string ItemGroups { get; set; }
    }
}
