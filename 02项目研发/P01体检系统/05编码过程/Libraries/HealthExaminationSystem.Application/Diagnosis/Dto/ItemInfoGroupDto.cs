using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Diagnosis.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmItemGroup))]
#endif
    public class ItemInfoGroupDto : EntityDto<Guid>
    {
        /// <summary>
        /// 科室名称
        /// </summary>
        public virtual DepartmentIdNameDto Department { get; set; }
        /// <summary>
        /// 项目组合名称
        /// </summary>
        public virtual string ItemGroupName { get; set; }

        /// <summary>
        /// 项目信息
        /// </summary>
        public virtual List<ItemInfoDiagnosisDto> ItemInfos { get; set; }
    }
}