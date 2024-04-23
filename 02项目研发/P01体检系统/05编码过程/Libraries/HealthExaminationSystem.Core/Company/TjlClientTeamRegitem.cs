using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;

namespace Sw.Hospital.HealthExaminationSystem.Core.Company
{
    /// <summary>
    /// 单位分组登记项目
    /// </summary>
    public class TjlClientTeamRegitem : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 单位分组信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey(nameof(ClientTeamInfo))]
        public virtual Guid? ClientTeamInfoId { get; set; }

        /// <summary>
        /// 分组ID
        /// </summary>
        public virtual TjlClientTeamInfo ClientTeamInfo { get; set; }

        //public virtual CustomerRegDto ClientTeamInfos { get; set; }
        /// <summary>
        /// 选择组合信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey(nameof(ItemGroup))]
        public virtual Guid? TbmItemGroupId { get; set; }

        /// <summary>
        /// 项目组合ID
        /// </summary>
        public virtual TbmItemGroup ItemGroup { get; set; }

        /// <summary>
        /// 组合编码
        /// </summary>
        [StringLength(64)]
        public virtual string ItemGroupCodeBM { get; set; }

        /// <summary>
        /// 项目组合名称
        /// </summary>
        [StringLength(64)]
        public virtual string ItemGroupName { get; set; }

        /// <summary>
        /// 组合序号
        /// </summary>
        public virtual int? ItemGroupOrder { get; set; }

        /// <summary>
        /// 选择科室信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey(nameof(Department))]
        public virtual Guid? TbmDepartmentId { get; set; }

        /// <summary>
        /// 科室ID
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
        [StringLength(64)]
        public virtual string DepartmentCodeBM { get; set; }

        /// <summary>
        /// 科室序号
        /// </summary>
        public virtual int? DepartmentOrder { get; set; }

        /// <summary>
        /// 选择套餐信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey(nameof(ItemSuit))]
        public virtual Guid? ItemSuitId { get; set; }

        /// <summary>
        /// 套餐ID
        /// </summary>
        public virtual TbmItemSuit ItemSuit { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>
        [StringLength(256)]
        public virtual string ItemSuitName { get; set; }

        /// <summary>
        /// 组合价格
        /// </summary>
        public virtual decimal ItemGroupMoney { get; set; }

        /// <summary>
        /// 组合折扣率
        /// </summary>
        public virtual decimal Discount { get; set; }

        /// <summary>
        /// 组合折扣后价格
        /// </summary>
        public virtual decimal ItemGroupDiscountMoney { get; set; }

        /// <summary>
        /// 支付方式 个人支付 团体支付
        /// </summary>
        [Obsolete("暂停使用", true)]
        [NotMapped]
        public virtual string PayerCat { get; set; }

        /// <summary>
        /// 支付方式 个人支付 团体支付
        /// </summary>
        public virtual int PayerCatType { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }

        /// <summary>
        /// 单位预约标识
        /// </summary>
        /// <remarks>
        /// 必填项，自己验证
        /// </remarks>
        [ForeignKey(nameof(ClientReg))]
        public Guid? ClientRegId { get; set; }
        /// <summary>
        /// 所属院区
        /// </summary>
        public virtual int? HospitalArea { get; set; }
        /// <summary>
        /// 单位预约
        /// </summary>
        public virtual TjlClientReg ClientReg { get; set; }

        /// <summary>
        ///是否职业健康项目1职业健康项目2健康体检项目3全部
        /// </summary>
        public virtual int? IsZYB { get; set; }

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }
    }
}