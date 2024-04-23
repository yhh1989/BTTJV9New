using System;
using Abp.Application.Services.Dto;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
#if Application
    [AutoMap(typeof(TjlCustomerItemGroup))]
#endif
    public class GuideUpdateCustomerItemGroupDto : EntityDto<Guid>
    {

        /// <summary>
        /// 是否已打导引单 只有项目组合选择有变动，须同步状态
        /// </summary>
        public virtual int? GuidanceSate { get; set; }
    }
}