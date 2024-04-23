using System;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.FrmSeleteItemGroup.Dto
{
    /// <summary>
    /// 科室设置
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TbmDepartment))]
#endif
    public class SelectDepartmentDto : EntityDto<Guid>
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