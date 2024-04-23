using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.DoctorStation
{
    /// <summary>
    /// 医生站和科室人员查询页面实时交互
    /// </summary>
    public class DepartmentCustomerEventArgs : EventArgs
    {
        /// <summary>
        /// 体检号
        /// </summary>
        public string CustomerNumber { get; set; }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="customerNumber"></param>
        public DepartmentCustomerEventArgs(string customerNumber)
        {
            CustomerNumber = customerNumber;
        }
    }

}
