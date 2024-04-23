namespace Sw.Hospital.HealthExaminationSystem.Common.Models
{
    /// <summary>
    /// 枚举类模型
    /// </summary>
    public class EnumModel
    {
        /// <summary>
        /// 标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string Display { get; set; }
    }

    /// <summary>
    /// 枚举类模型
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    public class EnumModel<TEnum> : EnumModel where TEnum : struct
    {
        /// <summary>
        /// 枚举
        /// </summary>
        public TEnum EnumFiled { get; set; }
    }
}