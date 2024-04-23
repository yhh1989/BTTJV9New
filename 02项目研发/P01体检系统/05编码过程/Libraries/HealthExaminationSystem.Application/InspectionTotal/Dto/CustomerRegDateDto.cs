#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
    /// <summary>
    /// 体检人预约登记信息表
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomerReg))]
#endif
  public   class CustomerRegDateDto 
    {

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }
    }
}
