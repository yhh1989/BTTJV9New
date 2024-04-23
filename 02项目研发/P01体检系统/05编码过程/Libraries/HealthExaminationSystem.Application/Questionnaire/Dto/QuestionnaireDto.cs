using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Questionnaire.Dto
{
    /// <summary>
    /// 问卷数据传输对象
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCusQuestion))]
#endif
    public class QuestionnaireDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检人预约标识
        /// </summary>
        public virtual Guid? CustomerRegId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(32)]
        public virtual string personName { get; set; }

        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string checkNo { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [StringLength(32)]
        public virtual string mobile { get; set; }

        /// <summary>
        /// 线上预约体检订单号
        /// </summary>
        [StringLength(32)]
        public virtual string tempPersonCheckOrderno { get; set; }

        /// <summary>
        /// 团队体检订单号
        /// </summary>
        [StringLength(32)]
        public virtual string teamCheckOrderno { get; set; }

        /// <summary>
        /// 问卷标题
        /// </summary>
        [StringLength(32)]
        public virtual string questionName { get; set; }

        /// <summary>
        /// 回答评定分数
        /// </summary>
        public virtual int? answerGrade { get; set; }

        /// <summary>
        /// 是否已生成评估报告1:是;0:否
        /// </summary>
        public virtual int? hasReport { get; set; }

        /// <summary>
        /// 评估报告文件地址
        /// </summary>
        [StringLength(32)]
        public virtual string reportUrl { get; set; }

        /// <summary>
        /// 回答评定结果
        /// </summary>
        [StringLength(32)]
        public virtual string evaluateResult { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        [StringLength(32)]
        public virtual string lastTime { get; set; }

        /// <summary>
        /// 1自定义问卷2评估问卷
        /// </summary>
        public virtual int? Type { get; set; }
    }
}