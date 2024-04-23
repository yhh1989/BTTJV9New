using System;

namespace HealthExaminationSystem.Enumerations.Models
{
    /// <summary>
    /// 生日年龄性别模型
    /// </summary>
    public class BirthdayAgeSex
    {
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }
        public int Age { get; set; }
        public Sex Sex { get; set; }
    }
}