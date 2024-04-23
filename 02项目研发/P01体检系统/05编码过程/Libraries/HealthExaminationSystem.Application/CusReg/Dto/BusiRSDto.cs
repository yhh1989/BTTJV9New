using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
  public  class BusiRSDto
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
        /// 分组名称
        /// </summary>
        public virtual string TeamName { get; set; }
        /// <summary>
        /// 销售人
        /// </summary>
        public virtual string linkMan { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public virtual int? RegCount { get; set; }
        /// <summary>
        /// 商务金额
        /// </summary>
        public virtual decimal? BusiMoney { get; set; }
        /// <summary>
        /// 合计金额
        /// </summary>
        public virtual decimal? teamMoney { get; set; }
    }
}
