using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.IDNumber.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TdbIdNumber))]
#endif
    public class IDNumberDto : EntityDto<Guid>
    {
        /// <summary>
        /// Id名称
        /// </summary>
        [StringLength(64)]
        public virtual string IDName { get; set; }

        /// <summary>
        /// 类别id
        /// </summary>
        [StringLength(64)]
        public virtual string IDType { get; set; }

        /// <summary>
        /// 前缀
        /// </summary>
        [StringLength(64)]
        public virtual string prefix { get; set; }

        /// <summary>
        /// 日期前缀 yyyyMMdd-4
        /// </summary>
        [StringLength(64)]
        public virtual string Dateprefix { get; set; }

        /// <summary>
        /// Id值
        /// </summary>
        public virtual int IDValue { get; set; }
    }
}