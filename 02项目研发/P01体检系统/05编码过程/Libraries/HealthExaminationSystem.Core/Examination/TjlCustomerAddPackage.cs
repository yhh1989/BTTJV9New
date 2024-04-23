using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    /// <summary>
   /// 加项包记录表
   /// </summary>
  public  class TjlCustomerAddPackage : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 体检人预约标识
        /// </summary>
        [ForeignKey(nameof(CustomerReg))]
        public virtual Guid? CustomerRegId { get; set; }

        /// <summary>
        /// 体检人预约
        /// </summary>
        public virtual TjlCustomerReg CustomerReg { get; set; }

        /// <summary>
        /// 单位预约信息标识
        /// </summary>
        [ForeignKey(nameof(ClientReg))]
        public virtual Guid? ClientRegId { get; set; }

        /// <summary>
        /// 单位预约信息
        /// </summary>
        public virtual TjlClientReg ClientReg { get; set; }
        /// <summary>
        /// 加项包表ID
        /// </summary>
        [ForeignKey(nameof(ItemSuit))]
        public virtual Guid? ItemSuitID { get; set; }
        /// <summary>
        /// 加项包表
        /// </summary>
        public virtual TbmItemSuit ItemSuit { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }
    }
}
