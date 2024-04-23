using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterSummarize.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
   public  class SumAdviseDto
    {
        /// <summary>
        /// 性别 
        /// </summary>
        public virtual int? Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }
        /// <summary>
        /// 
        /// <summary>
		/// 总检结论
		/// </summary>
		[StringLength(3072)]
        public virtual string CharacterSummary { get; set; }

        public virtual List< CustomerRegisterSummarizeSuggestDto> Advice { get; set; }
    }
}
