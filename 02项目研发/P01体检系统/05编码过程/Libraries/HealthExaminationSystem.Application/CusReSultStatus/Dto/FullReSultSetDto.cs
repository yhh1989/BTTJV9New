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

    /// <summary>
    /// 查询客户预约信息Dto
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TbmReSultSet))]
#endif
    public class FullReSultSetDto
    {
        /// <summary>
        ///  统计
        /// </summary>
        public AddResultSetDto ReSultSets { get; set; }
        /// <summary>
        /// 显示项目名称
        /// </summary>     
        public virtual List<Guid> Departs { get; set; }
        /// <summary>
        /// 显示组合名称
        /// </summary>     
        public virtual List<Guid> Groups { get; set; }
        /// <summary>
        /// 显示项目名称
        /// </summary>     
        public virtual List<Guid> Items { get; set; }
    }
}
