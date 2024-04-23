using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto
{
    public class SearchSummarizeAdvice : EntityDto<Guid>
    {
        /// <summary>
        ///     建议名称
        /// </summary>
        [StringLength(32)]
        public virtual string QueryText { get; set; }
        
        /// <summary>
        /// 科室Id
        /// </summary>
        public virtual Guid DepartmentId { get; set; }

        /// <summary>
        ///     建议依据
        /// </summary>
        [StringLength(1024)]
        public virtual string Advicevalue { get; set; }
        
        /// <summary>
        ///     疾病状态 1疾病2正常
        /// </summary>
        public virtual int? DiagnosisSate { get; set; }

        /// <summary>
        ///     危急值状态 1危急值2正常
        /// </summary>
        public virtual int? CrisisSate { get; set; }
        
        /// <summary>
        ///     疾病介绍
        /// </summary>
        [StringLength(32)]
        public virtual string DiagnosisExpain { get; set; }

        /// <summary>
        ///     适用性别 1男2女3不限
        /// </summary>
        public virtual int? SexState { get; set; }

        /// <summary>
        ///     适用婚别 1结婚2不结婚3不限
        /// </summary>
        public virtual int? MarrySate { get; set; }

        /// <summary>
        ///     最小年龄
        /// </summary>
        public virtual int? MinAge { get; set; }

        /// <summary>
        ///     最大年龄
        /// </summary>
        public virtual int? MaxAge { get; set; }

        /// <summary>
        ///     疾病类别 字典
        /// </summary>
        public virtual int? DiagnosisAType { get; set; }

        /// <summary>
        ///     团报隐藏1隐藏2显示
        /// </summary>
        public virtual int? HideOnGroupReport { get; set; }

    }
}
