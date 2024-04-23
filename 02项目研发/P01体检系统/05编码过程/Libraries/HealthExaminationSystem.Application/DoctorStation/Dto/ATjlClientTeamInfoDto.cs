using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;

#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 单位分组信息
    /// 医生站读取患者信息用
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlClientTeamInfo))]
#endif
    public class ATjlClientTeamInfoDto : EntityDto<Guid>
    {
        /// <summary>
        /// 分组ID
        /// </summary>
        public virtual int TeamBM { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        [StringLength(256)]
        public virtual string TeamName { get; set; }

        /// <summary>
        /// 单位预约登记
        /// </summary>
        [Required]
        public virtual ATjlClientRegDto ClientReg { get; set; }
    }
}