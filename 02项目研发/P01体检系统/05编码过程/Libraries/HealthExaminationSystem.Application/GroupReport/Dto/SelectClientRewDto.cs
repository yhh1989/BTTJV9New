using System;
using Abp.Application.Services.Dto;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto
{
    /// <summary>
    /// 单位预约登记查询
    /// </summary>
#if Application
    [AutoMap(typeof(TjlClientReg))]
#endif
    public class SelectClientRewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime StartCheckDate { get; set; }

        /// <summary>
        /// 单位信息
        /// </summary>
        public virtual SelectGroupClientInfoDto ClientInfo { get; set; }

        /// <summary>
        /// 预约日期起
        /// </summary>
        public virtual DateTime? ClientRegStart { get; set; }

        /// <summary>
        /// 预约日期止
        /// </summary>
        public virtual DateTime? ClientRegEnd { get; set; }
    }
}