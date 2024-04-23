using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.AddStatistics.Dto
{
    /// <summary>
    /// 加项统计
    /// </summary>
   public class AddStatisticsDto
    {
        /// <summary>
        /// 操作人姓名
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 体检组合名称
        /// </summary>
        public virtual string ItemGroupName { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 体检人名称
        /// </summary>
        public virtual string CustomerName { get; set; }
        /// <summary>
        /// 原价
        /// </summary>
        public virtual decimal ItemPrice { get; set; }
        /// <summary>
        /// 加项价格
        /// </summary>
        public virtual decimal PriceAfterDis { get; set; }
        /// <summary>
        /// 体检日期
        /// </summary>
        public virtual DateTime CreationTime { get; set; }
    }
}
