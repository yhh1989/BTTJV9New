using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto
{
    /// <summary>
    /// 单位预约登记
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlClientReg))]
#endif
    public class GroupClientRegDto : EntityDto<Guid>
    {
        /// <summary>
        /// 单位信息
        /// </summary>
        public virtual GroupClientInfoDto ClientInfo { get; set; }

        /// <summary>
        /// 单位分组信息
        /// </summary>
        public virtual List<GroupClientTeamInfoDto> ClientTeamInfo { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime StartCheckDate { get; set; }

        /// <summary>
        /// 预约日期起
        /// </summary>
        public virtual DateTime? ClientInfoStart { get; set; }

        /// <summary>
        /// 预约日期止
        /// </summary>
        public virtual DateTime? ClientInfoEnd { get; set; }
    }
}