using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
    public class CusNameInput : EntityDto<Guid>
    {
      
        [StringLength(32)]
        public string Theme { get; set; }
        /// <summary>
        /// 体检类别 1健康体检2职业健康体检3健康证体检4公务员体检5学生体检6驾驶证体检7婚检
        /// </summary>
        public int PhysicalType { get; set; }
        //是否隐私项目 1是2不是
        public int? PrivacyState { get; set; }
        /// <summary>
        /// 体检号
        /// </summary>
        public string CusRegBM { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? Custime { get; set; }
    }
}