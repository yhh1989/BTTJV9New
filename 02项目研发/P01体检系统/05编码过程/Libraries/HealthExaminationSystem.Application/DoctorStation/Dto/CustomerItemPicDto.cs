using System;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 科室小结
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomerItemPic))]
#endif
    public class CustomerItemPicDto : EntityDto<Guid>
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