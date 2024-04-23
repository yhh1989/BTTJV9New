using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
   public  class TjlApplicationForm : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 申请单号
        /// </summary>
        [StringLength(20)]
        public virtual string SQDH { get; set; }
        /// <summary>
        /// 发票号
        /// </summary>
        [StringLength(20)]
        public virtual string FPH { get; set; }
        /// <summary>
        /// 发票抬头
        /// </summary> 
        [StringLength(500)]
        public virtual string FPName { get; set; }
        /// <summary>
        /// 备注
        /// </summary> 
        [StringLength(500)]
        public virtual string Remark { get; set; }
        /// <summary>
        /// 病人收费号
        /// </summary>
        [StringLength(20)]
        public virtual string BRSFH { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        public virtual decimal AllMoney { get; set; }

        /// <summary>
        /// 折后价
        /// </summary>
        public decimal FYZK { get; set; }

        /// <summary>
        /// 回款金额
        /// </summary>
        public decimal? REFYZK { get; set; }
        /// <summary>
        /// 最后回款日期
        /// </summary>
        public DateTime? RETIME { get; set; }

        /// <summary>
        /// 申请状态 1"未收费" 2"已收费 "3"已作废","4部分收费"
        /// </summary>
        public int SQSTATUS { get; set; }

        /// <summary>
        /// 申请状态
        /// </summary>
        public int HCZT { get; set; }


        /// <summary>
        /// 预约ID
        /// </summary>
        [ForeignKey("CustomerReg")]
        public virtual Guid? CustomerRegId { get; set; }

        /// <summary>
        /// 体检人预约集合
        /// </summary>
        public virtual TjlCustomerReg CustomerReg { get; set; }

        /// <summary>
        /// 单位预约信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey("ClientReg")]
        public virtual Guid? ClientRegId { get; set; }

        /// <summary>
        /// 单位预约信息
        /// </summary>
        public virtual TjlClientReg ClientReg { get; set; }

        /// <summary>
        /// 收费ID
        /// </summary>
        [ForeignKey("MReceiptInfo")]
        public virtual Guid? MReceiptInfoId { get; set; }

        /// <summary>
        /// 收费记录
        /// </summary>
        public virtual TjlMReceiptInfo MReceiptInfo { get; set; }

        /// <summary>
        /// 收费明细记录
        /// </summary>
        [InverseProperty(nameof(TjlMReceiptInfo.ApplicationForm))]
        public virtual ICollection<TjlMReceiptInfo> MReceiptInfoes { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
