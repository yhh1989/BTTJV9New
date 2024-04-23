using System;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterItemPicture.Dto
{
	/// <summary>
	/// 体检人预约项目结果图像数据传输对象
	/// </summary>
	public class CustomerRegisterItemPictureDto : EntityDto<Guid>
	{
		/// <summary>
		/// 预约ID
		/// </summary>
		public virtual Guid? TjlCustomerRegID { get; set; }

		/// <summary>
		/// 项目ID
		/// </summary>
		public virtual Guid? ItemBMID { get; set; }

		/// <summary>
		/// 项目组合ID
		/// </summary>
		public virtual Guid? CustomerItemGroupID { get; set; }

		/// <summary>
		/// 图片ID
		/// </summary>
		public virtual Guid? PictureBM { get; set; }
	}
}