#if !Proxy
using Abp.AutoMapper;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto
{
    /// <summary>
    /// 查询客户预约信息Dto
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlOccQuesFamilyHistory))]
#endif
    public class ReportOccQuesFamilyHistoryDto
    {
        /// <summary>
        /// 家族史疾病
        /// </summary>
        [StringLength(500)]
        public virtual string IllName { get; set; }
        /// <summary>
        /// 疾病关系人
        /// </summary>
        [StringLength(500)]
        public virtual string relatives { get; set; }
        /// <summary>
        /// 报告签名
        /// </summary>
#if !Proxy
                [IgnoreMap]
#endif
        public virtual string 受检人签名 { get; set; }
    }
}
