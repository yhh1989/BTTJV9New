using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
    /// <summary>
    /// 用户预约组合
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomerItemGroup),typeof(TjlCustomerItemGroupDto))]
#endif
    public class SaveCusGroupsDto : EntityDto<Guid>
    {
        /// <summary>
        /// 单位预约id
        /// </summary>
        public virtual Guid? CustomerRegBMId { get; set; }
        /// <summary>
        /// 科室外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        public virtual Guid DepartmentId { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        [StringLength(64)]
        public virtual string DepartmentName { get; set; }

        /// <summary>
        /// 科室排序
        /// </summary>
        public virtual int? DepartmentOrder { get; set; }

        /// <summary>
        /// 项目组合id
        /// </summary>
        public virtual Guid ItemGroupBM_Id { get; set; }
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
        /// 组合编码
        /// </summary>
        [StringLength(32)]
        public virtual string ItemGroupCodeBM { get; set; }
        /// <summary>
        /// 暂停状态 1正常2暂停3解冻
        /// </summary>
        public virtual int? SuspendState { get; set; }

        /// <summary>
        /// 收费类别 SFType字典
        /// </summary>
        public virtual int? SFType { get; set; }

        /// <summary>
        /// 套餐外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        public virtual Guid? ItemSuitId { get; set; }
        /// <summary>
        /// 套餐名称
        /// </summary>
        [StringLength(256)]
        public virtual string ItemSuitName { get; set; }

        /// <summary>
        /// 项目检查状态 1未检查2已检查3部分检查4放弃5待查 6暂存
        /// </summary>
        public virtual int? CheckState { get; set; }

        /// <summary>
        /// 总检退回 1未退回2已退回3已处理4已审核
        /// </summary>
        public virtual int? SummBackSate { get; set; }
        /// <summary>
        /// 开单医生ID
        /// </summary>
        public virtual long? BillingEmployeeBMId { get; set; }
      
        /// <summary>
        /// 开单医生ID
        /// </summary>
        public virtual long? BillEmployeeBMId { get; set; }

        public virtual long? TotalEmployeeBMId { get; set; }

        
        /// <summary>
        /// 加减状态 1为正常项目2为加项3为减项
        /// </summary>
        public virtual int? IsAddMinus { get; set; }

        /// <summary>
        /// 退费状态 1正常2带退费3退费 收费处退费后变为减项状态
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
        /// 收费状态 1未收费2个人已支付3单位已支付
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
        public virtual Guid? MReceiptInfoPersonalId { get; set; }

        /// <summary>
        /// 单位结账ID
        /// </summary>
        public virtual Guid? MReceiptInfoClientlId { get; set; }
        /// <summary>
        /// 是否已打导引单 只有项目组合选择有变动，须同步状态
        /// </summary>
        public virtual int? GuidanceSate { get; set; }

        /// <summary>
        /// 是否已打条码 1未打印2已打印
        /// </summary>
        public virtual int? BarState { get; set; }

        /// <summary>
        /// 是否已打申请单 1未打印2已打印
        /// </summary>
        public virtual int? RequestState { get; set; }

        /// <summary>
        /// 是否已抽血 1未抽血2已抽血3无须抽血
        /// </summary>
        public virtual int? DrawSate { get; set; }

        /// <summary>
        /// 采血时间
        /// </summary>
        public virtual DateTime? Drawtime { get; set; }
        /// <summary>
        /// 是否核酸检测
        /// </summary>
        public virtual int? NucleicAcidState { get; set; }

        /// <summary>
        /// 是否条码核收 1未核收2已核收
        /// </summary>
        public virtual int? CollectionState { get; set; }

        /// <summary>
        ///是否职业健康项目1职业健康项目2健康体检项目3全部
        /// </summary>
        public virtual int? IsZYB { get; set; }

    }
}
