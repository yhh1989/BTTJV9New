using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
    /// <summary>
    /// 电子发票返回
    /// </summary>
  public   class DZFPDout
    {
        /// <summary>
        /// 返回代码
        /// </summary>
        public virtual string returnCode { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public virtual string returnMsg { get; set; }
        /// <summary>
        /// 请求流水号
        /// </summary>
        public virtual string xsdjbh { get; set; }
        /// <summary>
        /// 合计金额
        /// </summary>
        public virtual string hjje { get; set; }
        /// <summary>
        /// 合计税额
        /// </summary>
        public virtual string hjse { get; set; }

        /// <summary>
        /// 开票日期
        /// </summary>
        public virtual string kprq { get; set; }

        /// <summary>
        /// 所属月份
        /// </summary>
        public virtual string ssyf { get; set; }

        /// <summary>
        /// 发票代码
        /// </summary>
        public virtual string fpdm { get; set; }

        /// <summary>
        /// 发票号码
        /// </summary>
        public virtual string fphm { get; set; }


        /// <summary>
        /// 清单标志
        /// </summary>
        public virtual string qdbz { get; set; }


        /// <summary>
        /// 发票密文
        /// </summary>
        public virtual string mw { get; set; }


        /// <summary>
        /// 校验码
        /// </summary>
        public virtual string jym { get; set; }


        /// <summary>
        /// 数字签名
        /// </summary>
        public virtual string qmz { get; set; }


        /// <summary>
        /// 二维码
        /// </summary>
        public virtual string ewm { get; set; }


        /// <summary>
        /// 机器编号
        /// </summary>
        public virtual string jqbh { get; set; }


      
    }
}
