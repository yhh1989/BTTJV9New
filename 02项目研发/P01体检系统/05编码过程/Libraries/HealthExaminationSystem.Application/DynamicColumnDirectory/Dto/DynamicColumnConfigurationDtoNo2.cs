using System.ComponentModel.DataAnnotations;

namespace Sw.Hospital.HealthExaminationSystem.Application.DynamicColumnDirectory.Dto
{
    /// <summary>
    /// 动态列配置数据传输对象
    /// </summary>
    public class DynamicColumnConfigurationDtoNo2
    {
        /// <summary>
        /// 表格视图标识
        /// </summary>
        [StringLength(64)]
        [Required]
        public string GridViewId { get; set; }
    }
}