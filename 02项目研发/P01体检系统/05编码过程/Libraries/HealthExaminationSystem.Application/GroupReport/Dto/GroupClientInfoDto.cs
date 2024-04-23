using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto
{
    /// <summary>
    /// 单位信息
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlClientInfo))]
#endif
    public class GroupClientInfoDto : EntityDto<Guid>
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

        /// <summary>
        /// 预约日期起
        /// </summary>
        public virtual DateTime? ClientRegStart { get; set; }

        /// <summary>
        /// 预约日期止
        /// </summary>
        public virtual DateTime? ClientRegEnd { get; set; }







    }
}