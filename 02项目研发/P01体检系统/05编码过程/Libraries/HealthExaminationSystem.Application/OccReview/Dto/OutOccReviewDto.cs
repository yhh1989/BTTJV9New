#if !Proxy
using Abp.AutoMapper;
using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif
using System;
using System.ComponentModel.DataAnnotations;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccReview.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TjlOccCustomerSum))]
#endif
    public class OutOccReviewDto
    {
        /// <summary>
        /// 有害因素名称
        /// </summary>
        public string HazardFactorName { get; set; }
        /// <summary>
        /// 有害因素类别
        /// </summary>
        public string HazardFactorType { get; set; }
        /// <summary>
        /// 体检号
        /// </summary>
        public string CustomerBM { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int? Sex { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int? Age { get; set; }

        /// <summary>
        /// 总工龄
        /// </summary>
        [StringLength(16)]
        public virtual string TotalWorkAge { get; set; }
        /// <summary>
        /// 总工龄单位
        /// </summary>
        [StringLength(2)]
        public virtual string WorkAgeUnit { get; set; }

        /// <summary>
        /// 接害工龄
        /// </summary>
        [StringLength(16)]
        public virtual string InjuryAge { get; set; }
        /// <summary>
        /// 接害工龄单位
        /// </summary>
        [StringLength(2)]
        public virtual string InjuryAgeUnit { get; set; }

        /// <summary>
        /// 工种
        /// </summary>
        [StringLength(32)]
        public virtual string TypeWork { get; set; }

        /// <summary>
        /// 总检结论
        /// </summary>
        [StringLength(3072)]
        public virtual string CharacterSummary { get; set; }
        /// <summary>
        /// 职业健康结论
        /// </summary>
        public string Conclusion { get; set; }
        /// <summary>
        /// 处理意见
        /// </summary>
        public string Opinions { get; set; }
        /// <summary>
        /// 复查项目
        /// </summary>
        public string ReviewContent { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartCheckDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndCheckDate { get; set; }
        /// <summary>
        /// 年度
        /// </summary>
        public string YearTime { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 单位预约ID
        /// </summary>
        public Guid? ClientRegId { get; set; }
    }
}
