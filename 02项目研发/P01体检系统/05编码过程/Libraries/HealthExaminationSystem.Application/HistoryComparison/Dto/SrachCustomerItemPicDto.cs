using Abp.Application.Services.Dto;
using System;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison.Dto
{
    /// <summary>
    /// 体检人图片
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomerItemPic))]
#endif
    public class SrachCustomerItemPicDto : EntityDto<Guid>
    {
        /// <summary>
        /// 预约ID
        /// </summary>
        public virtual Guid? TjlCustomerRegID { get; set; }
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
