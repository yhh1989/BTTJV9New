using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
    /// <summary>
    /// 查询科室患者dto
    /// </summary>
    public class QueryDepartCustomerRegDto
    {
        /// <summary>
        /// 科室ids
        /// </summary>
        public List<Guid> DepartMentIds { get; set; }
        /// <summary>
        ///单位ids
        /// </summary>
        public List<Guid> ClientRegIds { get; set; }

        /// <summary>
        /// 组合Id
        /// </summary>
        public Guid? ItemGroupId { get; set; }
        /// <summary>
        /// 组合状态
        /// </summary>
        public int? GroupState { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 介绍人
        /// </summary>
        public string Introducer { get; set; }
    }
}
