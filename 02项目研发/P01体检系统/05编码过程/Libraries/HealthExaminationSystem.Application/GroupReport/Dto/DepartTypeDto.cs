using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto
{
    /// <summary>
    /// 查询客户预约信息Dto
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TbmDepartment))]
#endif
    public class DepartTypeDto 
    {
        /// <summary>
        /// 部门类别 检查 检验 功能 放射 彩超 其他 耗材
        /// </summary>  
        public virtual string Category { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>  
        public virtual string Name { get; set; }
    }
}
