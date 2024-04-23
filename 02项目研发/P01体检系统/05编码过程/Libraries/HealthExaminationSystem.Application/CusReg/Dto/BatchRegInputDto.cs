using System;
using System.Collections.Generic;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
    /// <summary>
    /// 批量预约 取消预约输入Dto
    /// </summary>
    public class BatchRegInputDto
    {
        /// <summary>
        /// 是预约还是取消
        /// </summary>
        public bool IsReg { get; set; }
        /// <summary>
        /// 人员登记ID
        /// </summary>
        public List<Guid> RegIds { get; set; }
    }
}
