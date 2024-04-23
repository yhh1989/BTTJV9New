using Abp.Application.Services.Dto;
using System;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 科室设置
    /// 医生站读取患者信息用
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TbmDepartment))]
#endif
    public class ATbmDepartmentDto : EntityDto<Guid>
    {
    }
}
