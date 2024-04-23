using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
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
   /// <summary>
   /// 体检人加项包
   /// </summary>
   public  class TjlCustomerAddPackageItem : AuditedEntity<Guid>, IMustHaveTenant
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
        /// <summary>
        /// 个人加项包表
        /// </summary>
        [ForeignKey(nameof(TjlCusAddpackages))]
        public virtual Guid? TjlCusAddpackagesID { get; set; }
        /// <summary>
        /// 个人加项包表
        /// </summary>
        public virtual TjlCustomerAddPackage TjlCusAddpackages { get; set; }
        /// <summary>
        /// 组合名称
        /// </summary>
        [ForeignKey(nameof(ItemGroup))]
        public virtual Guid? ItemGroupID { get; set; }

        /// <summary>
        /// 组合名称
        /// </summary>

        public virtual TbmItemGroup ItemGroup { get; set; }
        
        /// <summary>
        /// 项目组合名称
        /// </summary>      
        [StringLength(64)]
        public virtual string ItemGroupName { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual decimal? Suitgrouprate { get; set; }

        /// <summary>
        /// 项目原价
        /// </summary>
        public virtual decimal? ItemPrice { get; set; }

        /// <summary>
        /// 折扣后价格
        /// </summary>
        public virtual decimal? PriceAfterDis { get; set; }

        /// <summary>
        /// 选择状态0未选，1已选
        /// </summary>
        public int CheckState { get; set; }

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }

    }
}
