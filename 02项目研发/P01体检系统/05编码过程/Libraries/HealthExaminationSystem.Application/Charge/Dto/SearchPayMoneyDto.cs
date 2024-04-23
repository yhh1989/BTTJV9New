using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
  public   class SearchPayMoneyDto : EntityDto<Guid>
    {       
     /// <summary>
     /// 支付金额
     /// </summary>
        public virtual decimal PayMoney { get; set; }
        /// <summary>
        /// 抹零金额
        /// </summary>
    
        public virtual decimal DistMoney { get; set; }

    }
}
