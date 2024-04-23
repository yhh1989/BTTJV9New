using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
    /// <summary>
    /// 查询个人登记信息
    /// </summary>
    public class SearchCustomerRegDto
    {
        /// <summary>
        /// 个人身份证号
        /// </summary>
        public string IDCardNo { get; set; }
    }
}
