using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlCustomerAddPackage))]
#endif
    public class CustomerAddPackageDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检人预约标识
        /// </summary>       
        public virtual Guid? CustomerRegId { get; set; }

        /// <summary>
        /// 单位预约信息标识
        /// </summary>  
        public virtual Guid? ClientRegId { get; set; }
     
        /// <summary>
        /// 加项包表ID
        /// </summary>
        public virtual Guid? ItemSuitID { get; set; }
        
    }
}
