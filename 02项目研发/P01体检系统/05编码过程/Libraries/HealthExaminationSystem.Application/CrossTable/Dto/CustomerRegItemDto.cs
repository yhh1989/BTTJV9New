using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Abp.Application.Services.Dto;
using System;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlCustomerRegItem))]
#endif
    public class CustomerRegItemDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检人检查项目组合id
        /// </summary>
        public virtual CustomerItemGroupDto CustomerItemGroupBM { get; set; }
        /// <summary>
        /// 项目状态 1放弃2已检3待查
        /// </summary>
        public virtual int? ProcessState { get; set; }
    }
}