using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmSummarizeAdvice))]
#endif
    public class FullSummarizeAdviceDto : SummarizeAdviceDto
    {
        /// <summary>
        /// 科室
        /// </summary>
        public virtual DepartmentSimpleDto Department { get; set; }
    }
}
