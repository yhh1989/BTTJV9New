using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Market.Dto
{
    /// <summary>
    /// 合同类别数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(targetTypes: typeof(Core.Market.ContractCategory))]
#endif
    public class ContractCategoryDto : EntityDto<Guid>
    {
        /// <summary>
        /// 启用的
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 合同列表名称
        /// </summary>
        [Required]
        [StringLength(maximumLength: 64)]
        public string Name { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(maximumLength: 32)]
        public string MnemonicCode { get; set; }
    }
}