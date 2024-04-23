using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Foreground.Dto
{
    /// <summary>
    /// 单位信息数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Core.Company.TjlClientInfo))]
#endif
    public class CompanyInformation52Dto : EntityDto<Guid>
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        [StringLength(256)]
        [Required]
        public virtual string ClientName { get; set; }
    }
}