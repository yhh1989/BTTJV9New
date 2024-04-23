using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 总检建议设置
    /// 查询科室总监建议设置
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TbmSummarizeAdvice))]
#endif
    public class CTbmSummarizeAdviceDto : EntityDto<Guid>
    {
        /// <summary>
        /// 编码
        /// </summary>
        [StringLength(32)]
        public virtual string Uid { get; set; }

        /// <summary>
        /// 输入码
        /// </summary>
        [StringLength(1024)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 科室ID
        /// </summary>
        public virtual ATbmDepartmentDto DepartmentBM { get; set; }

        /// <summary>
        /// 建议依据
        /// </summary>
        [StringLength(64)]
        public virtual string Advicevalue { get; set; }

        /// <summary>
        /// 建议名称
        /// </summary>
        [StringLength(1024)]
        public virtual string AdviceName { get; set; }

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
        /// 健康知道
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
        public virtual Guid? DiagnosisAType { get; set; }

        /// <summary>
        /// 团报隐藏1隐藏2显示
        /// </summary>
        public virtual int? HideOnGroupReport { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? OrderNum { get; set; }
        
    }
}