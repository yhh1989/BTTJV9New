using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto;
using System.ComponentModel.DataAnnotations;
#if !Proxy
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Abp.AutoMapper;
#endif
namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
    /// <summary>
    /// 查询客户预约信息Dto
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlCustomerReg))]
#endif
    public class GroupCusReg : EntityDto<Guid>
    {
       
        /// <summary>
        /// 体检人
        /// </summary>
        public virtual GroupCustomerDto Customer { get; set; }
        
        /// <summary>
        /// 体检号
        /// </summary>
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 预约日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }
        /// <summary>
        /// 登记状态 1未登记 2已登记
        /// </summary>
        public virtual int? RegisterState { get; set; }
        /// <summary>
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }

        /// <summary>
        /// 总检状态 1未总检2已分诊3已初检4已审核
        /// </summary>
        public virtual int? SummSate { get; set; }

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }
        /// <summary>
        /// 工种
        /// </summary>
        [StringLength(16)]
        public virtual string TypeWork { get; set; }

        /// <summary>
        /// 车间
        /// </summary>
        [StringLength(16)]
        public virtual string WorkName { get; set; }

        /// <summary>
        /// 总工龄
        /// </summary>
        [StringLength(16)]
        public virtual string TotalWorkAge { get; set; }

        /// <summary>
        /// 接害工龄
        /// </summary>
        [StringLength(16)]
        public virtual string InjuryAge { get; set; }

        /// <summary>
        /// 复查状态 1正常2复查3回访
        /// </summary>
        public virtual int? ReviewSate { get; set; }

        /// <summary>
        /// 复查原预约ID
        /// </summary>   
        public virtual Guid? ReviewRegID { get; set; }
    }
}
