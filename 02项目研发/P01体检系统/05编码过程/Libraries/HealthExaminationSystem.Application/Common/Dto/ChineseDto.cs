using System.ComponentModel.DataAnnotations;

namespace Sw.Hospital.HealthExaminationSystem.Application.Common.Dto
{
    /// <summary>
    /// 汉字数据传输对象
    /// </summary>
    public class ChineseDto
    {
        /// <summary>
        /// 汉字
        /// </summary>
        [Required]
        public string Hans { get; set; }

        /// <summary>
        /// 简拼
        /// </summary>
        public string Brief { get; set; }
    }
}