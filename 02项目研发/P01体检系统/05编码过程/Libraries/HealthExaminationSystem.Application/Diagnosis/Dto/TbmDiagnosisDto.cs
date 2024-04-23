using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Diagnosis.Dto
{
    /// <summary>
    /// 复合判断设置
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TbmDiagnosis))]
#endif
    public class TbmDiagnosisDto : EntityDto<Guid>
    {
        /// <summary>
        /// 复合判断明细集合
        /// </summary>
        public virtual List<TbmDiagnosisDataDto> DiagnosisDatals { get; set; }

        /// <summary>
        /// 复合判断名称
        /// </summary>
        [StringLength(128)]
        public virtual string RuleName { get; set; }

        /// <summary>
        /// 复合判断结论
        /// </summary>
        [StringLength(128)]
        public virtual string Conclusion { get; set; }
        /// <summary>
        /// 复合判断序号
        /// </summary>
        public virtual string OrderNum { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(256)]
        public virtual string Remarks { get; set; }

    }
}