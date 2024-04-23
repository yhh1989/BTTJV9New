using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Sw.Hospital.HealthExaminationSystem.Application.Market.Dto
{
    /// <summary>
    /// 合同信息数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Core.Market.ContractInformation))]
#endif
    public class ContractInformationFullDto : ContractInformationDto
    {
        /// <summary>
        /// 回款记录集合
        /// </summary>
        public virtual List<ContractProceedsDto> ProceedsCollection { get; set; }

        /// <summary>
        /// 开票记录集合
        /// </summary>
        public virtual List<ContractInvoiceDto> InvoiceCollection { get; set; }

#if Proxy
        /// <summary>
        /// 回款金额
        /// </summary>
        [JsonIgnore]
        public decimal ProceedsAmount
        {
            get
            {
                if (ProceedsCollection == null)
                {
                    return 0;
                }

                return ProceedsCollection.Sum(r => r.Amount);
            }
        }

        /// <summary>
        /// 回款比例
        /// </summary>
        [JsonIgnore]
        public decimal ProceedsPercentage
        {
            get
            {
                if (Amount == 0)
                {
                    return 1;
                }

                return ProceedsAmount / Amount;
            }
        }

        /// <summary>
        /// 开票金额
        /// </summary>
        [JsonIgnore]
        public decimal InvoiceAmount
        {
            get
            {
                if (InvoiceCollection == null)
                {
                    return 0;
                }

                return InvoiceCollection.Sum(r => r.Amount);
            }
        }

        /// <summary>
        /// 开票比例
        /// </summary>
        [JsonIgnore]
        public decimal InvoicePercentage
        {
            get
            {
                if (ProceedsAmount == 0)
                {
                    return 1;
                }

                return InvoiceAmount / ProceedsAmount;
            }
        }
#endif
    }
}