using Abp.Application.Services.Dto;
using System;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Crisis;
using Abp.AutoMapper;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto
{
    /// <summary>
    /// 危急值记录实体dto
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCrisisSet))]
#endif
    public class TjlCrisisSetDto : EntityDto<Guid>
    {
        /// <summary>
        /// 项目结果表id 外键
        /// </summary>
        public virtual Guid TjlCustomerRegItemId { get; set; }

        /// <summary>
        /// 设置说明
        /// </summary>
        public virtual string SetNotice { get; set; }
        /// <summary>
        /// 通知消息状态
        /// </summary>
        public virtual int? MsgState { get; set; }
        /// <summary>
        /// 危急值类型 接口回传0 系统生成1 人为设置2
        /// </summary>
        public virtual int CrisisType { get; set; }
        /// <summary>
        /// 回访状态 否0 是1
        /// </summary>
        public virtual int CallBackState { get; set; }
    }
}
