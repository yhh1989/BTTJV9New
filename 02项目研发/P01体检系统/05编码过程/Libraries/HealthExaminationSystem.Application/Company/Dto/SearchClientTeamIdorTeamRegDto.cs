using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
    /// <summary>
    /// 同步数据
    /// </summary>
    public class SearchClientTeamIdorTeamRegDto : EntityDto<Guid>
    {
        /// <summary>
        /// 单位分组登记组合集合
        /// </summary>
        public List<ClientTeamRegitemViewDto> ClientTeamRegItem { get; set; }
    }
}