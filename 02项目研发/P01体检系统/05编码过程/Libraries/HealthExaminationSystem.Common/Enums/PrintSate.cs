using System.ComponentModel;


namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 打印状态
    /// </summary>
    public enum PrintSate
    {
        [Description("未打印")]
        NotToPrint = 1,

        [Description("已打印")]
        Print = 2
      

    }
}