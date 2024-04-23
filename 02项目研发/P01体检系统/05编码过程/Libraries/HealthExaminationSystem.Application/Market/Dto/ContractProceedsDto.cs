using System;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Market.Dto
{
    /// <summary>
    /// 合同回款记录数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Core.Market.ContractProceeds))]
#endif
    public class ContractProceedsDto : EntityDto<Guid>
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
        /// 回款日期
        /// </summary>
        public DateTime Date { get; set; }
    }
}