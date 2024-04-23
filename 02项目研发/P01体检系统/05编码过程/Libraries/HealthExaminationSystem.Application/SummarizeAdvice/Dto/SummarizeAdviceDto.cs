using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using HealthExaminationSystem.Enumerations.Helpers;
#if !Proxy
using AutoMapper;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TbmSummarizeAdvice))]
#endif
    public class SummarizeAdviceDto : SimpleSummarizeAdviceDto
    {
        /// <summary>
        /// 科室Id
        /// </summary>
        public virtual Guid DepartmentId { get; set; }
       

        /// <summary>
        /// 建议依据
        /// </summary>
        [StringLength(1024)]
        public virtual string Advicevalue { get; set; }

        /// <summary>
        /// 阳性状态 1阳性2正常
        /// </summary>
        public virtual int? SummState { get; set; }

        /// <summary>
        /// 疾病状态 1疾病2正常
        /// </summary>
        public virtual int? DiagnosisSate { get; set; }

        /// <summary>
        /// 危急值状态 1危急值2正常
        /// </summary>
        public virtual int? CrisisSate { get; set; }

        /// <summary>
        /// 总检建议
        /// </summary>
        [StringLength(1024)]
        public virtual string SummAdvice { get; set; }

        /// <summary>
        /// 专科建议
        /// </summary>
        [StringLength(1024)]
        public virtual string DepartmentAdvice { get; set; }

        /// <summary>
        /// 团体建议
        /// </summary>
        [StringLength(1024)]
        public virtual string ClientAdvice { get; set; }

        /// <summary>
        /// 饮食指导
        /// </summary>
        [StringLength(1024)]
        public virtual string DietGuide { get; set; }

        /// <summary>
        /// 运动指导
        /// </summary>
        [StringLength(1024)]
        public virtual string SportGuide { get; set; }

        /// <summary>
        /// 健康指导
        /// </summary>
        [StringLength(1024)]
        public virtual string Knowledge { get; set; }

        /// <summary>
        /// 健康建议
        /// </summary>
        [StringLength(1024)]
        public virtual string HealthcareAdvice { get; set; }

        /// <summary>
        /// 疾病介绍
        /// </summary>
        [StringLength(32)]
        public virtual string DiagnosisExpain { get; set; }

        /// <summary>
        /// 适用性别 1男2女3不限
        /// </summary>
        public virtual int? SexState { get; set; }

        /// <summary>
        /// 适用婚别 1结婚2不结婚3不限
        /// </summary>
        public virtual int? MarrySate { get; set; }

        /// <summary>
        /// 最小年龄
        /// </summary>
        public virtual int? MinAge { get; set; }

        /// <summary>
        /// 最大年龄
        /// </summary>
        public virtual int? MaxAge { get; set; }

        /// <summary>
        /// 疾病类别 字典
        /// </summary>
        public virtual int? DiagnosisAType { get; set; }

        /// <summary>
        /// 团报隐藏1隐藏2显示
        /// </summary>
        public virtual int? HideOnGroupReport { get; set; }

        //是否属于自定义诊断  1为自定义
        public int? IsTestInfo { get; set; }

        //建议汇总中的索引位置
        public int? IndexOfNum{ get; set; }

        #region 格式化
        /// <summary>
        /// 性别格式化
        /// </summary>
#if Application
        [IgnoreMap]
#endif
        [JsonIgnore]
        public virtual string FormatSex
        {
            get
            {
                if (SexState.HasValue)
                {
                    return SexHelper.GetSexModelsForItemInfo().Find(o => o.Id == SexState.Value).Display;
                }

                return null;
            }
        }

        /// <summary>
        /// 婚姻格式化
        /// </summary>
#if Application
        [IgnoreMap]
#endif
        [JsonIgnore]
        public virtual string FormatMarital
        {
            get
            {
                if (MarrySate.HasValue)
                {
                    return MarrySateHelper.GetMarrySateModelsForItemInfo().Find(o => o.Id == MarrySate).Display;
                }

                return null;
            }
        }
        #endregion
    }
}