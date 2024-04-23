using Abp.Application.Services.Dto;


using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto
{
    /// <summary>
    /// 回访dto
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TbmReviewItemSet))]
#endif
    public class ReviewSetDto : EntityDto<Guid>
    {
        public virtual Guid? SummarizeAdviceId { get; set; }
        /// <summary>
        /// 疾病名称
        /// </summary>
        [StringLength(256)]
        public virtual string IllName { get; set; }

        /// <summary>
        /// 复查周期/天
        /// </summary>       
        public virtual int? Checkday { get; set; }

        /// <summary>
        /// 回访周期/天
        /// </summary>      
        public virtual int? KFday { get; set; }
        /// <summary>
        /// 备注
        /// </summary>      
        [StringLength(256)]
        public virtual string Remark { get; set; }
        /// <summary>
        /// 复查组合ID
        /// </summary>      
        public virtual List<SimpleItemGroupDto> ItemGroupBM { get; set; }

    }
}
