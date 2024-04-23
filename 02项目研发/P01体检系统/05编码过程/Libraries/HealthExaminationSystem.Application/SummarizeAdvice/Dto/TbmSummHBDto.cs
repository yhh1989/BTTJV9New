using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmSummHB))]
#endif
    public class TbmSummHBDto : EntityDto<Guid>
    {
        /// <summary>
        /// 诊断ID
        /// </summary>
        public virtual Guid SummarizeAdviceId { get; set; }
        /// <summary>
        /// 复合诊断名称
        /// </summary>
        public virtual string AdviceName { get; set; }

        /// <summary>
        /// 诊断信息
        /// </summary>    
        public virtual ICollection<SummarizeAdviceDto> Advices { get; set; }
    }
}
