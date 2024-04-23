#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto
{   /// <summary>
    /// 查询客户预约信息Dto
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomerReg))]
#endif
   public  class GroupNoCheckCusDtocs
    {
        /// <summary>
        /// 体检人
        /// </summary>
        public virtual GroupCustomerDto Customer { get; set; }

        /// <summary>
        /// 体检人项目组合
        /// </summary>
        public virtual ICollection<GroupCusGroupDto> CustomerItemGroup { get; set; }
    }
}
