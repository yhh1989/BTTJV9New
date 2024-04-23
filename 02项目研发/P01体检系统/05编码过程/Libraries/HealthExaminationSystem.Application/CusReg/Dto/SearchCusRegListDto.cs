using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchCusRegListDto
    {
        /// <summary>
        /// 查询天数
        /// </summary>
        public int Day { get; set; }
        /// <summary>
        /// 单位登记Id
        /// </summary>
        public Guid ClientRegId { get; set; }
        /// <summary>
        /// 体检状态
        /// </summary>
        public int CheckState { get; set; }
        /// <summary>
        /// 登记状态 1未登记 2已登记
        /// </summary>
        public int RegisterState { get; set; }
        /// <summary>
        /// 收费状态 1未收费6已收费7欠费
        /// </summary>
        public virtual int CostState { get; set; }
        /// <summary>
        /// 交表确认 1未交表2已交表
        /// </summary>
        public virtual int SendToConfirm { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime? Satr { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime? End { get; set; }

    }
}
