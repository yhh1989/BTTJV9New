using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Department.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmDepartment))]
#endif
    public class DepartmentIdNameDto : EntityDto<Guid>
    {
        /// <summary>
        /// 科室名称
        /// </summary>
        [Required]
        [StringLength(64)]
        public virtual string Name { get; set; }
        /// <summary>
        /// 科室序号
        /// </summary>
        public virtual int? OrderNum { get; set; }
    }
}