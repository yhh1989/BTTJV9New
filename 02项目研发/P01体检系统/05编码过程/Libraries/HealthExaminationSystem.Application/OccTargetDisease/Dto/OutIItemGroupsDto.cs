using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease.Dto
{
    public class OutIItemGroupsDto
    {
        /// <summary>
        /// 必选项目
        /// </summary>
        public virtual List<SimpleItemGroupDto> MustItemGroups{get;set;}
        /// <summary>
        /// 可选项目
        /// </summary>
        public virtual List<SimpleItemGroupDto> MayItemGroups { get; set; }
    }
}
