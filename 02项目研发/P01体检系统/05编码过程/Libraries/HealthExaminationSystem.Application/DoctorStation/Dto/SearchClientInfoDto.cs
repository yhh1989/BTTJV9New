using Abp.Application.Services.Dto;
using System;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 科室患者信息查询
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlClientInfo))]
#endif
    public class SearchClientInfoDto : EntityDto<Guid>
    {
        /// <summary>
        /// 单位编码
        /// </summary>
        public virtual string ClientBM { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }

        /// <summary>
        /// 单位简称
        /// </summary>
        public virtual string ClientAbbreviation { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        public virtual string HelpCode { get; set; }

        /// <summary>
        /// 五笔编码
        /// </summary>
        public virtual string WubiCode { get; set; }
    }
}
