using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
    /// <summary>
    /// 为下拉框提供公司预约分组信息数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Core.Company.TjlClientTeamInfo))]
#endif
    public class CompanyRegisterTeamInformationDtoForComboBox : EntityDto<Guid>
    {
        /// <summary>
        /// 单位预约信息外键
        /// </summary>
        public virtual Guid ClientRegId { get; set; }

        /// <summary>
        /// 分组ID
        /// </summary>
        public virtual int TeamBM { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        [StringLength(256)]
        public virtual string TeamName { get; set; }
    }
}