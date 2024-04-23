using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.HealthCard
{
    /// <summary>
    /// 健康卡
    /// </summary>
    public class TbmCard : AuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 卡号
        /// </summary>       
        [StringLength(64)]
        public virtual string CardNo { get; set; }
        /// <summary>
        /// 卡类别标识
        /// </summary>
        [ForeignKey(nameof(CardType))]
        public virtual Guid? CardTypeId { get; set; }
        /// <summary>
        /// 卡类别
        /// </summary>
        public virtual TbmCardType CardType { get; set; }

        /// <summary>
        /// 面值取卡类别面值
        /// </summary>
        public virtual decimal FaceValue { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public virtual DateTime? EndTime { get; set; }

        /// <summary>
        /// 开卡人标识
        /// </summary>
        [ForeignKey("CreateCardUser")]
        public virtual long? CreateCardUserId { get; set; }

        /// <summary>
        /// 开卡人
        /// </summary>
        public virtual User CreateCardUser { get; set; }

        /// <summary>
        /// 销售员标识
        /// </summary>
        [ForeignKey("SellCardUser")]
        public virtual long? SellCardUserId { get; set; }

        /// <summary>
        /// 销售员
        /// </summary>
        public virtual User SellCardUser { get; set; }
        /// <summary>
        /// 反款方式1先返款2后返款
        /// </summary>
        public virtual int PayType { get; set; }

        /// <summary>
        /// 先返款结算金额
        /// </summary>
        public virtual decimal? SellMoney{ get; set; }
        /// <summary>
        /// 是否启用枚举InvoiceState
        /// </summary>
        public virtual int? Available { get; set; }
        /// <summary>
        /// 使用状态0未使用1已使用
        /// </summary>
        public virtual int? HasUse { get; set; }
        /// <summary>
        /// 使用者Id
        /// </summary>
        [ForeignKey("CustomerReg")]
        public virtual Guid? CustomerRegId { get; set; }

        /// <summary>
        /// 使用者
        /// </summary>
        public virtual TjlCustomerReg CustomerReg { get; set; }

        /// <summary>
        /// 单位预约ID
        /// </summary>
        [ForeignKey("ClientReg")]
        public virtual Guid? ClientRegId { get; set; }
        /// <summary>
        /// 单位预约ID
        /// </summary>
        public virtual TjlClientReg ClientReg { get; set; }

        /// <summary>
        /// 单位分组信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey("ClientTeamInfo")]
        public virtual Guid? ClientTeamInfoId { get; set; }

        /// <summary>
        /// 单位分组
        /// </summary>
        public virtual TjlClientTeamInfo ClientTeamInfo { get; set; }

        /// <summary>
        /// 卡类别
        /// </summary>       
        [StringLength(64)]
        public virtual string CardCategory{ get; set; }


        /// <summary>
        /// 使用时间
        /// </summary>
        public virtual DateTime? UseTime { get; set; }
        /// <summary>
        /// 租户标识
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual decimal DiscountRate { get; set; }
    }
}
