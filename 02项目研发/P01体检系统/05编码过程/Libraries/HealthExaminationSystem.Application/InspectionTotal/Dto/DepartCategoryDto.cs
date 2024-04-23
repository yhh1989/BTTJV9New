#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmDepartment))]
#endif
    public class DepartCategoryDto
    {
        /// <summary>
        /// 部门类别 检查 检验 功能 放射 彩超 其他 耗材
        /// </summary>
        [StringLength(64)]
        public virtual string Category { get; set; }

    }
}
