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
    public class QuestiontemReportDto : EntityDto<Guid>
    {
      

        /// <summary>
        /// 题目
        /// </summary>
        [StringLength(32)]
        public virtual string bomItemName { get; set; }

        /// <summary>
        /// 题目类型1:填空;2:单选;3: 复选
        /// </summary>     
        public virtual int? bomItemType { get; set; }     

        /// <summary>
        /// 填空题答案
        /// </summary>
        [StringLength(32)]
        public virtual string answerContent { get; set; }

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
        public virtual string  isSelected { get; set; }

        /// <summary>
        /// 序号
        /// </summary>   
        public virtual int? OrderNum { get; set; }

        /// <summary>
        /// 选项序号
        /// </summary>   
        public virtual int? ItemOrderNum { get; set; }

        /// <summary>
        /// 是否已选中
        /// </summary>    
        public virtual string Sig { get; set; }


        /// <summary>
        /// 题目
        /// </summary>
        [StringLength(320)]
        public virtual string Title { get; set; }
      
    }
}