using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
    /// <summary>
    /// 体检人预约登记信息表
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomerReg))]
#endif
    public class EditCustomerRegStateDto : EntityDto<Guid>
    {
        /// <summary>
        /// 初审医生外键
        /// </summary>
        public virtual long? CSEmployeeId { get; set; }

        /// <summary>
        /// 复审医生外键
        /// </summary>
        public virtual long? FSEmployeeId { get; set; }
        /// <summary>
        /// 总检状态 1未总检2已分诊3已初检4已审核
        /// </summary>
        public virtual int? SummSate { get; set; }
        ///// <summary>
        ///// 职业总检状态 1未总检2已分诊3已初检4已审核
        ///// </summary>
        //public virtual int? OccSummSate { get; set; }
        /// <summary>
        /// 总检锁定 1锁定2未锁定
        /// </summary>
        public virtual int? SummLocked { get; set; }
    }
}
