using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 体检人项目组合
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomerItemGroup))]
#endif
    public class UpdateCustomerItemGroupDto : EntityDto<Guid>
    {
     

        /// <summary>
        /// 体检人项目
        /// </summary>
        public virtual List<UpdateCustomerRegItemDto> CustomerRegItem { get; set; }
        /// <summary>
        /// 项目检查状态 1未检查2已检查3部分检查4放弃5待查 6暂存
        /// </summary>
        public virtual int? CheckState { get; set; }
        
        /// <summary>
        /// 开单医生ID
        /// </summary>
        public virtual long? BillingEmployeeBMId { get; set; }

        /// <summary>
        /// 检查人
        /// </summary>
        public virtual long? InspectEmployeeBMId { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public virtual long? CheckEmployeeBMId { get; set; }

        /// <summary>
        /// 组合小结
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemGroupSum { get; set; }

        /// <summary>
        /// 第一次检查时间
        /// </summary>
        public virtual DateTime? FirstDateTime { get; set; }

    }
}
