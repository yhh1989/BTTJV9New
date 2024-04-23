using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Market.Dto
{
    /// <summary>
    /// 合同附件
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Core.Market.ContractAdjunct))]
#endif
    public class ContractAdjunctDto : EntityDto<Guid>
    {
        /// <summary>
        /// 文件的长度（以字节为单位）。
        /// </summary>
        public int ContentLength { get; set; }

        /// <summary>
        /// 合同标识
        /// </summary>
        public Guid ContractId { get; set; }

        /// <summary>
        /// 文件的 MIME 内容类型。
        /// </summary>
        [StringLength(maximumLength: 64)]
        public string ContentType { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        [Required]
        [StringLength(maximumLength: 128)]
        public string Name { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        [Required]
        [StringLength(maximumLength: 256)]
        public string FileName { get; set; }
    }
}