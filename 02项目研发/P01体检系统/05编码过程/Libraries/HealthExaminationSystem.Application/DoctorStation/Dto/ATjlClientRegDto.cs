using Abp.Application.Services.Dto;
using System;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif
namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 单位预约登记
    /// 医生站读取患者信息用
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlClientReg))]
#endif
    public class ATjlClientRegDto : EntityDto<Guid>
    {
        /// <summary>
        /// 单位信息
        /// </summary>
        public virtual ATjlClientInfoDto ClientInfo { get; set; }
    }
}
