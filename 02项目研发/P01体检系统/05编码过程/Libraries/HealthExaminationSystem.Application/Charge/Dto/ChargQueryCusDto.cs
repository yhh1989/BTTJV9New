#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{    /// <summary>
     /// 查询客户预约信息Dto
     /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomerReg))]
#endif
    public class ChargQueryCusDto : EntityDto<Guid>
    {     
        /// <summary>
        /// 预约ID
        /// </summary>  
        public virtual int? CustomerRegID { get; set; }

        /// <summary>
        /// 档案号
        /// </summary>
        [StringLength(32)]
        public virtual string TjCode { get; set; }

        /// <summary>
        /// 体检号
        /// </summary>
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? CustomerRegNum { get; set; }


        /// <summary>
        /// 体检场所-1院内-2外出
        /// </summary>

        public virtual int? ExamPlace { get; set; }

        /// <summary>
        /// 预约日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }
        /// <summary>
        /// 登记状态 1未登记 2已登记
        /// </summary>
        public virtual int? RegisterState { get; set; }

        /// <summary>
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }    

        /// <summary>
        /// 导检状态 1未导检2正在导检3已暂停4已结束
        /// </summary>
        public virtual int? NavigationSate { get; set; }

        /// <summary>
        /// 导诊开始时间
        /// </summary>
        public virtual DateTime? NavigationStartTime { get; set; }

        /// <summary>
        /// 导诊结束时间
        /// </summary>
        public virtual DateTime? NavigationEndTime { get; set; }     

        /// <summary>
        /// 收费状态 1未收费2已收费3欠费
        /// </summary>
        public virtual int? CostState { get; set; } 

  

      
    }
}
