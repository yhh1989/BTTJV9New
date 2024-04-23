using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto
{
    public  class SearIdsDto
    {
        /// <summary>
        /// 科室Id
        /// </summary>
        public virtual List< Guid> GroupIds { get; set; }
    }
}
