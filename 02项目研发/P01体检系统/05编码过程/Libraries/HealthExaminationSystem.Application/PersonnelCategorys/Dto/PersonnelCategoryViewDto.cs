using System;
using Abp.Application.Services.Dto;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.PersonnelCategorys.Dto
{
#if Application
    [AutoMap(typeof(PersonnelCategory))]
#endif
    public class PersonnelCategoryViewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 类别名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否免费
        /// </summary>
        public bool IsFree { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
    }
}