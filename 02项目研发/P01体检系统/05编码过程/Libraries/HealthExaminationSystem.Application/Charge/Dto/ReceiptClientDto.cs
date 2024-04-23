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
    /// <summary>
    /// 收费记录表
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlMReceiptInfo))]
#endif
    public class ReceiptClientDto
    {     

        /// <summary>
        /// 实收
        /// </summary>
        public virtual decimal Actualmoney { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual decimal Discount { get; set; }  

        /// <summary>
        /// 收费状态:1正常收费2作废3已作废
        /// </summary>
        public virtual int ReceiptSate { get; set; }
    

    }
}
