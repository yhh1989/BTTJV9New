using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 上传状态 0未上传 1已上传 2可上传
    /// </summary>
    public enum UploadState
    {
        [Description("未上传")]
        No = 0,
        [Description("已上传")]
        Yse = 1,
        [Description("可上传")]
        OnYse = 2,
    }
}
