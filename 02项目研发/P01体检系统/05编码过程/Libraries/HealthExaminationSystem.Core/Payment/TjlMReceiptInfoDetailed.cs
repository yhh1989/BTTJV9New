using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Core.Payment
{
    /// <summary>
    /// 结算明细表
    /// </summary>
    [Obsolete("暂停使用", true)]
    public class TjlMReceiptInfoDetailed : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 结算ID
        /// </summary>
        public virtual TjlMReceiptInfo MReceiptInfo { get; set; }

        /// <summary>
        /// 收费分类
        /// </summary>
        public virtual int ReceiptType { get; set; }

        /// <summary>
        /// 组合ID
        /// </summary>
        public virtual TbmItemGroup ItemGroup { get; set; }

        /// <summary>
        /// 组合编码
        /// </summary>
        [StringLength(32)]
        public virtual string ItemGroupCodeBM { get; set; }
        /// <summary>
        /// 项目组合名称
        /// </summary>
        [StringLength(32)]
        public virtual string ItemGroupName { get; set; }

        /// <summary>
        /// 组合序号
        /// </summary>
        public virtual int? ItemGroupOrder { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public virtual TbmDepartment Department { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        [StringLength(32)]
        public virtual string DepartmentName { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        [StringLength(32)]
        public virtual string DepartmentCodeBM { get; set; }
        /// <summary>
        /// 科室序号
        /// </summary>
        public virtual int? DepartmentOrder { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        public virtual decimal ItemGroupMoney { get; set; }

        /// <summary>
        /// 折扣后
        /// </summary>
        public virtual decimal ItemGroupDiscountMoney { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual decimal Discount { get; set; }

        ///// <summary>
        ///// 体检id
        ///// </summary>
        //// public virtual int CustoemrRegBM { get; set; }
        //public virtual TjlCustomerRegDto CustomerReg { get; set; }

        ///// <summary>
        ///// 开单医生
        ///// </summary>
        //// public virtual int BillingEmployeeBM { get; set; }
        //public virtual UserViewDto BillingUser { get; set; }

        ///// <summary>
        ///// 收费员
        ///// </summary>
        //// public virtual int ReceiptEmployeeBM { get; set; }
        //public virtual User ReceiptUser { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}