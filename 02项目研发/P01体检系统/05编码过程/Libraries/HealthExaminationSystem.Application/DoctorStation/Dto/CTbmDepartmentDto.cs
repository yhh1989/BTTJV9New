using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 科室设置
    /// 查询科室总监建议设置
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TbmDepartment))]
#endif
    public class CTbmDepartmentDto : EntityDto<Guid>
    {
        /// <summary>
        /// 科室名称
        /// </summary>
        [StringLength(64)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 科室序号
        /// </summary>
        public virtual int? OrderNum { get; set; }
    }
}
