using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Abp.AutoMapper;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
    /// <summary>
    /// 查询客户预约信息Dto 
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomerReg))]
#endif
    public class CustomerRegEssentialInfoViewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 单位基本信息
        /// </summary>
        public virtual  QueryClientInfoDto ClientInfo { get; set; }
        /// <summary>
        /// 体检人
        /// </summary>
        public virtual CustomerEssentialInfoViewDto Customer { get; set; }
        /// <summary>
        /// 档案号
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }
        /// <summary>
        /// 是否盲检 1正常2盲检
        /// </summary>
        public virtual int? BlindSate { get; set; }

        /// <summary>
        /// 登记序号
        /// </summary>
        public virtual int? CustomerRegNum { get; set; }
        /// <summary>
        /// 单位序号
        /// </summary>
        public virtual int? ClientRegNum { get; set; }

        /// <summary>
        /// 原体检人
        /// </summary>
        [StringLength(64)]
        public virtual string PrimaryName { get; set; }
    }
}
