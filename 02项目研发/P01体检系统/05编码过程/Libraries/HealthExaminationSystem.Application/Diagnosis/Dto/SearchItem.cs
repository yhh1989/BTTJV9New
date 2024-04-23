using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Diagnosis.Dto
{
    public class SearchItem
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual Guid? ClientRegId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime? StartDateTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime? EndDateTime { get; set; }
        /// <summary>
        /// 项目Id
        /// </summary>
        public virtual List<Guid>ItemID { get; set; }

        /// <summary>
        /// 项目结果最小值
        /// </summary>
        public virtual decimal? MinValue { get; set; }
        /// <summary>
        /// 项目结果最大值
        /// </summary>
        public virtual decimal? MaxValue { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public virtual string IllStr { get; set; }

        /// <summary>
        /// 是否异常
        /// </summary>
        public virtual bool ISIll { get; set; }


    }
}
