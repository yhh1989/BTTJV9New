using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Abp.AutoMapper;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Scheduling.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TjlClientInfo))]
#endif
    public class SimpleClientInfoDto : EntityDto<Guid>
    {
        /// <summary>
        /// 单位编码
        /// </summary>
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
        /// 五笔编码
        /// </summary>
        [StringLength(128)]
        public virtual string WubiCode { get; set; }

        /// <summary>
        /// 机构代码
        /// </summary>
        [StringLength(64)]
        public virtual string OrganizationCode { get; set; }

    }
}
