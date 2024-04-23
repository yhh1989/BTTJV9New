using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public class WCusSumDto
    {
        

        /// <summary>
        /// 审核人标识
        /// </summary>
       
        public virtual string  ShEmployeeName { get; set; }

        /// <summary>
        /// 总检人
        /// </summary>

        public virtual string EmployeeName { get; set; }       

        /// <summary>
        /// 总检结论
        /// </summary>
        [StringLength(3072)]
        public virtual string CharacterSummary { get; set; }

        /// <summary>
        /// 诊断结论
        /// </summary>
        [StringLength(3072)]
        public virtual string DagnosisSummary { get; set; }

        /// <summary>
        /// 总检建议
        /// </summary>
        [StringLength(8192)]
        public virtual string Advice { get; set; }

        /// <summary>
        /// 总检日期
        /// </summary>
        public virtual DateTime? ConclusionDate { get; set; }

        /// <summary>
        /// 总检审核日期
        /// </summary>
        public virtual DateTime? ExamineDate { get; set; }

    }
}
