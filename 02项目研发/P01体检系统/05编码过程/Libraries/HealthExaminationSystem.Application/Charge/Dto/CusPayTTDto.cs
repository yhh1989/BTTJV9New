#if Application
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
#if Application
    [Abp.AutoMapper.AutoMap(typeof(TjlMcusPayMoney))]
#endif
    public class CusPayTTDto
    {
        
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
