using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif 

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccGroupReport.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TjlCustomerReg))]
#endif
    public class OccCusRegDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检人
        /// </summary>
        public virtual OccCusInfoDto Customer { get; set; }
        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [StringLength(32)]
        public virtual string OrderNum { get; set; }        

        /// <summary>
        /// 套餐名称
        /// </summary>
        [StringLength(128)]
        public virtual string ItemSuitName { get; set; }

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
        /// 危害因素 逗号隔开
        /// </summary>
        [StringLength(800)]
        public virtual string RiskS { get; set; }

        /// <summary>
        /// 岗位类别
        /// </summary>
        [StringLength(16)]
        public virtual string PostState { get; set; }

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
        /// 总工龄单位
        /// </summary>
        [StringLength(2)]
        public virtual string WorkAgeUnit { get; set; }

        /// <summary>
        /// 接害工龄
        /// </summary>
        [StringLength(16)]
        public virtual string InjuryAge { get; set; }
        /// <summary>
        /// 接害工龄单位
        /// </summary>
        [StringLength(2)]
        public virtual string InjuryAgeUnit { get; set; }
        /// <summary>
        /// 危害因素
        /// </summary>
        public virtual List<OutOccHazardFactorDto> OccHazardFactors { get; set; }


        /// <summary>
        ///体检日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }

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
