using System.ComponentModel;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 人员登记中 是否早餐 1不吃2吃3已吃
    /// </summary>
    public enum BreakfastType
    {
        /// <summary>
        /// 不吃
        /// </summary>
        [Description("不吃")]
        NotEat =1,
        /// <summary>
        /// 吃
        /// </summary>
        [Description("吃")]
        Eat =2,
        /// <summary>
        /// 已吃
        /// </summary>
        [Description("已吃")]
        Eaten =3,
    }
}
