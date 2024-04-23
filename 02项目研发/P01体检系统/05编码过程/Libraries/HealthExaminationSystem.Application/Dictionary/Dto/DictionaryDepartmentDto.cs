using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Dictionary.Dto
{
#if Application
    [AutoMap(typeof(TbmDepartment))]
#endif
    public class DictionaryDepartmentDto : EntityDto<Guid>
    {
        /// <summary>
        /// 科室名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 科室序号
        /// </summary>
        public virtual int? OrderNum { get; set; }

    }

}