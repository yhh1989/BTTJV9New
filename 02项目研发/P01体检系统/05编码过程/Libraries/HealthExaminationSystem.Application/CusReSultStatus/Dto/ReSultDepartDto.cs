#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReSultStatus.Dto
{

#if !Proxy
    [AutoMap(typeof(TbmReSultDepart))]
#endif
    public class ReSultDepartDto
    {


        /// <summary>
        /// 科室标识
        /// </summary>       
        public virtual Guid DepartmentId { get; set; }

        public string DepartName { get; set; }
    }
}
