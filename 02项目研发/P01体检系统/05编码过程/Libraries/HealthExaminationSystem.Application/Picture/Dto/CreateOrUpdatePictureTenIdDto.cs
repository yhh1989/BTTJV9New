using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
namespace Sw.Hospital.HealthExaminationSystem.Application.Picture.Dto
{
    [AutoMapTo(typeof(Core.AppSystem.Picture))]
    [AutoMapFrom(typeof(PictureDto))]
  public   class CreateOrUpdatePictureTenIdDto : EntityDto<Guid>
    {

        /// <summary>
        /// 图片在程序中的相对路径
        /// </summary>
        public virtual string RelativePath { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public virtual string Thumbnail { get; set; }

        /// <summary>
        /// 归属于
        /// </summary>
        [StringLength(32)]
        public virtual string Belong { get; set; }

        public virtual int TenantId { get; set; }
    }
}
