using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
   public  class TjlCusCabit : FullAuditedEntity<Guid>, IMustHaveTenant
    {

        /// <summary>
        /// 柜子相关
        /// </summary>
        [ForeignKey("CustomerRegBM")]
        public virtual Guid? CustomerRegId { get; set; }
        /// <summary>
        /// 预约id
        /// </summary>
        public virtual    TjlCustomerReg  CustomerRegBM { get; set; }


        /// <summary>
        /// 柜子相关
        /// </summary>
        [ForeignKey("ClientReg")]
        public virtual Guid? ClientRegId { get; set; }

        /// <summary>
        /// 单位预约信息
        /// </summary>
        public virtual TjlClientReg ClientReg { get; set; }
        /// <summary>
        /// 报个状态1个报2团报
        /// </summary>

        public int? ReportState { get; set; }
        /// <summary>
        /// 柜子号
        /// </summary>
        public string CabitName { get; set; }
        /// <summary>
        /// 取出状态1存入2取出
        /// </summary>
        public int? GetState { get; set; }

        /// <summary>
        /// 交接时间
        /// </summary>
        public virtual DateTime? HandoverTime { get; set; }

        /// <summary>
        /// 存入人
        /// </summary>
        [ForeignKey("Handover")]
        public virtual long? HandoverId { get; set; }

        /// <summary>
        /// 存入人
        /// </summary>     
        public virtual User Handover { get; set; }

        /// <summary>
        /// 发单时间
        /// </summary>
        public virtual DateTime? SendTime { get; set; }



        /// <summary>
        /// 发单人Id
        /// </summary>
        [ForeignKey("Sender")]
        public virtual long? SenderId { get; set; }
        /// <summary>
        /// 发单人
        /// </summary>      
        public virtual User Sender { get; set; }

        /// <summary>
        /// 领取人
        /// </summary>
        [MaxLength(32)]
        public virtual string Receiver { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(256)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 租户标识
        /// </summary>
        public int TenantId { get; set; }
    }
}
