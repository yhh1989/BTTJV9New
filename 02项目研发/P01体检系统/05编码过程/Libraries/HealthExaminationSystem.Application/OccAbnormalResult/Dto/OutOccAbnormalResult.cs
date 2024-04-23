using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !Proxy
using Abp.AutoMapper;
using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif


namespace Sw.Hospital.HealthExaminationSystem.Application.OccAbnormalResult.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TjlCustomerSummarizeBM))]
#endif
    public class OutOccAbnormalResult
    {
        /// <summary>
        /// 异常结果名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 检出人数
        /// </summary>
        public int? CountNumber { get; set; }
        /// <summary>
        /// 检出率
        /// </summary>
        public decimal CheckNumber { get; set; }
        /// <summary>
        /// 男性人数
        /// </summary>
        public int? MenCount { get; set; }
        /// <summary>
        /// 男性检出率
        /// </summary>
        public decimal CheckMen { get; set; }
        /// <summary>
        /// 女性人数
        /// </summary>
        public int? WoMenCount { get; set; }
        /// <summary>
        /// 女性检出率
        /// </summary>
        public decimal CheckWoMen { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartCheckDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndCheckDate { get; set; }
        /// <summary>
        /// 年度
        /// </summary>
        public string YearTime { get; set; }
        public int? Sex { get; set; }
    }
}
