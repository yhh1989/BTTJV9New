using System;
using System.ComponentModel.DataAnnotations;

namespace Sw.Hospital.HealthExaminationSystem.Application.Questionnaire.Dto
{
    /// <summary>
    /// 问卷检索输入
    /// </summary>
    public class QuestionnaireSearchInput
    {
        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public string checkNo { get; set; }

        /// <summary>
        /// 线上预约体检订单号
        /// </summary>
        [StringLength(32)]
        public string tempPersonCheckOrderno { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public virtual DateTime? lastTimeStart { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public virtual DateTime? lastTimeEnd { get; set; }
    }
}