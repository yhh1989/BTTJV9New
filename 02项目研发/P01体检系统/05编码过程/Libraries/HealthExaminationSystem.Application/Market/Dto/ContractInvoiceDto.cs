using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Market.Dto
{
    /// <summary>
    /// 合同开票记录数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Core.Market.ContractInvoice))]
#endif
    public class ContractInvoiceDto : EntityDto<Guid>
    {
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 合同标识
        /// </summary>
        public Guid ContractId { get; set; }

        /// <summary>
        /// 开票日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 发票号码
        /// </summary>
        [StringLength(maximumLength: 64)]
        public string InvoiceNumber { get; set; }
    }
}