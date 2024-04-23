using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlClientInfo))]
#endif
    public class ClientInfosNameDto : EntityDto<Guid>
    {
        // <summary>
        /// 单位编码
        /// </summary>
        public virtual string ClientBM { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }
        /// <summary>
        /// 助记码
        /// </summary>
        public virtual string HelpCode { get; set; }

        public virtual DateTime? CreationTime { get; set; }

        /// <summary>
        /// 企业负责人
        /// </summary>
        [StringLength(32)]
        public virtual string LinkMan { get; set; }

    }
}