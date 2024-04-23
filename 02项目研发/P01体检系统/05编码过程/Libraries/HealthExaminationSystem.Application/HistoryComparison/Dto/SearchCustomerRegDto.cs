using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison.Dto
{
    /// <summary>
    /// 体检人预约信息
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomerReg))]
#endif
    public class SearchCustomerRegDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检人项目组合
        /// </summary>
        public virtual ICollection<CustomerItemGroupDto> CustomerItemGroup { get; set; }

        /// <summary>
        /// 体检人科室小结
        /// </summary>
        public virtual ICollection<SearchCustomerDepSummaryDto> CustomerDepSummary { get; set; }

        ///// <summary>
        ///// 体检人专科建议
        ///// </summary>
        //public virtual ICollection<SearchCustomerSummaryDto> CustomerSummary { get; set; }

        /// <summary>
        /// 体检ID
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }
    }
}
