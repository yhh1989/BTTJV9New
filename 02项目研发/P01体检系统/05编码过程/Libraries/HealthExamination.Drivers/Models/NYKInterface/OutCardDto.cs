using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExamination.Drivers.Models.NYKInterface
{
   public  class OutCardDto
    {
       

        /// <summary>
        ///会员卡号
        /// </summary>       
        public virtual string cardno { get; set; }

        /// <summary>
        ///剩余金额金额(扣款后剩余金额)
        /// </summary>       
        public virtual decimal Amount { get; set; }

        /// <summary>
        ///编码（0扣款失败，1扣款成功）
        /// </summary>       
        public virtual int code { get; set; }

        /// <summary>
        ///失败说明
        /// </summary>       
        public virtual string Mess { get; set; }
    }
}
