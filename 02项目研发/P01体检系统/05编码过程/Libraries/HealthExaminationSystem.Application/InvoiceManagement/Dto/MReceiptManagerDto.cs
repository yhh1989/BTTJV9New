using System;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.InvoiceManagement.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmMReceiptManager))]
#endif
    public class MReceiptManagerDto : EntityDto<Guid>
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public virtual long? UserId { get; set; }
        /// <summary>
        /// 领票id
        /// </summary>
        public virtual UserViewDto User { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? SerialNumber { get; set; }

        /// <summary>
        /// 开始号
        /// </summary>
        public virtual int? StratCard { get; set; }

        /// <summary>
        /// 结束号
        /// </summary>
        public virtual int? EndCard { get; set; }

        /// <summary>
        /// 当前号
        /// </summary>
        public virtual int? NowCard { get; set; }

        /// <summary>
        /// 类别:1收据2发票
        /// </summary>
        public virtual int? Type { get; set; }

        /// <summary>
        /// 状态:1启用2停止
        /// </summary>
        public virtual int? State { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remarks { get; set; }
    }
}