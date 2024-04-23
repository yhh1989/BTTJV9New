using System;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 项目参考值设置 
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TbmItemStandard))]
#endif
    public class SearchItemStandardDto : EntityDto<Guid>
    {
        /// <summary>
        /// 项目外键
        /// </summary>
        public virtual Guid ItemId { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual string Sex { get; set; }

        /// <summary>
        /// 最小年龄
        /// </summary>
        public virtual int? MinAge { get; set; }

        /// <summary>
        /// 最大年龄
        /// </summary>
        public virtual int? MaxAge { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public virtual decimal? MinValue { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public virtual decimal? MaxValue { get; set; }

        /// <summary>
        /// 结果异常 1异常2正常
        /// </summary>
        public virtual int? PositiveSate { get; set; }

        /// <summary>
        /// 标示状态0偏高 1极高 2偏低  3极低 4正常 5异常
        /// </summary>
        public virtual int? IsNormal { get; set; }


        /// <summary>
        /// 结果判断状态 1大小区间2包含问字3等于文字
        /// </summary>
        public virtual int? CheckType { get; set; }

        /// <summary>
        /// 病种
        /// </summary>
        public virtual string Symbol { get; set; }

        /// <summary>
        /// 结论
        /// </summary>
        public virtual string Summ { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int? OrderNum { get; set; }

        /// <summary>
        /// 期间 如排卵期
        /// </summary>
        public virtual string Period { get; set; }

        /// <summary>
        /// 是否职业健康
        /// </summary>
        public virtual int? IsIndustrial { get; set; }

        /// <summary>
        /// 开始工龄
        /// </summary>
        public virtual int? WorkAgeStart { get; set; }

        /// <summary>
        /// 结束工龄
        /// </summary>
        public virtual int? WorkAgeEnd { get; set; }

        /// <summary>
        /// 危害因素
        /// </summary>
        public virtual string HazardFactors { get; set; }
        /// <summary>
        /// 重度等级 1.轻微、2.中度、3.重度
        /// </summary>
        public virtual int? IllnessLevel { get; set; }

        /// <summary>
        /// 体检类别 1健康体检2职业健康体检3健康证体检4公务员体检5学生体检6驾驶证体检7婚检
        /// </summary>
        public virtual int? PhysicalType { get; set; }
    }
}