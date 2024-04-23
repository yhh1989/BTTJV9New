using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
  
#if !Proxy
    [AutoMap(typeof(TjlCustomerReg))]
#endif
    public class ChargeInfoDto : EntityDto<Guid>
    {
        /// <summary>
        /// 人员类别
        /// </summary>
        public virtual PersonnelCategoryDto PersonnelCategory { get; set; }

        /// <summary>
        /// 人员类别标识
        /// </summary>     
        public Guid? PersonnelCategoryId { get; set; }
   
        /// <summary>
        /// 套餐
        /// </summary>
        public virtual ItemSuitDto ItemSuit { get; set; }

        /// <summary>
        /// 关联项目
        /// </summary>
        public virtual ICollection<ChargeGroupsDto> ChargeGroups { get; set; }

        /// <summary>
        /// 项目金额
        /// </summary>
        public decimal GroupsMoney { get; set; }

        /// <summary>
        /// 加项金额
        /// </summary>
        public decimal AddGroupMoney { get; set; }

        /// <summary>
        /// 减项金额
        /// </summary>
        public decimal SubtractMoney { get; set; }

        /// <summary>
        /// 调项金额
        /// </summary>
        public decimal AdjustmentMoney { get; set; }

        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal ReceivableMoney { get; set; }

        /// <summary>
        /// 已收金额
        /// </summary>
        public decimal CollectedMoney { get; set; }

        /// <summary>
        /// 剩余金额
        /// </summary>
        public decimal SurplusMoney { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public virtual decimal Discontmoney { get; set; }
    }
}