using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
   public  class BusiCusRegDto
    {
        /// <summary>
        /// 订单编码
        /// </summary>
        public virtual string ClientRegBM { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>       
      
        public virtual string ClientName { get; set; }

        /// <summary>
        /// 体检号
        /// </summary>     
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public virtual string linkMan { get; set; }

        /// <summary>
        ///体检日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }
        /// <summary>
        /// 登记状态 1未登记 2已登记
        /// </summary>
        public virtual int? RegisterState { get; set; }

        /// <summary>
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }

        /// <summary>
        /// 总检状态 1未总检2已分诊3已初检4已审核5审核不通过
        /// </summary>
        public virtual int? SummSate { get; set; }

        /// <summary>
        /// 报告打印状态 1未打印2已打印
        /// </summary>
        public virtual int? PrintSate { get; set; }
    }
}
