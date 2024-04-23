using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 单位信息
    /// 医生站读取患者信息用
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlClientInfo))]
#endif
    public class ATjlClientInfoDto : EntityDto<Guid>
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        [StringLength(256)]
        [Required]
        public virtual string ClientName { get; set; }
    }
}
