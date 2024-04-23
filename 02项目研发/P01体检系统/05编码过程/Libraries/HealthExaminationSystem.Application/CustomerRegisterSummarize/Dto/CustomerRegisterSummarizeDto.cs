using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif 
namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterSummarize.Dto
{
    /// <summary>
    /// 体检人预约总检汇总数据传输对象
    /// </summary>
    /// <summary>
    /// 查询客户预约信息Dto 
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlCustomerSummarize))]
#endif 
    public class CustomerRegisterSummarizeDto : EntityDto<Guid>
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
		/// 总检人标识
		/// </summary>

		public virtual long? EmployeeBMId { get; set; }

		/// <summary>
		/// 总检结论
		/// </summary>
		[StringLength(3072)]
		public virtual string CharacterSummary { get; set; }

		/// <summary>
		/// 隐私总检结论
		/// </summary>
		[StringLength(3072)]
		public virtual string PrivacyCharacterSummary { get; set; }

		/// <summary>
		/// 诊断结论
		/// </summary>
		[StringLength(3072)]
		public virtual string DagnosisSummary { get; set; }

		/// <summary>
		/// 总检建议
		/// </summary>
		[StringLength(8192)]
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
		public virtual DateTime? ReviewContentDate { get; set; }

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
		/// 主检日期
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
		[StringLength(32)]
		public virtual string Qualified { get; set; }


        /// <summary>
        /// 建议
        /// </summary>
        [StringLength(1024)]
        public virtual string Opinion { get; set; }

    }
}