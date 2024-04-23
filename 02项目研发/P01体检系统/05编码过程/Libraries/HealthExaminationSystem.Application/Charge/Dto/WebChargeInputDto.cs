using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
   public class WebChargeInputDto
    {
        /// <summary>
        /// 体检人预约主键
        /// </summary>
   
        public virtual Guid? CustomerRegBMId { get; set; } 
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        ///订单总金额（单位：元，精确到小数点后两位）
        /// </summary>
        public string total_amount { get; set; }
        /// <summary>
        /// 用户付款码，付款码支付（被扫）必传
        /// </summary>
        public string auth_code { get; set; }
        /// <summary>
        /// 请求时间 格式：yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string timestamp { get; set; }

        /// <summary>
        /// 商户退款单号
        /// </summary>
        public string out_refund_no { get; set; }
    }
}
