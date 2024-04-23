using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison.Dto
{
    /// <summary>
    /// 专科建议体检人总检建议
    /// </summary>
    [Obsolete("暂停使用", true)]
#if !Proxy
    [AutoMap(typeof(TjlCustomerSummary))]
#endif
    public class SearchCustomerSummaryDto : EntityDto<Guid>
    {
        /// <summary>
        /// 预约id
        /// </summary>
        public virtual Guid? CustomerRegBMId { get; set; }
        /// <summary>
        /// 科室ID
        /// </summary>
        public virtual Guid? DepartmentBMId { get; set; }
        /// <summary>
        /// 专科建议
        /// </summary>
        [StringLength(3072)]
        public virtual string CharacterSummary { get; set; }
    }
}
