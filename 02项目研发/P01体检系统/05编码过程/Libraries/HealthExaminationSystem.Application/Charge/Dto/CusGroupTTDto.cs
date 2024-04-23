using Abp.Application.Services.Dto;
#if Application
using Sw.Hospital.HealthExaminationSystem.Core.Examination; 
#endif
using System;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
#if Application //TjlCustomerItemGroup
    [Abp.AutoMapper.AutoMap(typeof(TjlCustomerItemGroup))]
#endif
    public class CusGroupTTDto : EntityDto<Guid>
    {   
        /// <summary>
        /// 项目检查状态 1未检查2已检查3部分检查4放弃5待查 6暂存
        /// </summary>
        public virtual int? CheckState { get; set; }

        /// <summary>
        /// 团体支付金额
        /// </summary>
        public virtual decimal TTmoney { get; set; }

       
    }
}
