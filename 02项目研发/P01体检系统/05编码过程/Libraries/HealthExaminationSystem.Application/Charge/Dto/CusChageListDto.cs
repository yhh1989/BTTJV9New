using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
    /// <summary>
    /// 结算支付方式记录表
    /// </summary>
#if !Proxy
    [AutoMapFrom(typeof(TjlMPayment))]
#endif
    public class CusChageListDto
    {
        /// <summary>
        /// 支付方式名称
        /// </summary>
        public virtual string MChargeTypename { get; set; }

        /// <summary>
        /// 折扣价格
        /// </summary>
        public virtual decimal Actualmoney { get; set; }
    }
}
