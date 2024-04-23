using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
   public class GuIdDto : EntityDto<Guid>
    {
        /// <summary>
        /// 退费方式
        /// </summary>
        public virtual Guid? PaymentId { get; set; }

        /// <summary>
        /// 退费金额（部分退费使用）
        /// </summary>
        public virtual decimal? Remoney { get; set; }

    }
}
