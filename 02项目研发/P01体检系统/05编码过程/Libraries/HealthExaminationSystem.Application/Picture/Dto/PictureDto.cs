using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Picture.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(Core.AppSystem.Picture), typeof(Core.AppSystem.UserPicture))]
#endif
    public class PictureDto : FullAuditedEntityDto<Guid>
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
    }
}