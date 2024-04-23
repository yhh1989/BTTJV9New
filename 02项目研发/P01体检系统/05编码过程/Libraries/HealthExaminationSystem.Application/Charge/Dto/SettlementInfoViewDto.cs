using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
    /// <summary>
    /// 结算信息视图
    /// </summary>
    public class SettlementInfoViewDto:EntityDto<Guid>
    {
        /// <summary>
        /// 结算总额
        /// </summary>
        public decimal Total { get; set; }
        /// <summary>
        /// 已收金额
        /// </summary>
        public decimal Already { get; set; }
        /// <summary>
        /// 剩余金额
        /// </summary>
        public decimal Surplus { get; set; }
    }
}
