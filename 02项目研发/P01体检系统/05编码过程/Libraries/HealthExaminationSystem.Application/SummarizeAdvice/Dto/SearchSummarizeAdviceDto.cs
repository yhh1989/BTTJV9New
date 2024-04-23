using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto
{
   public  class SearchSummarizeAdviceDto
    {
        /// <summary>
        /// 科室名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 疾病名称
        /// </summary>
        public virtual string AdviceName { get; set; }

        /// <summary>
        /// 疾病类别
        /// </summary>
        public virtual string DiagnosisAType { get; set; }
    }
}
