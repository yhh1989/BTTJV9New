using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public  class OutResult
    {
        /// <summary>
        /// 体检号
        /// </summary>      
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>      
        public virtual string OrderId { get; set; }
        /// <summary>
        /// 编码
        /// </summary>      
        public virtual int? Code { get; set; }
        /// <summary>
        /// 异常详情
        /// </summary>      
        public virtual string ErrInfo { get; set; }
        /// <summary>
        /// 收费Id（退费用）
        /// </summary>

        public virtual Guid? ReceiptId { get; set; }

    }
}
