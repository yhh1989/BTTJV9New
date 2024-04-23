using System.ComponentModel;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 科室类别
    /// </summary>
    public enum DepartmentType
    {
        /// <summary>
        /// 检查科
        /// </summary>
        [Description("检查")]
        InspectionDepartment=1,
        /// <summary>
        /// 检验科
        /// </summary>
        [Description("检验")]
        ClinicalLab=2,
        /// <summary>
        /// 功能科
        /// </summary>
        [Description("功能")]
        Functional=3,
        /// <summary>
        /// 放射科
        /// </summary>
        [Description("放射")]
        RadiologyDepartment=4,
        /// <summary>
        /// 彩超科
        /// </summary>
        [Description("彩超")]
        ColorUltrasound=5,
        /// <summary>
        /// 其他科
        /// </summary>
        [Description("其他")]
        OtherDepartment=6
    }
}