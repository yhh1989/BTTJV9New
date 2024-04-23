using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    public class InterfaceBack
    {
        /// <summary>
        /// 返回信息
        /// </summary>
        public StringBuilder StrBui { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public StringBuilder JKStrBui { get; set; }
        /// <summary>
        /// 返回提取的科室id
        /// </summary>
        public List<Guid> IdList { get; set; }
    }
}
