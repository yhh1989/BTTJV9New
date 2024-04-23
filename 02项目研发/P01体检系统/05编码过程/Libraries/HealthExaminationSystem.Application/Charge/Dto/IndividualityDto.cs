using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlMReceiptInfo))]
#endif 
    public class IndividualityDto
    {
        /// <summary>
        /// 预约表
        /// </summary>
        public virtual CustomerRegDto CustomerReg { get; set; }        
        /// 体检类型:1单位2体检人
        /// </summary>
        public virtual int TJType { get; set; }
        /// <summary>
        /// 应收:优惠
        /// </summary>
        public virtual decimal Shouldmoney { get; set; }

        /// <summary>
        /// 实收
        /// </summary>
        public virtual decimal Actualmoney { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual decimal Discount { get; set; }
        /// <summary>
        /// 收费员
        /// </summary>
        public virtual string UserName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 收费时间
        /// </summary>
        public DateTime ChargeDate { get; set; }
    }
}  
