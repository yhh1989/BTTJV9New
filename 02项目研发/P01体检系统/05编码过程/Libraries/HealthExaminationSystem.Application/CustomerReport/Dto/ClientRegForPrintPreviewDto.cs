using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Newtonsoft.Json;
#if Application
using AutoMapper;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlClientReg))]
#endif
    public class ClientRegForPrintPreviewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 单位信息
        /// </summary>
        public virtual ClientInfoOfPrintPreviewDto ClientInfo { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime StartCheckDate { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [StringLength(128)]
        public virtual string Remark { get; set; }

#if Proxy
        [JsonIgnore]     
        public string DisplayMember => $"{Remark}|{StartCheckDate:D}";
#endif
    }
}