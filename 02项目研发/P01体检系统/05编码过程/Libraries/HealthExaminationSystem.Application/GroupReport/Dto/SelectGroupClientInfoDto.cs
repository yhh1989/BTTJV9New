using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto
{
    /// <summary>
    /// 单位信息查询
    /// </summary>
#if Application
    [AutoMap(typeof(TjlClientInfo))]
#endif
    public class SelectGroupClientInfoDto : EntityDto<Guid>
    {

        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }

        /// <summary>
        /// 单位简称
        /// </summary>
        public virtual string ClientAbbreviation { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        public virtual string HelpCode { get; set; }

        
    }
}