using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
   public  class CusReceivsDto
    {

        /// 体检号 
        /// </summary>
        public virtual string CusRegBM { get; set; }

        /// 姓名 
        /// </summary>
        public virtual string CusName { get; set; }
       
        /// 体检类型:1单位2体检人
        /// </summary>
        public virtual int TJType { get; set; }
        /// <summary>
        /// 应收:优惠
        /// </summary>
        public virtual decimal Shouldmoney { get; set; }

        /// <summary>
        /// 实收
        /// </summary>
        public virtual decimal Actualmoney { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual decimal Discount { get; set; }
        /// <summary>
        /// 收费员
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public virtual string FPH { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 收费时间
        /// </summary>
        public DateTime ChargeDate { get; set; }
    }
}
