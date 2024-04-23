using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 科室设置
    /// 查询项目字典表
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TbmDepartment))]
#endif
    public class BTbmDepartmentDto : EntityDto<Guid>
    {

    }
}
