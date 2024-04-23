using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
    /// <summary>
    /// 团体预约数据传输对象（打印专用）
    /// </summary>
#if Application
    [AutoMapFrom(typeof(TjlClientReg))]
#endif
    public class CompanyRegisterForPrintDto : EntityDto<Guid>
    {
        /// <summary>
        /// 单位信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        public virtual Guid ClientInfoId { get; set; }

        /// <summary>
        /// 单位信息
        /// </summary>
        [Required]
        public virtual ClientInfoCacheDto ClientInfo { get; set; }

        /// <summary>
        /// 单位分组信息
        /// </summary>
        public virtual List<GroupClientTeamInfoDto> ClientTeamInfo { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime StartCheckDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime EndCheckDate { get; set; }
    }
}