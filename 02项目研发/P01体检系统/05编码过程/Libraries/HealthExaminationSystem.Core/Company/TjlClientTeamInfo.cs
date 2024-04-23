using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Illness;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;

namespace Sw.Hospital.HealthExaminationSystem.Core.Company
{
    /// <summary>
    /// 单位分组信息
    /// </summary>
    public class TjlClientTeamInfo : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 体检预约集合
        /// </summary>
        public virtual ICollection<TjlCustomerReg> CustomerReg { get; set; }

        /// <summary>
        /// 分组ID
        /// </summary>
        public virtual int TeamBM { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        [StringLength(256)]
        public virtual string TeamName { get; set; }
        /// <summary>
        /// 单位预约信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey("ClientReg")]
        public virtual Guid ClientRegId { get; set; }
        /// <summary>
        /// 单位预约登记
        /// </summary>
        [Required]
        public virtual TjlClientReg ClientReg { get; set; }

        /// <summary>
        /// 单位分组登记项目
        /// </summary>
        public virtual ICollection<TjlClientTeamRegitem> ClientTeamRegitem { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int Sex { get; set; }

        /// <summary>
        /// 最小年龄
        /// </summary>
        public virtual int MinAge { get; set; }

        /// <summary>
        /// 最大年龄
        /// </summary>
        public virtual int MaxAge { get; set; }

        /// <summary>
        /// 结婚状态
        /// </summary>
        public virtual int MaritalStatus { get; set; }

        /// <summary>
        /// 危害因素
        /// </summary>
        [StringLength(256)]
        public virtual string RiskName { get; set; }
        /// <summary>
        /// 危害因素
        /// </summary>
        public virtual List<TbmOccHazardFactor> OccHazardFactors { get; set; }

        /// <summary>
        /// 检查类别
        /// </summary>
        [StringLength(256)]
        public virtual string CheckType { get; set; }
        /// <summary>
        /// 车间
        /// </summary>
        [StringLength(256)]
        public virtual string WorkShop { get; set; }

        /// <summary>
        /// 工种
        /// </summary>
        [StringLength(256)]
        public virtual string  WorkType { get; set; }

        /// <summary>
        /// 体检场所-院内-外出
        /// </summary>
        [StringLength(64)]
        public virtual string ExamPlace { get; set; }

        /// <summary>
        /// 启用备孕 1备孕2不备孕
        /// </summary>
        public virtual int? ConceiveStatus { get; set; }
        /// <summary>
        /// 单位预约信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey("ItemSuit")]
        public virtual Guid? ItemSuit_Id { get; set; }
        /// <summary>
        /// 选择套餐id
        /// </summary>
        public virtual TbmItemSuit ItemSuit { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>
        [StringLength(128)]
        public virtual string ItemSuitName { get; set; }

        /// <summary>
        /// 人数
        /// </summary>
        public virtual int? PersonAmount { get; set; }

        /// <summary>
        /// 岗位类别
        /// </summary>
        // public virtual int? PostStateBM { get; set; }
        [Obsolete("暂停使用", true)]
        public virtual TbmOPostState OPostState { get; set; }



        /// <summary>
        /// 启用早餐
        /// </summary>
        public virtual int? BreakfastStatus { get; set; }

        /// <summary>
        /// 启用短信
        /// </summary>
        public virtual int? MessageStatus { get; set; }

        /// <summary>
        /// 启用邮寄
        /// </summary>
        public virtual int? EmailStatus { get; set; }

        /// <summary>
        /// 启用健康管理
        /// </summary>
        public virtual int? HealthyMGStatus { get; set; }

        /// <summary>
        /// 启用盲检查 1正常2盲检
        /// </summary>
        public virtual int BlindSate { get; set; }

        /// <summary>
        /// 锁定状态 1锁定2未锁定
        /// </summary>
        public virtual int Locking { get; set; }

        /// <summary>
        /// 体检类型 字典 TJType
        /// </summary>
        public virtual int TJType { get; set; }

        /// <summary>
        /// 拿报告时间 系统参数设定
        /// </summary>
        public virtual DateTime? HowDayReport { get; set; }

        /// <summary>
        /// 分组价格
        /// </summary>
        public virtual decimal? TeamMoney { get; set; }

        /// <summary>       
        /// 分组折扣
        /// </summary>
        public virtual decimal? TeamDiscount { get; set; }

        /// <summary>
        /// 分组折扣后价格
        /// </summary>
        public virtual decimal? TeamDiscountMoney { get; set; }

        /// <summary>
        /// 支付方式 单位支付，个人支付，固定金额
        /// </summary>
        public virtual int? CostType { get; set; }

        /// <summary>
        /// 加项支付方式 单位支付，个人支付，固定金额
        /// </summary>
        public virtual int? JxType { get; set; }

        /// <summary>
        /// 加项金额
        /// </summary>
        public virtual decimal? Jxje { get; set; }

        /// <summary>
        /// 加项折扣
        /// </summary>
        public virtual decimal? Jxzk { get; set; }

        /// <summary>
        /// 加项折扣后价格
        /// </summary>
        public virtual decimal? Jxzkjg { get; set; }

        /// <summary>
        /// 商务价格
        /// </summary>
        public virtual decimal? SWjg { get; set; }



        /// <summary>
        /// 限额金额
        /// </summary>
        public virtual decimal? QuotaMoney { get; set; }
        /// <summary>
        /// 所属院区
        /// </summary>
        public virtual int? HospitalArea { get; set; }

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }
        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}