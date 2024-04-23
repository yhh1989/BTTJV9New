using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccConclusionStatistics.Dto
{
 public    class OccYearsSaticsDto
    {
        /// <summary>
        /// 统计名称
        /// </summary>
        public virtual string SaticsName { get; set; }
        /// <summary>
        /// 上岗前体检体检人数
        /// </summary>
        public virtual int? BeforeCheckCount { get; set; }

        /// <summary>
        /// 上岗前体检禁忌证人数
        /// </summary>
        public virtual int? BeforeJJZCount { get; set; }
        /// <summary>
        /// 在岗体检体检人数
        /// </summary>
        public virtual int? OnCheckCount { get; set; }

        /// <summary>
        /// 在岗体检禁忌证人数
        /// </summary>
        public virtual int? OnJJZCount { get; set; }

        /// <summary>
        /// 在岗体检职业病人数
        /// </summary>
        public virtual int? OnZYBCount { get; set; }

        /// <summary>
        /// 在岗体检复查人数
        /// </summary>
        public virtual int? OnFCCount { get; set; }

        /// <summary>
        /// 离岗体检人数
        /// </summary>
        public virtual int? AfterCheckCount { get; set; }

        /// <summary>
        /// 离岗体检职业病人数
        /// </summary>
        public virtual int? AfterZYBCount { get; set; }

        /// <summary>
        /// 离岗体检复查人数
        /// </summary>
        public virtual int? AfterFCCount { get; set; }

    }
}
