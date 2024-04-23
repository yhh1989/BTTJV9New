using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 科室工作量统计Dto
    /// </summary>
    public class SearchKSGZLStatisticsDto
    {
        ///// <summary>
        ///// 单位标题
        ///// </summary>
        //public virtual string ClientTitle { get; set; }
        /// <summary>
        /// 科室Id
        /// </summary>
        public virtual Guid Department_Id { get; set; }

        /// <summary>
        /// 分组Id
        /// </summary>
        public virtual Guid? ClientTeamInfoId { get; set; }
        /// <summary>
        /// 加项状态
        /// </summary>
        public virtual string IsAddMinus { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public virtual string TeamName { get; set; }

        /// <summary>
        /// 大科室
        /// </summary>
        public int? LargeDepart { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public virtual string DepartmentName { get; set; }
        /// <summary>
        /// 医生
        /// </summary>
        public virtual string InspectEmployeeName { get; set; }
        /// <summary>
        /// 已登记人数
        /// </summary>
        public virtual int RegisterNum { get; set; }
        /// <summary>
        /// 未登记人数
        /// </summary>
        public virtual int UnRegisterNum { get; set; }

        /// <summary>
        /// 项目序号
        /// </summary>
        public virtual int? ItemGroupOrder { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [StringLength(32)]
        public virtual string ItemGroupName { get; set; }
        /// <summary>
        /// 项目状态 放弃数1
        /// </summary>
        public virtual int? GiveUpNum { get; set; }
        /// <summary>
        /// 项目状态 已检数2
        /// </summary>
        public virtual int? CompleteNum { get; set; }
        /// <summary>
        /// 未检人数
        /// </summary>
        public virtual int? UnCheckedNum { get; set; }
        /// <summary>
        /// 已检人数
        /// </summary>
        public int? CheckedCount { get; set; }

        /// <summary>
        /// 项目状态 待查3
        /// </summary>
        public virtual int? AwaitNum { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public virtual decimal? ItemPrice { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public virtual int? Count { get; set; }
        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual decimal DiscountRate { get; set; }
        /// <summary>
        /// 应收金额
        /// </summary>
        public virtual decimal? ShouldMoney { get; set; }
        /// <summary>
        /// 实收金额
        /// </summary>
        public virtual decimal? ActualMoney { get; set; }
        /// <summary>
        /// 实检金额
        /// </summary>
        public virtual decimal? CheckMoney { get; set; }
        /// <summary>
        /// 科室实检金额
        /// </summary>
        public virtual decimal? DetCheckMoney { get; set; }
        /// <summary>
        /// 科室总金额
        /// </summary>
        public virtual decimal? DetActualMoney { get; set; }
        /// <summary>
        /// 科室应收金额
        /// </summary>
        public virtual decimal? DetShouldMoney { get; set; }

        /// <summary>
        /// 收费类别
        /// </summary>
        public virtual int? SFType { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }
        /// <summary>
        /// 介绍人
        /// </summary>
        public virtual string LinkMan { get; set; }

    }
    /// <summary>
    /// 科室压力
    /// </summary>
    public class SearchKSYLStatisticsDto
    {
        /// <summary>
        /// 科室Id
        /// </summary>
        public virtual Guid Department_Id { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public virtual string DepartmentName { get; set; }
        /// <summary>
        /// 医生
        /// </summary>
        public virtual string InspectEmployeeName { get; set; }

        /// <summary>
        /// 组合序号
        /// </summary>
        public virtual int? ItemGroupOrder { get; set; }
        /// <summary>
        /// 组合名称
        /// </summary>
        [StringLength(32)]
        public virtual string ItemGroupName { get; set; }
        /// <summary>
        /// 最大人数
        /// </summary>
        public virtual int? MaxNum { get; set; }
        /// <summary>
        /// 总人数
        /// </summary>
        public virtual int? TotalNum { get; set; }
        /// <summary>
        /// 在检人数3
        /// </summary>
        public virtual int? CheckingNum { get; set; }
        /// <summary>
        /// 未检人数
        /// </summary>
        public virtual int? UnCheckedNum { get; set; }
        /// <summary>
        /// 已检人数
        /// </summary>
        public virtual int? CheckedNum { get; set; }
        /// <summary>
        /// 科室开始检查时间
        /// </summary>
        public virtual DateTime? StartTime { get; set; }
        /// <summary>
        /// 平均时间
        /// </summary>
        public virtual double? AvgTime { get; set; }
       
    }

    /// <summary>
    /// 科室工作量统计图形Dto
    /// </summary>
    public class SearchKSGZLStatisticsTXDto
    {
        /// <summary>
        /// 科室Id
        /// </summary>
        public virtual Guid Department_Id { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public virtual string DepartmentName { get; set; }
        /// <summary>
        /// 体检人预约ID
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }
    }

    /// <summary>
    /// 科室环比工作量统计
    /// </summary>
    public class HBQueryClass
    {
        /// <summary>
        /// 科室ID组合
        /// </summary>
        public List<Guid> DepartmentBMList { get; set; }
        /// <summary>
        /// 当前开始时间
        /// </summary>
        public DateTime? DQStartTime { get; set; }
        /// <summary>
        /// 当前结束时间
        /// </summary>
        public DateTime? DQEndTime { get; set; }
        /// <summary>
        /// 过去开始时间
        /// </summary>
        public DateTime? LSStartTime { get; set; }
        /// <summary>
        /// 过去结束时间
        /// </summary>
        public DateTime? LSEndTime { get; set; }
        /// <summary>
        /// 是否查询周
        /// </summary>
        public bool WeekQuery { get; set; }
        /// <summary>
        /// 已检人数
        /// </summary>
        //public int? CkeckedCount { get; set; }
        /// <summary>
        /// 已检人次
        /// </summary>
        //public int? CheckedPerson { get; set; }
    }

    public class KSHBGZLStatisticsDto
    {
        /// <summary>
        /// 类型
        /// </summary>
        public virtual string Type { get; set; }
        /// <summary>
        /// 历史数据
        /// </summary>
        public virtual int? ComparativeData { get; set; }
        /// <summary>
        /// 当前数据
        /// </summary>
        public virtual int? CurrentData { get; set; }
    }

    public class CacheDto
    {
        public virtual string CreatTime { get; set; }
        public virtual int? Register { get; set; }

        public virtual string DepartmentName { get; set; }
    }
    /// <summary>
    /// 其他统计
    /// </summary>
    public class OtherStatisticsDto
    {
        /// <summary>
        /// 统计类别
        /// </summary>
        public virtual string StatisticsType { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string InspectEmployeeName { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public virtual string CustomerCount { get; set; }
        /// <summary>
        /// 男（人数）
        /// </summary>
        public virtual string MaleCount { get; set; }
        /// <summary>
        /// 女（人数）
        /// </summary>
        public virtual string FemaleCount { get; set; }
        /// <summary>
        /// 散检人数
        /// </summary>
        public virtual string ScatterCount { get; set; }
        /// <summary>
        /// 单位人数
        /// </summary>
        public virtual string NormalCount { get; set; }

    }
}
