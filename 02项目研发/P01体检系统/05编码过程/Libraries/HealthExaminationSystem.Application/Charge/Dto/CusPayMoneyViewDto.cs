using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlMcusPayMoney))]
#endif
    public class CusPayMoneyViewDto : EntityDto<Guid>
    {

        /// <summary>
        /// 个人应收金额
        /// </summary>
        public virtual decimal PersonalMoney { get; set; }
        /// <summary>
        /// 个人应收加项
        /// </summary>
        public virtual decimal PersonalAddMoney { get; set; }

        /// <summary>
        /// 个人应收减项
        /// </summary>
        public virtual decimal PersonalMinusMoney { get; set; }

        /// <summary>
        /// 个人调项加金额
        /// </summary>
        public virtual decimal PersonalAdjustAddMoney { get; set; }

        /// <summary>
        /// 个人调项减金额
        /// </summary>
        public virtual decimal PersonalAdjustMinusMoney { get; set; }


        /// <summary>
        /// 个人已收
        /// </summary>
        public virtual decimal PersonalPayMoney { get; set; }

        /// <summary>
        /// 团体应收金额
        /// </summary>
        public virtual decimal ClientMoney { get; set; }

        /// <summary>
        /// 团体应收加项
        /// </summary>
        public virtual decimal ClientAddMoney { get; set; }

        /// <summary>
        /// 团体应收减项
        /// </summary>
        public virtual decimal ClientMinusMoney { get; set; }

        /// <summary>
        /// 团体调项加金额
        /// </summary>
        public virtual decimal ClientAdjustAddMoney { get; set; }

        /// <summary>
        /// 团体调项减金额
        /// </summary>
        public virtual decimal ClientAdjustMinusMoney { get; set; }

    }
}
