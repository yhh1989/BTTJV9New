using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlWebCharge))]
#endif
    public class WebChargeDto
    {
        /// <summary>
        /// 商户号（由支付平台分配）
        /// </summary>
        public string merchant_id { get; set; }
        /// <summary>
        /// 收银点编号
        /// </summary>
        public string cashier_id { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string oper_no { get; set; }
        /// <summary>
        ///商户订单号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        ///订单总金额（单位：元，精确到小数点后两位）
        /// </summary>
        public string total_amount { get; set; }
        /// <summary>
        ///  商品名称
        /// </summary>
        public string body { get; set; }
        /// <summary>
        /// 订单过期时间 格式：yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string expire_time { get; set; }
        /// <summary>
        /// 收款渠道，联系创识
        /// </summary>
        public string channel_type { get; set; }
        /// <summary>
        /// 支付方式，联系创识
        /// </summary>
        public string pay_type { get; set; }
        /// <summary>
        /// 用户付款码，付款码支付（被扫）必传
        /// </summary>
        public string auth_code { get; set; }
        /// <summary>
        /// 微信支付分配 APPID，微信公众号支付必传
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 用户 openid，微信公众号/支付宝服务号支付必传
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 用户端 ip 地址，微信公众号+微信 APP 支付必传
        /// </summary>
        public string spbill_create_ip { get; set; }
        /// <summary>
        /// 支付成功跳转地址，直连微信支付宝渠道下使用
        /// </summary>
        public string callback_url { get; set; }
        /// <summary>
        /// 异步通知回调地址（客户端对接可不填）
        /// </summary>
        public string notify_url { get; set; }
        /// <summary>
        /// 附加字段，json 字符串
        /// </summary>
        public string attach { get; set; }
        /// <summary>
        /// 是否清分（综合收银台二清使用），1：是，其他：否，默认：否
        /// </summary>
        public string is_split_order { get; set; }

        /// <summary>
        /// 清分明细，json 数组字符串，is_split_order=1 情况下必传
        /// </summary>
        public string split_info { get; set; }

        /// <summary>
        /// 终端设备号，特殊情况下必传，联系创识
        /// </summary>
        public string terminal_id { get; set; }

        /// <summary>
        /// 是否清分（综合收银台二清使用），1：是，其他：否，默认：否
        /// </summary>
        public string location { get; set; }

        /// <summary>
        /// 请求时间 格式：yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string timestamp { get; set; }

      


    }
}
