﻿using Sw.Hospital.HealthExaminationSystem.SoftFace.SDKModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.SoftFace.Entity
{
    /// <summary>
    /// 视频检测缓存实体类
    /// </summary>
    public class FaceTrackUnit
    {
        public MRECT Rect { get; set; }
        public IntPtr Feature { get; set; }
        public string message = string.Empty;
    }
}
