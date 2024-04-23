using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto
{
    /// <summary>
    /// 单位信息缓存数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMapFrom(typeof(Core.Company.TjlClientInfo))]
#endif
    public class ClientInfoCacheDto : EntityDto<Guid>
    {
        /// <summary>
        /// 父级单位标识
        /// </summary>
        public virtual Guid? ParentId { get; set; }

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
    }
}