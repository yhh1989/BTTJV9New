using System;
using System.Linq;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HealthExaminationSystem.Enumerations.Helpers;
#if !Proxy
using Abp.AutoMapper;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlClientTeamInfo))]
#endif
    public class CreateClientTeamInfoesDto : EntityDto<Guid>
    {

        ///// <summary>
        ///// 人员计算金额计算
        ///// </summary>
       // public ICollection<ChargeCusStateDto> CustomerReg { get; set; }
        //public RegsChargeCusDto ClientReg { get; set; }
        /// <summary>
        /// 分组ID
        /// </summary>
        public virtual int TeamBM { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>

        public virtual string TeamName { get; set; }

        /// <summary>
        /// 单位预约信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>

        public virtual Guid ClientRegId { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int Sex { get; set; }
        /// <summary>
        /// 性别格式化
        /// </summary>
#if Application
        [IgnoreMap]
#endif
        public virtual string FormatSex
        {
            get
            {
                var SexName = SexHelper.GetSexModelsForItemInfo().Where(o => o.Id == Sex).Select(o => o.Display).FirstOrDefault();
                return SexName;
            }

        }


        /// <summary>
        /// 最小年龄
        /// </summary>
        public virtual int MinAge { get; set; }
        
        public virtual string FormatMinAge
        {
            get
            {
                return MinAge + "岁";
            }
        }
        /// <summary>
        /// 最大年龄
        /// </summary>
        public virtual int MaxAge { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string FormatMaxAge
        {
            get
            {
                return MaxAge + "岁";
            }
        }
        /// <summary>
        /// 结婚状态
        /// </summary>
        public virtual int MaritalStatus { get; set; }

#if Application
        [IgnoreMap]
#endif 
        public virtual string FormatMaritalStatus
        {
            get
            {

                string Breed = EnumHelper.GetEnumDesc((MarrySate)MaritalStatus);

                return Breed;

            }

        }
        /// <summary>
        /// 单位预约分组危害因素
        /// </summary>
#if Application
        [IgnoreMap]
#endif 
        public List<Guid> ClientTeamRisk { get; set; }
        /// <summary>
        /// 危害因素
        /// </summary>

        public virtual string RiskName { get; set; }

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
        public virtual string WorkType { get; set; }

        /// <summary>
        /// 体检场所-院内-外出
        /// </summary>

        public virtual string ExamPlace { get; set; }

        /// <summary>
        /// 是否备孕 1备孕2不备孕
        /// </summary>
        public virtual int? ConceiveStatus { get; set; }

#if Application
        [IgnoreMap]
#endif
        public virtual string FormatConceiveStatus
        {
            get
            {

                string Conceive = EnumHelper.GetEnumDesc((BreedState)ConceiveStatus);

                return Conceive;

            }

        }
        ///// <summary>
        ///// 单位预约信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        ///// </summary>
        //[ForeignKey("ItemSuit")]
        //public virtual Guid? ItemSuitId { get; set; }
        /// <summary>
        /// 选择套餐编码
        /// </summary>
        public virtual string ItemSuitBM { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>

        [StringLength(128)]
        public virtual string ItemSuitName { get; set; }
        /// <summary>
        /// 套餐id
        /// </summary>
        public virtual Guid? ItemSuit_Id { get; set; }

        /// <summary>
        /// 人数
        /// </summary>
        public virtual int? PersonAmount { get; set; }

        /// <summary>
        /// 岗位类别
        /// </summary>

        // public virtual int? PostStateBM { get; set; }
        public virtual Guid? OPostState_id { get; set; }

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
        public virtual int? TJType { get; set; }

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
        /// 预约开始时间
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 预约结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 判断是编辑还是新增
        /// </summary>
        public bool? EditModle { get; set; }
        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal? YingShouJinE { get; set; }
        //#if !Proxy
        //        [IgnoreMap]
        //#endif
        //        public virtual decimal YingShouJinE
        //        {
        //            get
        //            {
        //                var mcusPayMoneys = CustomerReg?.Select(  r => r.McusPayMoney);
        //                var sumMcusPayMoneys = mcusPayMoneys?.Sum(r => r?.ClientMoney);
        //                return sumMcusPayMoneys ?? decimal.Parse("0.00");
        //            }
        //        }
        /// <summary>
        /// 实检金额
        /// </summary>
        public decimal? ShiJianJinE { get; set; }
        //#if !Proxy
        //        [IgnoreMap]
        //#endif
        //        public virtual decimal ShiJianJinE
        //        {
        //            get
        //            {

        //                var McusPayMoneys = CustomerReg?.Where(r => r.CheckSate != 1).Select(r => r.McusPayMoney);
        //                var sumMcusPayMoneys = McusPayMoneys?.Sum(r => r?.ClientMoney);
        //                return sumMcusPayMoneys ?? decimal.Parse("0.00");
        //            }
        //        }
        //        /// <summary>
        //        /// 
        //        /// </summary>
        //#if !Proxy
        //        [IgnoreMap]
        //#endif
        //        public virtual decimal AddJinE
        //        {
        //            get
        //            {
        //                var mcusPayMoneys = CustomerReg?.Select(r => r.McusPayMoney);
        //                var sumMcusPayMoneys = mcusPayMoneys?.Sum(r => r?.ClientAddMoney);
        //                return sumMcusPayMoneys ?? decimal.Parse("0.00");
        //            }
        //        }
        /// <summary>
        /// 加项金额
        /// </summary>
        public decimal? JianxiangJinE { get; set; }
        //#if !Proxy
        //        [IgnoreMap]
        //#endif
        //        public virtual decimal JianxiangJinE
        //        {
        //            get
        //            {
        //                var mcusPayMoneys = CustomerReg?.Select(r => r.McusPayMoney);
        //                var sumMcusPayMoneys = mcusPayMoneys?.Sum(r => r?.ClientMinusMoney);
        //                return sumMcusPayMoneys ?? decimal.Parse("0.00");
        //            }
        //        }
        /// <summary>
        /// 体检人数
        /// </summary>
        public int? TiJianRenShu { get; set; }
        //#if !Proxy
        //        [IgnoreMap]
        //#endif
        //        public virtual int TiJianRenShu
        //        {
        //            get
        //            {
        //                var count = CustomerReg?.Count;
        //                return count ?? 0;
        //            }
        //        }
        //        /// <summary>
        //        /// 实检人数
        //        /// </summary>
        //#if !Proxy
        //        [IgnoreMap]
        //#endif
        /// <summary>
        /// 实检人数
        /// </summary>
        public virtual int? SumSJRS { get; set; }
        //        {
        //            get
        //            {
        //                var count = CustomerReg?.Where(r => r.CheckSate != 1).Count();
        //                return count ?? 0;
        //            }
        //        }

        //#if !Proxy
        //        [IgnoreMap]
        //#endif
        //        public virtual int WJRS
        //        {
        //            get
        //            {
        //                var count = CustomerReg?.Where(r => r.CheckSate == 1).Count();
        //                return count ?? 0;
        //            }
        //        }

        /// <summary>
        /// 人员登记状态
        /// </summary>
        public bool? RegisterState { get; set; }
    }
}