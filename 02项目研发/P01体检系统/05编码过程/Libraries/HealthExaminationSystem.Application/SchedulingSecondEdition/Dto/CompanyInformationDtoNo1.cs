using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.SchedulingSecondEdition.Dto
{
    /// <summary>
    /// 公司信息数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Core.Company.TjlClientInfo))]
#endif
    public class CompanyInformationDtoNo1 : EntityDto<Guid>
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        [StringLength(256)]
        [Required]
        public string ClientName { get; set; }

        /// <summary>
        /// 单位简称
        /// </summary>
        [StringLength(256)]
        public string ClientAbbreviation { get; set; }
    }
}