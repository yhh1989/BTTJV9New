using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OAApproval.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmClientZKSet))]
#endif
    public class CreateClientXKSetDto : EntityDto<Guid>
    {
        /// <summary>
        /// 折扣类型
        /// </summary>     
        public virtual int? ZKType { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>     
        public virtual Decimal? DiscountRate { get; set; }

    }
}
