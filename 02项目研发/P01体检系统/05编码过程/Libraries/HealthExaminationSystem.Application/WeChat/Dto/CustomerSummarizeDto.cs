#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using System.ComponentModel.DataAnnotations.Schema;
#endif
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
#if !Proxy
    [AutoMapTo(typeof(TjlCustomerSummarize))]
#endif
    //总检建议
    public class CustomerSummarizeDto
    {
        

        /// <summary>
        /// 预约标识
        /// </summary>       
        public virtual Guid CustomerRegID { get; set; }
     

        /// <summary>
        /// 审核人标识
        /// </summary>
   
        public virtual long? ShEmployeeBMId { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public virtual UserViewDto ShEmployeeBM { get; set; }

        /// <summary>
        /// 总检人标识
        /// </summary>

        public virtual long? EmployeeBMId { get; set; }

        /// <summary>
        /// 总检人
        /// </summary>
        public virtual UserViewDto EmployeeBM { get; set; }

        /// <summary>
        /// 总检结论
        /// </summary>
        [StringLength(3072)]
        public virtual string CharacterSummary { get; set; }

        /// <summary>
        /// 诊断结论
        /// </summary>
        [StringLength(3072)]
        public virtual string DagnosisSummary { get; set; }

        /// <summary>
        /// 总检建议
        /// </summary>
        [StringLength(3072)]
        public virtual string Advice { get; set; }

        /// <summary>
        /// 保健建议
        /// </summary>
        [StringLength(3072)]
        public virtual string Jkzs { get; set; }

        /// <summary>
        /// 职业健康结论
        /// </summary>
        [StringLength(3072)]
        public virtual string ZYConclusion { get; set; }

        /// <summary>
        /// 危害因素 多个逗号拼接
        /// </summary>
        [StringLength(1024)]
        public virtual string ZYRiskName { get; set; }

        /// <summary>
        /// 岗位状态
        /// </summary>
        public virtual int? ZYPostState { get; set; }

        /// <summary>
        /// 疑似职业健康
        /// </summary>
        [StringLength(256)]
        public virtual string ZYTabooIllness { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
        [StringLength(256)]
        public virtual string ZYTreatmentAdvice { get; set; }

        /// <summary>
        /// 职业健康禁忌证
        /// </summary>
        [StringLength(64)]
        public virtual string ZYOccupationalName { get; set; }

        /// <summary>
        /// 复查周期
        /// </summary>
        [StringLength(64)]
        public virtual string ReviewContentCycle { get; set; }

        /// <summary>
        /// 复查日期
        /// </summary>

        public virtual DateTime ReviewContentDate { get; set; }

        /// <summary>
        /// 复查项目
        /// </summary>
        [StringLength(1024)]
        public virtual string ReviewContent { get; set; }

        /// <summary>
        /// 结论依据
        /// </summary>
        [StringLength(1024)]
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

    }
}
