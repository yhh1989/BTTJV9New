using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto
{
    public class EditItemGroupStateInput
    {
        /// <summary>
        /// 项目组合ID
        /// </summary>
        public List<Guid> Ids { get; set; }

        /// <summary>
        /// 检查状态
        /// </summary>
        public int CheckState { get; set; }
        /// <summary>
        /// 放弃待查记录
        /// </summary>
        public CusGiveUpDto Record { get; set; }
    }
}
