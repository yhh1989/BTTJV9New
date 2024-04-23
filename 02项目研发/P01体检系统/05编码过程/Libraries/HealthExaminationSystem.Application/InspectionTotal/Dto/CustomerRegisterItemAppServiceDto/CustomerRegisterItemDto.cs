using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto.CustomerRegisterItemAppServiceDto
{
    /// <summary>
    /// 体检人预约项目体检数据传输对象
    /// </summary>
    
#if !Proxy 
    [Abp.AutoMapper.AutoMap(typeof(TjlCustomerRegItem))]
#endif  
    public class CustomerRegisterItemDto : EntityDto<Guid>
	{
        /// <summary>
        /// 重要异常说明
        /// </summary>
        public virtual string CrisiContent { get; set; }
        /// <summary>
        /// 危急值提示
        /// </summary>
        public virtual string CrisiChar { get; set; }
        /// <summary>
        /// 危急值等级
        /// </summary>
        public virtual int? CrisisLever { get; set; }
        /// <summary>
        /// 体检人检查项目组合id
        /// </summary>
        public virtual CustomerItemGroupZYBDto CustomerItemGroupBM { get; set; }

        /// <summary>
        /// 体检人检查项目组合id
        /// </summary>
        public virtual Guid? CustomerItemGroupBMid { get; set; }

		/// <summary>
		/// 预约外键
		/// </summary>
		public virtual Guid CustomerRegId { get; set; }

		/// <summary>
		/// 科室外键
		/// </summary>
		public virtual Guid DepartmentId { get; set; }

		/// <summary>
		/// 组合外键
		/// </summary>
		public virtual Guid? ItemGroupBMId { get; set; }
        public virtual SimpItemInfoDto ItemBM { get; set; }
        /// <summary>
        /// 项目Id外键
        /// </summary>
        public virtual Guid ItemId { get; set; }

		/// <summary>
		/// 项目状态 1未检查2已检查3部分检查4放弃5待查
		/// </summary>
		public virtual int? ProcessState { get; set; }

		/// <summary>
		/// 暂停状态 1正常2暂停3解冻
		/// </summary>
		public virtual int? SuspendState { get; set; }

		/// <summary>
		/// 总检退回状态 1未退回2已退回3已处理4已审核
		/// </summary>
		public virtual int? SummBackSate { get; set; }

		/// <summary>
		/// 检查人ID外键
		/// </summary>
		public virtual long? InspectEmployeeBMId { get; set; }

		/// <summary>
		/// 审核人ID外键
		/// </summary>
		public virtual long? CheckEmployeeBMId { get; set; }

		/// <summary>
		/// 总审核人标识
		/// </summary>
		public virtual long? TotalEmployeeBMId { get; set; }

		/// <summary>
		/// 项目结果，中文说明 数值型也存入
		/// </summary>
		[StringLength(3072)]
		public virtual string ItemResultChar { get; set; }

		/// <summary>
		/// 项目标示 H偏高 HH超高L偏低 LL 超低M正常P异常
		/// </summary>
		[StringLength(16)]
		public virtual string Symbol { get; set; }

		/// <summary>
		/// 阳性状态 1正常2阳性
		/// </summary>
		public virtual int? PositiveSate { get; set; }

		/// <summary>
		/// 项目小结
		/// </summary>
		[StringLength(3072)]
		public virtual string ItemSum { get; set; }
		/// <summary>
		/// 项目诊断
		/// </summary>
		[StringLength(3072)]
		public virtual string ItemDiagnosis { get; set; }

		/// <summary>
		/// 参考值
		/// </summary>
		[StringLength(256)]
		public virtual string Stand { get; set; }

        /// <summary>
        /// 危急值状态2正常1危急值
        /// </summary>
        public virtual int? CrisisSate { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [StringLength(64)]
        public virtual string ItemName { get; set; }
        public virtual  DepartmentOrderDto DepartmentBM { get; set; }
        public virtual ItemGroupOrderDto ItemGroupBM { get; set; }

        /// <summary>
        /// 报告编码 
        /// </summary>
        [StringLength(640)]
        public virtual string ReportBM { get; set; }



        /// <summary>
        /// 复合判断状态 1属于复合判断2不属于
        /// </summary>
        public virtual int? DiagnSate { get; set; }

        /// <summary>
        /// 项目单位
        /// </summary>
        [StringLength(16)]
        public virtual string Unit { get; set; }
    }
}