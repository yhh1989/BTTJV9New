using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
   public class WebChargeShow
    {
        /// <summary>
        /// 体检号
        /// </summary>       
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>        
        public virtual string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }
        /// <summary>
        /// 商户号（由支付平台分配）
        /// </summary>
        [StringLength(32)]
        public string merchant_id { get; set; }
        /// <summary>
        /// 收银点编号
        /// </summary>
        [StringLength(32)]
        public string cashier_id { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        [StringLength(32)]
        public string oper_no { get; set; }
        /// <summary>
        ///商户订单号
        /// </summary>
        [StringLength(32)]
        public string out_trade_no { get; set; }
        /// <summary>
        ///订单总金额（单位：元，精确到小数点后两位）
        /// </summary>
        public string total_amount { get; set; }
        /// <summary>
        ///  商品名称
        /// </summary>
        [StringLength(32)]
        public string body { get; set; }
        /// <summary>
        ///  收费状态
        /// </summary>
        [StringLength(32)]
        public string Mess { get; set; }

        public DateTime? createTime { get; set; }

        /// <summary>
        /// 结算ID
        /// </summary>      
        public virtual Guid? MReceiptInfoID { get; set; }
    }
}
