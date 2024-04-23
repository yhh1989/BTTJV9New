using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
   public  class ZFDZFP
    {
        /// <summary>
        /// 销方税号
        /// </summary>
        public virtual string xfsh { get; set; }

        /// <summary>
        /// 流水号
        /// </summary>
        public virtual string xsdjbh { get; set; }

        /// <summary>
        /// 分机号
        /// </summary>
        public virtual string fjh { get; set; }

        /// <summary>
        /// 终端号
        /// </summary>
        public virtual string zdh { get; set; }

        /// <summary>
        /// 发票种类
        /// </summary>
        public virtual string fpzl { get; set; }

        /// <summary>
        /// 发票代码
        /// </summary>
        public virtual string fpdm { get; set; }

        /// <summary>
        /// 发票号码
        /// </summary>
        public virtual string fphm { get; set; }
    }
}
