using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public  class InCusPayDto
    {
        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>      
        public virtual DateTime? PayTime { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>     
        public virtual decimal? PayMoney { get; set; }

        /// <summary>
        /// 支付项目组合明细
        /// </summary>       
        public virtual List<PayGroupDto> PayGroups { get; set; }
    }
}
