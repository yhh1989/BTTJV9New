using System.ComponentModel;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum ControlDate
    {
        [Description("上传")]
        Control = 1,

        [Description("不上传")]
        NotControl = 2
    }
}