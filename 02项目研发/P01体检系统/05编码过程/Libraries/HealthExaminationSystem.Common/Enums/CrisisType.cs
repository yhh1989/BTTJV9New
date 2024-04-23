using System.ComponentModel;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 危急值产生类型
    /// </summary>
    public enum CrisisType
    {
        /// <summary>
        /// 接口返回
        /// </summary>
        [Description("接口返回")]
        InterfaceBack =0,
        /// <summary>
        /// 系统生成
        /// </summary>
        [Description("系统生成")]
        SystemCreate =1,
        /// <summary>
        /// 人工设置
        /// </summary>
        [Description("人工设置")]
        ManualSetting =2
    }
}
