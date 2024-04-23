using Abp.Application.Services.Dto;

using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlApplicationForm))]
#endif
    public class ApplicationDto : EntityDto<Guid>
    {
        /// <summary>
        /// 申请单号
        /// </summary>    
        public virtual string SQDH { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        public virtual decimal AllMoney { get; set; }

        /// <summary>
        /// 折后价
        /// </summary>
        public decimal ZHMoney { get; set; }

        /// <summary>
        /// 申请状态
        /// </summary>
        public int SQSTATUS { get; set; }
        /// <summary>
        /// 体检人预约集合
        /// </summary>
        public virtual CusRegSimpleDto CustomerReg { get; set; }

        /// <summary>
        /// 收费记录
        /// </summary>
        public virtual ReceiptInfoPerViewDto MReceiptInfo { get; set; }



    }
}
