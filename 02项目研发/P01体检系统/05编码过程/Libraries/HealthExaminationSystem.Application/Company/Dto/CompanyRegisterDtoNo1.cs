using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
    /// <summary>
    /// 单位预约数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Core.Company.TjlClientReg))]
#endif
    public class CompanyRegisterDtoNo1 : EntityDto<Guid>
    {
        /// <summary>
        /// 单位信息外键
        /// </summary>
        public virtual Guid ClientInfoId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime StartCheckDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime EndCheckDate { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [StringLength(128)]
        public virtual string Remark { get; set; }
    }
}