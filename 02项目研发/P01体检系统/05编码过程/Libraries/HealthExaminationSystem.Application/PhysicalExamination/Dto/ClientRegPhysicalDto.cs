using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.PhysicalExamination
{
    /// <summary>
    /// 单位预约登记
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlClientReg))]
#endif
    public class ClientRegPhysicalDto : EntityDto<Guid>
    {
        /// <summary>
        /// 单位信息
        /// </summary>
        [Required]
        public virtual ClientInfoPhysicalDto ClientInfo { get; set; }


        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime StartCheckDate { get; set; }

        /// <summary>
        /// 封帐状态 1已封帐 2未封帐
        /// </summary>
        public virtual int FZState { get; set; }


    }
}