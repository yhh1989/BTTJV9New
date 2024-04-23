using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Abp.AutoMapper;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto
{
    /// <summary>
    /// 查询客户预约信息Dto
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomerReg))]
#endif
    public class CustomerRegForCrossTableDto : EntityDto<Guid>
    {
        /// <summary>
        /// 交表确认 1未交表2已交表
        /// </summary>
        public virtual int? SendToConfirm { get; set; }

        /// <summary>
        /// 导引单照片
        /// </summary>
        public virtual Guid? GuidancePhotoId { get; set; }

        /// <summary>
        /// 交表人Id 1未交表2已交表
        /// </summary>     
        public virtual long? SendUserId { get; set; }

        public int? BloodState { get; set; }
    }
}
