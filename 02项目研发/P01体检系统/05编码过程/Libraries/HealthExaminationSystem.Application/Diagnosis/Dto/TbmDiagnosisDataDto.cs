using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Diagnosis.Dto
{
    /// <summary>
    /// 复合判断设置明细
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TbmDiagnosisData))]
#endif
    public class TbmDiagnosisDataDto : EntityDto<Guid>
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public virtual ItemInfoDiagnosisDto ItemInfo { get; set; }

        /// <summary>
        /// 项目标准值:用于文本型项目
        /// </summary>
        [StringLength(128)]
        public virtual string ItemStandard { get; set; }

        /// <summary>
        /// 仪器Id:用于文本型项目
        /// </summary>
        [StringLength(128)]
        public virtual string InstrumentId { get; set; }

        /// <summary>
        /// 项目下限:用于数值型项目
        /// </summary>
        public virtual decimal? minValue { get; set; }

        /// <summary>
        /// 项目上限:用于数值型项目
        /// </summary>
        public virtual decimal? maxValue { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual string Sex { get; set; }

        /// <summary>
        /// 最小年龄
        /// </summary>
        public virtual int? MinAge { get; set; }

        /// <summary>
        /// 最大年龄
        /// </summary>
        public virtual int? MaxAge { get; set; }
        /// <summary>
        /// 项目类型:1说明型(阴阳性)2数值型
        /// </summary>
        public virtual int? ItemType { get; set; }

    }
}