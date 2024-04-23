using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto
{
    /// <summary>
    /// 设置项目加减状态
    /// </summary>
    public class SetItemGroupAddMinusDto
    {
        /// <summary>
        /// 登记id
        /// </summary>
        public Guid RegID { get; set; }
        /// <summary>
        /// 项目列表
        /// </summary>
        public List<Guid> ItemGroupIds { get; set; }
        /// <summary>
        /// 删除项目列表
        /// </summary>
        public List<Guid> DelItemGroupIds { get; set; }
        /// <summary>
        /// 设置为加项/减项
        /// </summary>
        public int SetAddMinusState { get; set; }
    }
}
