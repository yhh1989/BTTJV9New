using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
  public   class WebChargeOutDto
    {
        /// <summary>
        /// 200:调用成功，其他失败
        /// </summary>
        public string code { get; set; }
        /// <summary>
        ///返回结果描述
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string merchant_id { get; set; }
        /// <summary>
        /// 支付平台支付订单号
        /// </summary>
        public string pay_order_id { get; set; }
        /// <summary>
        /// 支付渠道
        /// </summary>
        public string channel_type { get; set; }
        /// <summary>
        /// 支付相关参数，BASE64 格式的字符
        /// </summary>
        public string pay_data { get; set; }
        /// <summary>
        /// 返回时间  格式：yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string timestamp { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
    }
}
