using System;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
#if Application
using Abp.AutoMapper;
#endif
namespace Sw.Hospital.HealthExaminationSystem.Application.ReVisitTime.Dto
{
    /// <summary>
    /// 查询复诊时间
    /// </summary>
#if Application
    [AutoMap(typeof(Core.Illness.ReVisitTime))]
#endif
    public class SearchReVisitTimeDto : EntityDto<Guid>
    {
        /// <summary>
        /// 内容
        /// </summary>
        public virtual string Content { get; set; }
        /// <summary>
        /// 项目组合ID
        /// </summary>
        public virtual Guid? ItemGroupId { get; set; }

        /// <summary>
        /// 项目组合ID
        /// </summary>
        public virtual TjlCustomerItemGroupDto ItemGroup { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 是否已经复查
        /// </summary>
        public bool IsInspect { get; set; }
        
        public Guid CustomerRegisterId { get; set; }

        /// <summary>
        /// 单位预约标识
        /// </summary>
        public Guid? CompanyRegisterId { get; set; }
        /// <summary>
        /// 人员id
        /// </summary>
        public Guid? CustomerRegId { get; set; }
    }
}
