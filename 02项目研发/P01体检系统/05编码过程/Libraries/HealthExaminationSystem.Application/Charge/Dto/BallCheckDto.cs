using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
#if !Proxy
using Abp.AutoMapper;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
    public class BallCheckDto : Pays
    {
        /// <summary>
        /// 单位预约Id
        /// </summary>
        public Guid? ClientRegId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 体检日期
        /// </summary>
        public DateTime? CheckDate { get; set; }
        /// <summary>
        /// 体检单位
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 分组
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int? Age { get; set; }
        /// <summary>
        /// 体检号
        /// </summary>
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 总计
        /// </summary>
        public decimal SumPrice { get; set; }

        /// <summary>
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }
#if Application
        [IgnoreMap]
#endif
        public virtual string FormatPhysical
        {
            get
            {
                var Physical = CheckSateHelper.PhysicalEStateFormatter(CheckSate);
                return Physical;
            }

        }

        /// <summary>
        /// 总检状态 1未总检2已分诊3已初检4已审核 
        /// </summary>
        public virtual int? SummSate { get; set; }
#if Application
        [IgnoreMap]
#endif
        public virtual string FormatSummSate
        {
            get
            {

                // var Physical = CheckSateHelper.PhysicalEStateFormatter(CheckSate);
                var Physical = SummSateHelper.SummSateFormatter(SummSate);
                return Physical;
            }

        }
        /// <summary>
        /// 预约组合
        /// </summary>
        public virtual List<ChargeCustmoerItemGroupDto> 检查明细 { get; set; }


        
    }

    /// <summary>
    /// 付费人员
    /// </summary>
    public class Pays
    {
        /// <summary>
        ///团付
        /// </summary>
        public decimal TuanPrice { get; set; }
        /// <summary>
        /// 个付
        /// </summary>
        public decimal GePrice { get; set; }
        /// <summary>
        /// 个人已支付
        /// </summary>
        public decimal GePayPrice { get; set; }

    }
}