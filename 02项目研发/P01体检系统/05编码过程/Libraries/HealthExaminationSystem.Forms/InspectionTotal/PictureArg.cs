using System;
using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterItemPicture.Dto;

namespace Sw.Hospital.HealthExaminationSystem.InspectionTotal
{
	public class PictureArg
	{
		public List<CustomerRegisterItemPictureDto> Pictures { get; set; }

		public Guid CurrentItemId { get; set; }
	}
}