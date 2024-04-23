using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlCustomerSummarize))]
#endif
    public class CreateCustomerSummarizeDto : EntityDto<Guid>
    {
        /// <summary>
        /// 预约id
        /// </summary>
        public virtual Guid CustomerRegID { get; set; }
       

        /// <summary>
        /// 审核人IDint
        /// </summary>
        public virtual long? ShEmployeeBMId { get; set; }
       

        /// <summary>
        /// 总检人ID
        /// </summary>
        public virtual long? EmployeeBMId { get; set; }
        

        /// <summary>
        /// 总检结论
        /// </summary>
        public virtual string CharacterSummary { get; set; }

        /// <summary>
        /// 诊断结论
        /// </summary>
        public virtual string DagnosisSummary { get; set; }
        /// <summary>
        /// 隐私总检结论
        /// </summary>
        [StringLength(3072)]
        public virtual string PrivacyCharacterSummary { get; set; }

        /// <summary>
        /// 总检建议
        /// </summary>       
        [StringLength(8192)]
        public virtual string Advice { get; set; }

        /// <summary>
        /// 保健建议
        /// </summary>
        public virtual string Jkzs { get; set; }

        /// <summary>
        /// 职业健康结论
        /// </summary>
        public virtual string ZYConclusion { get; set; }

        /// <summary>
        /// 危害因素 多个逗号拼接
        /// </summary>
        public virtual string ZYRiskName { get; set; }

        /// <summary>
        /// 岗位状态
        /// </summary>
        public virtual int? ZYPostState { get; set; }

        /// <summary>
        /// 疑似职业健康
        /// </summary>
        public virtual string ZYTabooIllness { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
        public virtual string ZYTreatmentAdvice { get; set; }

        /// <summary>
        /// 职业健康禁忌证
        /// </summary>
        public virtual string ZYOccupationalName { get; set; }

        /// <summary>
        /// 复查项目
        /// </summary>
        public virtual string ReviewContent { get; set; }

        /// <summary>
        /// 结论依据
        /// </summary>
        public virtual string ZYBasis { get; set; }

        /// <summary>
        /// 总检日期
        /// </summary>
        public virtual DateTime? ConclusionDate { get; set; }

        /// <summary>
        /// 总检审核日期
        /// </summary>
        public virtual DateTime? ExamineDate { get; set; }

        /// <summary>
        /// 体检日期
        /// </summary>
        public virtual DateTime? CheckDate { get; set; }

        /// <summary>
        /// 总检状态 1已初检2已审核总检
        /// </summary>
        public virtual int? CheckState { get; set; }

        /// <summary>
        /// 暂停状态 1已暂停2正常3已解冻
        /// </summary>
        public virtual int? SuspendState { get; set; }

        /// <summary>
        /// 退回状态 1已退回2已处理3已审核
        /// </summary>
        public virtual int? BackSate { get; set; }

        /// <summary>
        /// 是否合格
        /// </summary>
        public virtual string Qualified { get; set; }
        /// <summary>
        /// 职业健康总检结论
        /// </summary>
        [StringLength(3072)]
        public virtual string occCharacterSummary { get; set; }
        /// <summary>
        /// 职业健康总检建议
        /// </summary>
        [StringLength(8192)]
        public virtual string occAdvice { get; set; }
        /// <summary>
        /// 职业健康总检日期
        /// </summary>
        public virtual DateTime? occConclusionDate { get; set; }

        /// <summary>
        /// 职业健康总检审核日期
        /// </summary>
        public virtual DateTime? occExamineDate { get; set; }


        /// <summary>
        /// 总检状态 1已初检2已审核总检
        /// </summary>
        public virtual int? occCheckState { get; set; }
        /// <summary>
        /// 审核人标识
        /// </summary>   
        public virtual long? occShEmployeeBMId { get; set; }

    

        /// <summary>
        /// 总检人标识
        /// </summary>      
        public virtual long? occEmployeeBMId { get; set; }


    }
}
