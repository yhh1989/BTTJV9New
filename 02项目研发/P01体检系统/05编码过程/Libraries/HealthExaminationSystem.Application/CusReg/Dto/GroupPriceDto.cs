using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
   public  class GroupPriceDto : EntityDto<Guid>
    {
        /// <summary>
        /// 组合价格
        /// </summary>
        public virtual decimal ItemPrice { get; set; }
    }
}
