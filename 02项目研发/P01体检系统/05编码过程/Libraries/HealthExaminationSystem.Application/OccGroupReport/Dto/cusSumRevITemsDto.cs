#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccGroupReport.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlCustomerSummarize))]
#endif
    public class cusSumRevITemsDto
    {
        /// <summary>
        /// 复查项目
        /// </summary>
        [StringLength(1024)]
        public virtual string ReviewContent { get; set; }
        /// <summary>
        /// 总检结论
        /// </summary>
        [StringLength(3072)]
        public virtual string CharacterSummary { get; set; }
        /// <summary>
        /// 总检建议
        /// </summary>
        [StringLength(3072)]
        public virtual string Advice { get; set; }
    }
}
