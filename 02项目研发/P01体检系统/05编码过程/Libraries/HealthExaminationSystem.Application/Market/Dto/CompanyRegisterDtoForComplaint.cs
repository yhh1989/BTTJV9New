using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Newtonsoft.Json;

namespace Sw.Hospital.HealthExaminationSystem.Application.Market.Dto
{
    /// <summary>
    /// 单位预约数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Core.Company.TjlClientReg))]
#endif
    public class CompanyRegisterDtoForComplaint : EntityDto<Guid>
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime StartCheckDate { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [StringLength(2048)]
        public virtual string Remark { get; set; }

#if Proxy
        /// <summary>
        /// 显示成员
        /// </summary>
        [JsonIgnore]
        public string DisplayMember => string.Concat(Remark, "|", StartCheckDate.ToString("d"));
#endif
    }
}