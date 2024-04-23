using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister.Dto
{
    /// <summary>
    /// 体检人项目组合数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Core.Examination.TjlCustomerItemGroup))]
#endif
    public class CustomerRegisterItemGroupDtoNo4 : EntityDto<Guid>
    {
        /// <summary>
        /// 体检人预约主键
        /// </summary>
        public Guid? CustomerRegBMId { get; set; }

        /// <summary>
        /// 项目组合名称
        /// </summary>
        [StringLength(32)]
        public string ItemGroupName { get; set; }

        /// <summary>
        /// 项目检查状态 1未检查2已检查3部分检查4放弃5待查
        /// <para>参考：Sw.Hospital.HealthExaminationSystem.Common.Enums.ItemInspectState</para>
        /// </summary>
        public int? CheckState { get; set; }
    }
}