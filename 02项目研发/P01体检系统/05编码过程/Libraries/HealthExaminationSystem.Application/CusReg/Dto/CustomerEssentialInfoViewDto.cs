using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
    /// <summary>
    /// 用户表
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomer))]
#endif
    public class CustomerEssentialInfoViewDto : EntityDto<Guid>
    {

    

        /// <summary>
        /// 档案号
        /// </summary>
        [StringLength(16)]
        public virtual string ArchivesNum { get; set; }

       
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
        /// 出生日期
        /// </summary>
        public virtual DateTime? Birthday { get; set; }

        /// <summary>
        /// 婚姻状况 1未婚2结婚
        /// </summary>
        public virtual int? MarriageStatus { get; set; }

    }
}
