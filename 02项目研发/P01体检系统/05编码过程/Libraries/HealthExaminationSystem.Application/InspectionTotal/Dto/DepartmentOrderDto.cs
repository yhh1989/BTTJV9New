using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TbmDepartment))]
#endif
    public class DepartmentOrderDto
    {
        /// <summary>
        /// 科室名称
        /// </summary>       
        public virtual string Name { get; set; }
        /// <summary>
        /// 科室序号
        /// </summary>
        public virtual int? OrderNum { get; set; }
    }
}
