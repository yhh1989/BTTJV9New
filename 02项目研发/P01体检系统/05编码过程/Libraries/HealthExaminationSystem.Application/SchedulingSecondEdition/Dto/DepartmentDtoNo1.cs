using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.SchedulingSecondEdition.Dto
{
    /// <summary>
    /// 科室信息数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Core.Coding.TbmDepartment))]
#endif
    public class DepartmentDtoNo1 : EntityDto<Guid>
    {
        /// <summary>
        /// 科室名称
        /// </summary>
        [Required]
        [StringLength(64)]
        public string Name { get; set; }
    }
}