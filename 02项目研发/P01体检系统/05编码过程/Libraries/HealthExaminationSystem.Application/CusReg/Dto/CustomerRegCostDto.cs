using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
    /// <summary>
    /// 查询客户预约信息Dto
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomerReg))]
#endif
    public class CustomerRegCostDto : EntityDto<Guid>
    {
        /// <summary>
        /// 收费状态 1未收费2已收费3欠费
        /// </summary>
        public virtual int? CostState { get; set; }
        /// <summary>
        /// 体检人项目组合
        /// </summary>
        public virtual List<CustomerItemGroupDto> CustomerItemGroup { get; set; }
    }
}
