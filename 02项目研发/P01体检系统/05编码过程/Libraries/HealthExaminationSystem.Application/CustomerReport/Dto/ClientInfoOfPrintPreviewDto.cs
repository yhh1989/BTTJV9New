using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto
{
#if Application
    [AutoMap(typeof(TjlClientInfo))]
#endif
    public class ClientInfoOfPrintPreviewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        [StringLength(256)]
        [Required]
        public virtual string ClientName { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(128)]
        public virtual string HelpCode { get; set; }
    }
}