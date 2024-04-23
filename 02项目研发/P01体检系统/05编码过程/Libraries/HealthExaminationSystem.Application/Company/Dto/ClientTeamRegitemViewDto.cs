using System;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;

#if !Proxy
using AutoMapper;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Diagnosis.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
    /// <summary>
    /// 单位分组登记项目
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlClientTeamRegitem))]
#endif
    public class ClientTeamRegitemViewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 单位预约ID
        /// </summary>
        public Guid? ClientRegId { get; set; }

        /// <summary>
        /// 单位分组信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        ///</summary>
        public virtual Guid? ClientTeamInfoId { get; set; }


        /// <summary>
        /// 选择组合信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        ///</summary>
        public virtual Guid? TbmItemGroupid { get; set; }

        /// <summary>
        /// 组合编码
        /// </summary>
        public virtual string ItemGroupCodeBM { get; set; }
        /// <summary>
        /// 项目组合名称
        /// </summary>
        public virtual string ItemGroupName { get; set; }

        /// <summary>
        /// 组合序号
        /// </summary>
        public virtual int? ItemGroupOrder { get; set; }
        /// <summary>
        /// 选择科室信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        ///</summary>
        public virtual Guid? TbmDepartmentid { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>

        public virtual string DepartmentName { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>

        public virtual string DepartmentCodeBM { get; set; }
        /// <summary>
        /// 科室序号
        /// </summary>
        public virtual int? DepartmentOrder { get; set; }
        /// <summary>
        /// 选择套餐信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        ///</summary>
        public virtual Guid? ItemSuitId { get; set; }


        /// <summary>
        /// 套餐名称
        /// </summary>
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
        public virtual int PayerCatType { get; set; }

        /// <summary>
        /// 选择组合
        /// </summary>
        public virtual ItemGroupDto ItemGroup { get; set; }

        /// <summary>
        /// 团体加减项标示 减项的clientregitem删除customeritemgroup表改成减项标示 
        /// </summary>
        #if!Proxy
                [IgnoreMap]
        #endif
        public virtual int? IsAddMinus { get; set; }

        /// <summary>
        /// 体检状态
        /// </summary>
        public virtual bool? ProcessState { get; set; }


        /// <summary>
        ///是否职业健康项目1职业健康项目2健康体检项目3全部
        /// </summary>
        public virtual int? IsZYB { get; set; }
    }
}