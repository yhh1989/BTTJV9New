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
    [Abp.AutoMapper.AutoMap(typeof(TjlCustomerReg))]
#endif
    public class CustomerGroupMoneyDto
    {
        /// <summary>
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }
        /// <summary>
        /// 团体应收已收
        /// </summary>
        public virtual ClientPayMoneyViewDto McusPayMoney { get; set; }
        /// <summary>
        /// 组合
        /// </summary>
        public virtual ICollection<ClientGroupMoneyDto> CustomerItemGroup { get; set; }

    }
}
