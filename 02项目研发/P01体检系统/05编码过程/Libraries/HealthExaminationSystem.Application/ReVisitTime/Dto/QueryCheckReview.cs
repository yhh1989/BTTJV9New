using System;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
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
    public class QueryCheckReview : EntityDto<Guid>
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
        public virtual SimpleItemGroupDto ItemGroup { get; set; }
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

        /// <summary>
        /// 体检人预约记录标识
        /// </summary>
        public Guid CustomerRegisterId { get; set; }

        /// <summary>
        /// 体检人预约记录
        /// </summary>
        public virtual TjlCustomerRegForInspectionTotalDto CustomerRegister { get; set; }

        /// <summary>
        /// 单位预约标识
        /// </summary>
        public Guid? CompanyRegisterId { get; set; }

    }
}
