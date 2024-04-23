using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TjlClientTeamInfo))]
#endif
    public class ClientTeamInfoesViewDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 单位预约
        /// </summary>

        public RegsChargeCusDto ClientReg { get; set; }

        /// <summary>
        /// 计算金额人员
        /// </summary>
        public ICollection<ChargeCusStateDto> CustomerReg { get; set; }

        /// <summary>
        /// 分组ID
        /// </summary>
        public virtual int TeamBM { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public virtual string TeamName { get; set; }

        /// <summary>
        /// 单位分组登记项目
        /// </summary>
        public virtual ICollection<ClientTeamRegitemViewDto> ClientTeamRegitem { get; set; }

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
        public virtual string RiskName { get; set; }

        /// <summary>
        /// 体检场所-院内-外出
        /// </summary>
        public virtual string ExamPlace { get; set; }

        /// <summary>
        /// 是否备孕 1备孕2不备孕
        /// </summary>
        public virtual int? ConceiveStatus { get; set; }

        ///// <summary>
        ///// 单位预约信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        ///// </summary>
        //[ForeignKey("ItemSuit")]
        //public virtual Guid? ItemSuitId { get; set; }
        /// <summary>
        /// 选择套餐id
        /// </summary>
        public virtual ItemSuitDto ItemSuit { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>
        public virtual string ItemSuitName { get; set; }

        /// <summary>
        /// 人数
        /// </summary>
        public virtual int? PersonAmount { get; set; }

        // 注释的时候 Doc 说明一起注释，说的脚趾头都记住了，长个人头都记不住
        ///// <summary>
        ///// 岗位类别
        ///// </summary>
        // public virtual int? PostStateBM { get; set; }
        //public virtual opo OPostState { get; set; }

        /// <summary>
        /// 是否早餐
        /// </summary>
        public virtual int? BreakfastStatus { get; set; }

        /// <summary>
        /// 是否短信
        /// </summary>
        public virtual int? MessageStatus { get; set; }

        /// <summary>
        /// 是否邮寄
        /// </summary>
        public virtual int? EmailStatus { get; set; }

        /// <summary>
        /// 是否健康管理
        /// </summary>
        public virtual int? HealthyMGStatus { get; set; }

        /// <summary>
        /// 是否盲检查 1正常2盲检
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


        public int TenantId { get; set; }
    }
}