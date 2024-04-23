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
    /// 问卷试题选项数据传输对象
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlQuestiontem))]
#endif
    public class QuestiontemDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检人预约标识
        /// </summary>
        public virtual Guid? CustomerRegId { get; set; }

        /// <summary>
        /// 体检人预约标识
        /// </summary>
        public virtual Guid? CusQuestionId { get; set; }

        /// <summary>
        /// 答卷题目详情
        /// </summary>
        public virtual Guid? QuestionBomId { get; set; }

        /// <summary>
        /// 选项
        /// </summary>
        [StringLength(32)]
        public virtual string itemName { get; set; }

        /// <summary>
        /// 给的分数
        /// </summary>   
        public virtual int? grade { get; set; }

        /// <summary>
        /// 是否已选中
        /// </summary>    
        public virtual int isSelected { get; set; }
    }
}