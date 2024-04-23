#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlCustomerItemGroup))]
#endif
    public class ClientGroupMoneyDto
    {
        /// <summary>
        /// 项目检查状态 1未检查2已检查3部分检查4放弃5待查
        /// </summary>
        public virtual int? CheckState { get; set; }
        /// <summary>
        /// 团体支付金额
        /// </summary>
        public virtual decimal TTmoney { get; set; }
    }
}
