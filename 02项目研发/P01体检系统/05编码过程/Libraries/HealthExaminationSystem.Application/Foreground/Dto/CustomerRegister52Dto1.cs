using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Foreground.Dto
{
    /// <summary>
    /// 体检人预约数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Core.Examination.TjlCustomerReg))]
#endif
    public class CustomerRegister52Dto1 : EntityDto<Guid>
    {
        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 体检人外键
        /// </summary>
        public virtual Guid CustomerId { get; set; }

        /// <summary>
        /// 体检人
        /// </summary>
        public virtual Customer52Dto Customer { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public virtual CompanyInformation52Dto ClientInfo { get; set; }

        /// <summary>
        /// 单位信息标识
        /// </summary>
        public virtual Guid? ClientInfoId { get; set; }
    }
}