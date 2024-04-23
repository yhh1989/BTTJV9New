using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public class WCusDepSumDto
    {
        
        /// <summary>
        /// 科室编码
        /// </summary>
        public virtual string DepartmentBM { get; set; }   
        /// <summary>
        /// 科室小结
        /// </summary>
        [StringLength(3072)]
        public virtual string CharacterSummary { get; set; }

        /// <summary>
        /// 科室诊断小结
        /// </summary>
        [StringLength(3072)]
        public virtual string DagnosisSummary { get; set; }

        /// <summary>
        /// 小结日期
        /// </summary>
        public virtual DateTime? CheckDate { get; set; }
       
        /// 小结医生
        /// </summary>
        public virtual string EmpName { get; set; }
    }
}
