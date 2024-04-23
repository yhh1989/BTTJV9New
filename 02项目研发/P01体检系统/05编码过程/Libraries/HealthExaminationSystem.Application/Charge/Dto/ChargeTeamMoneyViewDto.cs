using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#elif Proxy
using System.Linq;
using Newtonsoft.Json;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
#if Application
    [AutoMap(typeof(TjlClientTeamInfo))]
#endif
    public class ChargeTeamMoneyViewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检预约集合
        /// </summary>
        public virtual ICollection<ChargeCusStateDto> CustomerReg { get; set; }

        /// <summary>
        /// 分组ID
        /// </summary>
        public virtual int TeamBM { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        [StringLength(256)]
        public virtual string TeamName { get; set; }

        /// <summary>
        /// 分组价格
        /// </summary>
        public virtual decimal TeamMoney { get; set; }

        /// <summary>
        /// 分组折扣
        /// </summary>
        public virtual decimal TeamDiscount { get; set; }

        /// <summary>
        /// 分组折扣后价格
        /// </summary>
        public virtual decimal TeamDiscountMoney { get; set; }

#if Proxy

/// <summary>
/// 应收金额
/// </summary>
        [JsonIgnore]
        public virtual decimal YingShouJinE
        {
            get
            {
                var mcusPayMoneys = CustomerReg?.Select(r => r.McusPayMoney);
                var sumMcusPayMoneys = mcusPayMoneys?.Sum(r => r?.ClientMoney);
                return sumMcusPayMoneys ?? 0.00m;
            }
        }

        /// <summary>
        /// 实检金额
        /// </summary>
        [JsonIgnore]
        public virtual decimal ShiJianJinE
        {
            get
            {
                var mcusPayMoneys = CustomerReg?.Where(r => r.CheckSate != 1).Select(r => r.McusPayMoney);
                var sumMcusPayMoneys = mcusPayMoneys?.Sum(r => r?.ClientMoney);
                return sumMcusPayMoneys ?? 0.00m;
            }
        }

        /// <summary>
        /// 加项金额
        /// </summary>
        [JsonIgnore]
        public virtual decimal AddJinE
        {
            get
            {
                var mcusPayMoneys = CustomerReg?.Select(r => r.McusPayMoney);
                var sumMcusPayMoneys = mcusPayMoneys?.Sum(r => r?.ClientAddMoney);
                return sumMcusPayMoneys ?? 0.00m;
            }
        }

        /// <summary>
        /// 减项金额
        /// </summary>
        [JsonIgnore]
        public virtual decimal JianxiangJinE
        {
            get
            {
                var mcusPayMoneys = CustomerReg?.Select(r => r.McusPayMoney);
                var sumMcusPayMoneys = mcusPayMoneys?.Sum(r => r?.ClientMinusMoney);
                return sumMcusPayMoneys ?? 0.00m;
            }
        }

        /// <summary>
        /// 体检人数
        /// </summary>
        [JsonIgnore]
        public virtual int TiJianRenShu
        {
            get
            {
                var count = CustomerReg?.Count;
                return count ?? 0;
            }
        }

        /// <summary>
        /// 实检人数
        /// </summary>
        [JsonIgnore]
        public virtual int SumSJRS
        {
            get
            {
                var count = CustomerReg?.Where(r => r.CheckSate != 1).Count();
                return count ?? 0;
            }
        }
#endif
    }
}