using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.BarPrintInfoItemGroup.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlCustomerBarPrintInfoItemGroup))]
#endif
    public class BarPrintInfoItemGroupQueryDto : EntityDto<Guid>
    {
     
        /// <summary>
        /// 组合
        /// </summary>
        public virtual ItemGroup.Dto.SimpleItemGroupDto ItemGroup { get; set; }

        /// <summary>
        /// 组合名称
        /// </summary>
        [StringLength(32)]
        public virtual string ItemGroupName { get; set; }

        /// <summary>
        /// 组合别名
        /// </summary>
        [StringLength(32)]
        public virtual string ItemGroupNameB { get; set; }


    }
}
