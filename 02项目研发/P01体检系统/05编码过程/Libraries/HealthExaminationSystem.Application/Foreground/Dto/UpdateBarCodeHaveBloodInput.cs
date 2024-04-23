using System;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Foreground.Dto
{
    /// <summary>
    /// 更新条码抽血输入数据传输对象
    /// </summary>
    public class UpdateBarCodeHaveBloodInput : EntityDto<Guid>
    {
        /// <summary>
        /// 已抽血
        /// </summary>
        public bool HaveBlood { get; set; }
        /// <summary>
        /// 2送检3接收
        /// </summary>
        public int? type { get; set; }
    }
}