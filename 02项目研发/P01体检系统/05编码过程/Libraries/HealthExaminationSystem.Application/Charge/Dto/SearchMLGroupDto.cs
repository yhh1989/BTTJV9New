using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
   public  class SearchMLGroupDto : EntityDto<Guid>
    {
        /// <summary>
        /// 预约ID
        /// </summary>  
        public virtual Guid CustomerRegID { get; set; }
        /// <summary>
        /// 结算ID
        /// </summary>  
        public virtual Guid MReceiptInfoPersonalId { get; set; }
        /// <summary>
        /// 抹零金额
        /// </summary>
        public virtual decimal MLMoney { get; set; }

    }
}
