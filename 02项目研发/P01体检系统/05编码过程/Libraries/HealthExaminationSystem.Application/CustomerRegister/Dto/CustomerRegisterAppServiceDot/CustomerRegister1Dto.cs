using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister.Dto.CustomerRegisterAppServiceDot
{
	/// <summary>
	/// 体检人预约记录
	/// </summary>
	public class CustomerRegister1Dto : EntityDto<Guid>
	{
		/// <summary>
		/// 体检人外键
		/// </summary>
		public virtual Guid CustomerId { get; set; }

		/// <summary>
		/// 姓名
		/// </summary>
		[StringLength(32)]
		public virtual string Name { get; set; }

		/// <summary>
		/// 性别
		/// </summary>
		public virtual int? Sex { get; set; }

		/// <summary>
		/// 单位信息标识
		/// </summary>
		public virtual Guid? ClientInfoId { get; set; }

		/// <summary>
		/// 单位名称
		/// </summary>
		[StringLength(256)]
		[Required]
		public virtual string ClientName { get; set; }

		/// <summary>
		/// 体检号
		/// </summary>
		[StringLength(32)]
		public virtual string CustomerBM { get; set; }

		/// <summary>
		/// 登记日期 第一次登记日期
		/// </summary>
		public virtual DateTime? LoginDate { get; set; }

		/// <summary>
		/// 套餐信息外键
		/// </summary>
		public virtual Guid? ItemSuitBMId { get; set; }

		/// <summary>
		/// 套餐名称
		/// </summary>
		[StringLength(128)]
		public virtual string ItemSuitName { get; set; }

		/// <summary>
		/// 总检状态 1未总检2已分诊3已初检4已审核
		/// </summary>
		public virtual int? SummSate { get; set; }

		/// <summary>
		/// 体检类别 1健康体检2职业健康体检3健康证体检4公务员体检5学生体检6驾驶证体检7婚检
		/// </summary>
		public virtual int? PhysicalType { get; set; }

		/// <summary>
		/// 年龄
		/// </summary>
		public virtual int? RegAge { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>     
        [StringLength(128)]
        public virtual string FPNo { get; set; }

        /// <summary>
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }


        /// <summary>
        /// 总检锁定 1锁定2未锁定
        /// </summary>
        public virtual int? SummLocked { get; set; }

        /// <summary>
        /// 总检锁定用户ID
        /// </summary>     
        public virtual long? SummLockEmployeeBMId { get; set; }

        /// <summary>
        /// 初审医生外键
        /// </summary>   
        public virtual long? CSEmployeeId { get; set; }

        /// <summary>
        /// 复审医生外键
        /// </summary>
        public virtual long? FSEmployeeId { get; set; }

    }
}