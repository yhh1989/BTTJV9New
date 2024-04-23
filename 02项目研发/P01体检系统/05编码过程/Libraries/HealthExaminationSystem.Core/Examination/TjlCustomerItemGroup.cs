using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    /// <summary>
    /// 体检人项目组合
    /// </summary>
    public class TjlCustomerItemGroup : FullAuditedEntity<Guid>, IMustHaveTenant
    {


        /// <summary>
        /// 调项记录表
        /// </summary>
        public virtual TjlAdjustMoney AdjustMoney { get; set; }

        /// <summary>
        /// 体检人项目
        /// </summary>
        public virtual ICollection<TjlCustomerRegItem> CustomerRegItem { get; set; }

        /// <summary>
        /// 体检人预约主键
        /// </summary>
        [ForeignKey("CustomerRegBM")]
        public virtual Guid? CustomerRegBMId { get; set; }

        /// <summary>
        /// 体检人预约ID
        /// </summary>
        public virtual TjlCustomerReg CustomerRegBM { get; set; }

        /// <summary>
        /// 科室外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey("DepartmentBM")]
        public virtual Guid DepartmentId { get; set; }

        /// <summary>
        /// 科室ID
        /// </summary>
        public virtual TbmDepartment DepartmentBM { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        [StringLength(64)]
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
        /// 项目组合ID
        /// </summary>
        [ForeignKey("ItemGroupBM")]
        public virtual Guid? ItemGroupBM_Id { get; set; }

        /// <summary>
        /// 项目组合ID
        /// </summary>
        public virtual TbmItemGroup ItemGroupBM { get; set; }

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
        /// 暂停状态 1正常2暂停3解冻
        /// </summary>
        public virtual int? SuspendState { get; set; }

        /// <summary>
        /// 收费类别 SFType字典
        /// </summary>
        public virtual int? SFType { get; set; }

        /// <summary>
        /// 项目检查状态 1未检查2已检查3部分检查4放弃5待查
        /// </summary>
        public virtual int? CheckState { get; set; }

        /// <summary>
        /// 总检退回 1未退回2已退回3已处理4已审核bxy
        /// </summary>
        public virtual int? SummBackSate { get; set; }

        /// <summary>
        /// 开单医生ID
        /// </summary>
        [ForeignKey("BillingEmployeeBM")]
        public virtual long? BillingEmployeeBMId { get; set; }
        /// <summary>
        /// 开单医生
        /// </summary>
        public virtual User BillingEmployeeBM { get; set; }
        /// <summary>
        /// 开单医生ID
        /// </summary>
        [ForeignKey("BillEmployeeBM")]
        public virtual long? BillEmployeeBMId { get; set; }
        /// <summary>
        /// 开单医生
        /// </summary>
        public virtual User BillEmployeeBM { get; set; }
        /// <summary>
        /// 检查人bxy
        /// </summary>
        [ForeignKey("InspectEmployeeBM")]
        public virtual long? InspectEmployeeBMId { get; set; }
        /// <summary>
        /// 检查人bxy
        /// </summary>
        public virtual User InspectEmployeeBM { get; set; }

        /// <summary>
        /// 审核人bxy
        /// </summary>
        [ForeignKey("CheckEmployeeBM")]
        public virtual long? CheckEmployeeBMId { get; set; }
        /// <summary>
        /// 审核人 bxy
        /// </summary>
        public virtual User CheckEmployeeBM { get; set; }

        /// <summary>
        /// 总审人标识
        /// </summary>
        [ForeignKey(nameof(TotalEmployeeBM))]
        public virtual long? TotalEmployeeBMId { get; set; }

        /// <summary>
        /// 总审人
        /// </summary>
        public virtual User TotalEmployeeBM { get; set; }

        /// <summary>
        /// 加减状态 1为正常项目2为加项3为减项4调项减5调项加
        /// </summary>
        public virtual int? IsAddMinus { get; set; }

        /// <summary>
        /// 退费状态 10正常9待退费8退费 收费处退费后变为减项状态
        /// </summary>
        public virtual int? RefundState { get; set; }

        /// <summary>
        /// 组合价格
        /// </summary>
        public virtual decimal ItemPrice { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual decimal DiscountRate { get; set; }

        /// <summary>
        /// 折后价格
        /// </summary>
        public virtual decimal PriceAfterDis { get; set; }

        /// <summary>
        /// 收费状态 1未收费2个人已支付3单位已支付4.混合付款5赠检 只标示个人是否付费，团体
        /// </summary>
        public virtual int? PayerCat { get; set; }

        /// <summary>
        /// 团体支付金额
        /// </summary>
        public virtual decimal TTmoney { get; set; }

        /// <summary>
        /// 个人支付金额
        /// </summary>
        public virtual decimal GRmoney { get; set; }

        /// <summary>
        /// 个人结账ID
        /// </summary>
        [ForeignKey("MReceiptInfoPersonal")]
        public virtual Guid? MReceiptInfoPersonalId { get; set; }
        /// <summary>
        /// 个人结账表
        /// </summary>
        public virtual TjlMReceiptInfo MReceiptInfoPersonal { get; set; }

        /// <summary>
        /// 单位结账ID
        /// </summary>
        [ForeignKey("MReceiptInfoClient")]
        public virtual Guid? MReceiptInfoClientlId { get; set; }
        /// <summary>
        /// 单位结账表
        /// </summary>
        public virtual TjlMReceiptInfo MReceiptInfoClient { get; set; }

        /// <summary>
        /// 是否已打导引单 只有项目组合选择有变动，须同步状态
        /// </summary>
        public virtual int? GuidanceSate { get; set; }

        /// <summary>
        /// 是否已打条码 1未打印2已打印
        /// </summary>
        public virtual int? BarState { get; set; }

        /// <summary>
        /// 是否条码核收 1未核收2已核收
        /// </summary>
        public virtual int? CollectionState { get; set; }

        /// <summary>
        /// 是否已打申请单 1未打印2已打印
        /// </summary>
        public virtual int? RequestState { get; set; }

        /// <summary>
        /// 是否已抽血 1未抽血2已抽血3无须抽血
        /// </summary>
        public virtual int? DrawSate { get; set; }

        /// <summary>
        /// 套餐外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey("ItemSuit")]
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
        /// 采血时间
        /// </summary>
        public virtual DateTime? Drawtime { get; set; }

        /// <summary>
        /// 采血人
        /// </summary>
        public virtual User DrawEmployeeIBM { get; set; }

        /// <summary>
        /// 组合小结 bxy
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemGroupSum { get; set; }
        /// <summary>
        /// 组合诊断 bxy
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemGroupDiagnosis { get; set; }
        /// <summary>
        /// 原生组合小结 
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemGroupOriginalDiag { get; set; }
        /// <summary>
        /// 第一次检查时间 bxy
        /// </summary>
        public virtual DateTime? FirstDateTime { get; set; }

        /// <summary>
        ///是否职业健康项目1职业健康项目2健康体检项目3全部
        /// </summary>
        public virtual int? IsZYB { get; set; }

        /// <summary>
        /// 审核备注 
        /// </summary>
        [StringLength(256)]
        public virtual string SumRemark { get; set; }

        /// <summary>
        /// 报告编码 
        /// </summary>
        [StringLength(256)]
        public virtual string ReportBM { get; set; }
        /// <summary>
        /// 申请单号码 
        /// </summary>
        [StringLength(256)]
        public virtual string ApplicationNo { get; set; }

        /// <summary>
        /// Pacs号
        /// </summary>
        [StringLength(32)]
        public virtual string PacsBM { get; set; }

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }


        /// <summary>
        /// 费用单据号 
        /// </summary>
        [StringLength(32)]
        public string fee_no { get; set; }
        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}