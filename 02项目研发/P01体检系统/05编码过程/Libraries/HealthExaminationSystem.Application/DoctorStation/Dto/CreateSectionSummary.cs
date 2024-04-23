using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 用于生成小结时传入的参数，减少调用数据库次数
    /// </summary>
    public class CreateSectionSummary
    {
        /// <summary>
        /// 体检项目
        /// </summary>
        public List<ATjlCustomerItemGroupDto> lstaTjlCustomerItemGroupDtos { get; set; }
        /// <summary>
        /// 科室小结
        /// </summary>
        public CreateCustomerDepSummary outcreateCustomerDepSummary { get; set; }
    }
}
