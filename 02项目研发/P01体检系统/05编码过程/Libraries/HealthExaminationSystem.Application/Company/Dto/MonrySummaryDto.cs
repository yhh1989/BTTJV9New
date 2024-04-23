using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !Proxy
using AutoMapper;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
    /// <summary>
    /// 体检统计总表
    /// </summary>
    public class MonrySummaryDto
    {
        /// <summary>
        /// 人员组合Dto
        /// </summary>
        public ICollection<ChargeCusStateDto> CustomerReg { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public virtual ClientInfoRegDto ClientInfo { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }
        /// <summary>
        /// 团体支付总计
        /// </summary>
        public virtual decimal? GroupToPay { get; set; }
        /// <summary>
        /// 团体已支付总计
        /// </summary>
        public virtual decimal? GroupHavePay { get; set; }
        /// <summary>
        /// 个人支付总计
        /// </summary>
        public virtual decimal? PersonageToPay { get; set; }
        /// <summary>
        /// 个人已支付总计
        /// </summary>
        public virtual decimal? PersonageHavePay { get; set; }

    }
}
