using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
    /// <summary>
    /// 按科室统计团体结账单
    /// </summary>
    public class StatisticsTTJZDDepartmentDto
    {
        /// <summary>
        /// 部门类别
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 科室
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 总人数
        /// </summary>
        public int TotalNumber { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalFee { get; set; }
        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal YingShouJinE { get; set; }
        /// <summary>
        /// 实收金额
        /// </summary>
        public decimal ShiShouJinE { get; set; }
        /// <summary>
        /// 已检总金额
        /// </summary>
        public decimal YiJianZongJinE { get; set; }

    }
}
