using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto.CustomerRegisterDepartmentSummaryAppServiceDto
{
	/// <summary>
	/// 体检人预约科室小结汇总
	/// </summary>
	public class CustomerRegisterDepartmentSummaryDto : EntityDto<Guid>
	{
		/// <summary>
		/// 体检人预约信息标识
		/// </summary> 
		public virtual Guid? CustomerRegId { get; set; }

		/// <summary>
		/// 科室信息标识
		/// </summary>
		public virtual Guid? DepartmentId { get; set; }

		/// <summary>
		/// 科室小结
		/// </summary>
		[StringLength(3072)]
		public virtual string CharacterSummary { get; set; }

		/// <summary>
		/// 隐私科室小结
		/// </summary>
		[StringLength(3072)]
		public virtual string PrivacyCharacterSummary { get; set; }

        /// <summary>
        /// 原生科室小结 
        /// </summary>
        [StringLength(3072)]
        public virtual string OriginalDiag { get; set; }

        /// <summary>
        /// 科室诊断小结
        /// </summary>
        [StringLength(3072)]
		public virtual string DagnosisSummary { get; set; }

		/// <summary>
		/// 体检日期
		/// </summary>
		public virtual DateTime? CheckDate { get; set; }

		/// <summary>
		/// 系统生成小结
		/// </summary>
		[StringLength(3072)]
		public virtual string SystemCharacter { get; set; }

		/// <summary>
		///审核人外键
		/// </summary>
		public virtual long? ExamineEmployeeId { get; set; }



	}
}