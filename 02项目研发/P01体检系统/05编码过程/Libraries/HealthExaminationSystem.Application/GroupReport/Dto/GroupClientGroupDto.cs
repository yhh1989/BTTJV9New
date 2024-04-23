using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
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
    [AutoMap(typeof(TjlCustomerReg))]
#endif
    public class GroupClientGroupDto : EntityDto<Guid>
    {
        /// <summary>
        /// 预约id
        /// </summary>       
        public virtual Guid CustomerRegID { get; set; }
        /// <summary>
        /// 预约id
        /// </summary>
        public virtual GroupCusReg CustomerReg { get; set; }

        /// <summary>
        /// 体检人项目组合
        /// </summary>
        public virtual ICollection<GroupCusGroupDto> CustomerItemGroup { get; set; }




    }
}
