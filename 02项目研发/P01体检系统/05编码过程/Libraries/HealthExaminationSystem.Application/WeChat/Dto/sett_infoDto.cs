using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
  public  class sett_infoDto
    {
        /// <summary>
        /// 结算方式
        /// </summary>
        public string pay_type { get; set; }

        /// <summary>
        /// 结算金额
        /// </summary>
        public decimal? sett_price { get; set; }

        /// <summary>
        /// 支付交易流水号
        /// </summary>
        public string pay_no { get; set; }
    }
}
