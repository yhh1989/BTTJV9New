using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum ExaminationCategory
    {
        [Description("健康体检")]
        Health = 1,
        [Description("从业体检")]
        Occupational = 2,
        [Description("复查")]
        ReExamine = 3,
        [Description("通知复查")]
        NoticeReExamine = 4,
        [Description("散检")]
        IrregularExamine = 5,
        [Description("疑似职业健康")]
        SuspectedOccupationalDisease = 6,
        [Description("职业禁忌证")]
        OccupationalContraindication = 7
    }
}
