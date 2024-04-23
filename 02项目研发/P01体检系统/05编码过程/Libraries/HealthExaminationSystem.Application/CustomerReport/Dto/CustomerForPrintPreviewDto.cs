using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto
{
#if Application
    [AutoMapFrom(typeof(TjlCustomer))]
#endif
    public class CustomerForPrintPreviewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 档案号
        /// </summary>
        [StringLength(16)]
        public virtual string ArchivesNum { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }
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
        /// 出生日期
        /// </summary>
        public virtual DateTime? Birthday { get; set; }
        /// <summary>
        /// 移动电话
        /// </summary>
        [StringLength(16)]
        public virtual string Mobile { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        [StringLength(24)]
        public virtual string IDCardNo { get; set; }

        /// <summary>
        /// 就诊卡
        /// </summary>
        [StringLength(32)]
        public virtual string VisitCard { get; set; }

        /// <summary>
        /// 医保卡
        /// </summary>
        [StringLength(32)]
        public virtual string MedicalCard { get; set; }

        /// <summary>
        /// 门诊号
        /// </summary>
        [StringLength(32)]
        public virtual string SectionNum { get; set; }
    }
}