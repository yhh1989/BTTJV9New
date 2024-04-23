using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
    /// <summary>
    /// 单位信息
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Core.Company.TjlClientInfo))]
#endif
    public class CompanyInformationDtoNo3 : EntityDto<Guid>
    {
        /// <summary>
        /// 单位编码
        /// </summary>
        [StringLength(24)]
        public virtual string ClientBM { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        [StringLength(256)]
        [Required]
        public virtual string ClientName { get; set; }

        /// <summary>
        /// 单位简称
        /// </summary>
        [StringLength(256)]
        public virtual string ClientAbbreviation { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(128)]
        public virtual string HelpCode { get; set; }

        /// <summary>
        /// 机构代码
        /// </summary>
        [StringLength(64)]
        public virtual string OrganizationCode { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [StringLength(16)]
        public virtual string Mobile { get; set; }

        /// <summary>
        /// 单位预约信息
        /// </summary>
        public virtual List<CompanyRegisterDtoNo1> ClientReg { get; set; }
    }
}