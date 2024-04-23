using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Core.Payment
{
    /// <summary>
    /// 个人应收已收
    /// </summary>
    public class TjlMcusPayMoney : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 主键
        /// </summary>
        [ForeignKey("CustomerReg")]
        public override Guid Id { get; set; }

        /// <summary>
        /// 体检人外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey("Customer")]
        public virtual Guid Customer_Id { get; set; }
        /// <summary>
        /// 体检人
        /// </summary>
        public virtual TjlCustomer Customer { get; set; }

        /// <summary>
        /// 个人预约ID
        /// </summary>
        public virtual TjlCustomerReg CustomerReg { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [ForeignKey("ClientInfo")]
        public virtual Guid? ClientInfo_Id { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public virtual TjlClientInfo ClientInfo { get; set; }

        /// <summary>
        /// 单位预约
        /// </summary>
        [ForeignKey("ClientReg")]
        public virtual Guid? ClientReg_Id { get; set; }


        /// <summary>
        /// 单位预约ID
        /// </summary>
        public virtual TjlClientReg ClientReg { get; set; }

        /// <summary>
        /// 单位预约
        /// </summary>
        [ForeignKey("ClientTeamInfo")]
        public virtual Guid? ClientTeamInfo_Id { get; set; }
        /// <summary>
        /// 单位分组
        /// </summary>
        public virtual TjlClientTeamInfo ClientTeamInfo { get; set; }

        /// <summary>
        /// 个人应收金额
        /// </summary>
        public virtual decimal PersonalMoney { get; set; }
        /// <summary>
        /// 个人应收加项
        /// </summary>
        public virtual decimal PersonalAddMoney { get; set; }

        /// <summary>
        /// 个人应收减项
        /// </summary>
        public virtual decimal PersonalMinusMoney { get; set; }

        /// <summary>
        /// 个人调项加金额
        /// </summary>
        public virtual decimal PersonalAdjustAddMoney { get; set; }

        /// <summary>
        /// 个人调项减金额
        /// </summary>
        public virtual decimal PersonalAdjustMinusMoney { get; set; }


        /// <summary>
        /// 个人已收
        /// </summary>
        public virtual decimal PersonalPayMoney { get; set; }

        /// <summary>
        /// 团体应收金额
        /// </summary>
        public virtual decimal ClientMoney { get; set; }

        /// <summary>
        /// 团体应收加项
        /// </summary>
        public virtual decimal ClientAddMoney { get; set; }

        /// <summary>
        /// 团体应收减项
        /// </summary>
        public virtual decimal ClientMinusMoney { get; set; }

        /// <summary>
        /// 团体调项加金额
        /// </summary>
        public virtual decimal ClientAdjustAddMoney { get; set; }

        /// <summary>
        /// 团体调项减金额
        /// </summary>
        public virtual decimal ClientAdjustMinusMoney { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}