using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class ChargeCusRegDto : EntityDto<Guid>
    {
        /// <summary>
        /// 人员信息
        /// </summary>
        public virtual ChargeCusDto Customer { get; set; }

        /// <summary>
        /// 组合信息
        /// </summary>
        public virtual ICollection<ChargeGroupsDto> CustomerItemGroup { get; set; }

        /// <summary>
        /// 收费信息
        /// </summary>
        public virtual ICollection<MReceiptInfoPerDto> MReceiptInfo { get; set; }

        /// <summary>
        /// 单位预约信息
        /// </summary>

        public virtual ChargeClientRegDto ClientReg { get; set; }

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }

        /// <summary>
        /// 体检ID
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 体检类别 字典
        /// </summary>
        [StringLength(16)]
        public virtual string ClientType { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>
        [StringLength(128)]
        public virtual ItemSuitDto ItemSuitBM { get; set; }

        /// <summary>
        /// 个人结账ID
        /// </summary>  
        public virtual Guid? MReceiptInfoPersonalId { get; set; }
    }
}