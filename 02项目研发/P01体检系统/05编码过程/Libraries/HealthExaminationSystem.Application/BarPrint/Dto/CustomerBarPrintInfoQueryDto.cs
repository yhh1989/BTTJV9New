using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.BarPrintInfoItemGroup.Dto;
using System.Collections.Generic;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Abp.AutoMapper;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlCustomerBarPrintInfo))]
#endif
    public class CustomerBarPrintInfoQueryDto : EntityDto<Guid>
    {
       

        /// <summary>
        /// 条码ID
        /// </summary>
        public virtual BarSettingDto BarSettings { get; set; }
        /// <summary>
        /// 打印条码
        /// </summary>
        public virtual List<BarPrintInfoItemGroupQueryDto> customerBarPrintInfo { get; set; }

        /// <summary>
        /// 条码名称
        /// </summary>
        [StringLength(1000)]
        public virtual string BarName { get; set; }

        /// <summary>
        /// 条码编号
        /// </summary>
        [StringLength(32)]
        public virtual string BarNumBM { get; set; }

        /// <summary>
        /// 打印时间
        /// </summary>
        public virtual DateTime? BarPrintTime { get; set; }

        /// <summary>
        /// 打印次数
        /// </summary>
        public virtual int? BarPrintCount { get; set; }

        public int TenantId { get; set; }
    }
}