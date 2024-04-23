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
    /// <summary>
    /// 发票记录
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlMReceiptInfo))]
#endif
    public class UpdateTjlMcusMoneyDto : EntityDto<Guid>
    {
        /// <summary>
        /// 个人已收
        /// </summary>
        public virtual decimal PersonalPayMoney { get; set; }

    }
}
