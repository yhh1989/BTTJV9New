using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlCustomer))]
#endif
    public class ChargeCusDto : EntityDto<Guid>
    {
        /// <summary>
        /// 档案号
        /// </summary>
        [StringLength(16)]
        public virtual string ArchivesNum { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [StringLength(16)]
        public virtual string WorkNumber { get; set; }

        /// <summary>
        /// 会员卡
        /// </summary>
        [StringLength(16)]
        public virtual string CardNumber { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(64)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }

        /// <summary>
        /// 岁
        /// </summary>
        [StringLength(2)]
        public virtual string AgeUnit { get; set; }
    }
}