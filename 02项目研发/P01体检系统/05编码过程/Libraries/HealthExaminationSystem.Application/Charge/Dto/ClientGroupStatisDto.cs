using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
  public  class ClientGroupStatisDto
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }
        /// <summary>
        /// 体检科室
        /// </summary>
        public virtual string DepartName { get; set; }

        /// <summary>
        /// 组合名称
        /// </summary>
        public virtual string CroupName { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public virtual decimal? ItemPrice { get; set; }
        /// <summary>
        /// 登记数量
        /// </summary>
        public virtual int? RegCount { get; set; }
        /// <summary>
        /// 加项状态
        /// </summary>
        public virtual int? AddState { get; set; }
        /// <summary>
        /// 总原价额
        /// </summary>
        public virtual decimal? AllGroupMoney { get; set; }
        /// <summary>
        /// 不打折总原价额
        /// </summary>
        public virtual decimal? NZKMoney { get; set; }
        /// <summary>
        /// 打折总原价额
        /// </summary>
        public virtual decimal? ZKMoney { get; set; }

        /// <summary>
        /// 检查数量
        /// </summary>
        public virtual int? CheckCount { get; set; }
       
        /// <summary>
        /// 总原价额
        /// </summary>
        public virtual decimal? CheckGroupMoney { get; set; }
        /// <summary>
        /// 不打折总原价额
        /// </summary>
        public virtual decimal? CheckNZKMoney { get; set; }
        /// <summary>
        /// 打折总原价额
        /// </summary>
        public virtual decimal? CheckZKMoney { get; set; }


        /// <summary>
        ///科室总金额
        /// </summary>
        public virtual decimal? DepartMoney { get; set; }

        /// <summary>
        /// 团体支付金额
        /// </summary>
        public virtual decimal? TTmoney { get; set; }
    }
}
