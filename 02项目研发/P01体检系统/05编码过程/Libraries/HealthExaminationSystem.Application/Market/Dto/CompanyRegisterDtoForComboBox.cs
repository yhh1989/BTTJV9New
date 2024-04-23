using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Newtonsoft.Json;

namespace Sw.Hospital.HealthExaminationSystem.Application.Market.Dto
{
    /// <summary>
    /// 公司预约数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Core.Company.TjlClientReg))]
#endif
    public class CompanyRegisterDtoForComboBox : EntityDto<Guid>
    {
        /// <summary>
        /// 单位信息外键
        /// </summary>
        public virtual Guid ClientInfoId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime StartCheckDate { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [StringLength(2048)]
        public virtual string Remark { get; set; }

#if Proxy
        /// <summary>
        /// 显示内容
        /// </summary>
        [JsonIgnore]
        public string Display => $"{Remark}|{StartCheckDate:D}";
#endif
    }
}