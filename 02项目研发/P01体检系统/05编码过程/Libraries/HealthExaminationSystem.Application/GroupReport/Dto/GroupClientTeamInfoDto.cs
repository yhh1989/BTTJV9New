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
    /// 单位分组信息
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlClientTeamInfo))]
#endif
    public class GroupClientTeamInfoDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检预约集合
        /// </summary>
        public virtual List<GroupCustomerRegDto> CustomerReg { get; set; }


        /// <summary>
        /// 分组ID
        /// </summary>
        public virtual int TeamBM { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public virtual string TeamName { get; set; }

    }
}