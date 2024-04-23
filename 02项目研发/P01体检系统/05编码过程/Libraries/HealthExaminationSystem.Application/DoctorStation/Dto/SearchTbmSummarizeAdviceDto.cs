using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 科室建议设置
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TbmSummarizeAdvice))]
#endif
    public class SearchTbmSummarizeAdviceDto : EntityDto<Guid>
    {
        /// <summary>
        /// 科室标识
        /// </summary>
        public virtual Guid DepartmentId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [StringLength(32)]
        public virtual string Uid { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(1024)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 建议依据
        /// </summary>
        [StringLength(1024)]
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
        /// 专科建议
        /// </summary>
        [StringLength(1024)]
        public virtual string DepartmentAdvice { get; set; }

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
        /// 序号
        /// </summary>
        public virtual int? OrderNum { get; set; }
    }
}
