using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmItemGroup))]
#endif
    public class TbmItemGroupDto: FullItemGroup
    {       
        public virtual SimpleItemGroupDto ItemGroup { get; set; }
        /// <summary>
        /// 科室
        /// </summary>
        public virtual DepartmentSimpleDto Department { get; set; }
    }
}
