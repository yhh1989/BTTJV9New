using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using System;
using System.Collections.Generic;

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 查询类（方便查询体检）
    /// </summary>
    public class QueryClass
    { 
        /// <summary>
        /// 预约id
        /// </summary>
        public Guid? CustomerRegId { get; set; }
        /// <summary>
        /// 查询码
        /// </summary>
        public string WebQueryCode { get; set; }

        /// <summary>
        /// 科室ID
        /// </summary>
        public Guid? DepartmentBM { get; set; }

        /// <summary>
        /// 体检ID
        /// </summary>
        public string CustomerBM { get; set; }

        /// <summary>
        /// 获取小结格式和项目信息
        /// 仅用于查询这两个不用于其他
        /// </summary>
        public List<string> BasicDictionaryType { get; set; }
        /// <summary>
        /// 套餐类别
        /// </summary>
        public int? ItemSuitType { get; set; }
        /// <summary>
        /// 套餐名称ID
        /// </summary>
        public virtual List<Guid> ItemSuitBMId { get; set; }

        /// <summary>
        /// 体检人组合项目id
        /// </summary>
        public Guid? CustomerItemGroupId { get; set; }

        /// <summary>
        /// 体检人项目id
        /// </summary>
        public Guid? CustomerItemId { get; set; }
        /// <summary>
        /// 单位Id
        /// </summary>
        public List<Guid?> ClientInfoId { get; set; }

        /// <summary>
        /// 单位预约ID
        /// </summary>
        public List<Guid?> ClientInfoRegId { get; set; }
        /// <summary>
        /// 单位Id
        /// </summary>
        public List<Guid?> ClientTeamID { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 类别 1为普通用户2为VIP用户
        /// </summary>
        public virtual int? CustomerType { get; set; }
        /// <summary>
        /// 科室ID组合
        /// </summary>
        public List<Guid> DepartmentBMList { get; set; }
        /// <summary>
        /// 大科室ID
        /// </summary>
        public List<int> LargeDepartmentBMList { get; set; }

        /// <summary>
        /// 修改时间开始
        /// </summary>
        public int? DateType { get; set; }
        /// <summary>
        /// 修改时间开始
        /// </summary>
        public DateTime? LastModificationTimeBign { get; set; }
        /// <summary>
        /// 修改时间结束
        /// </summary>
        public DateTime? LastModificationTimeEnd { get; set; }
        /// <summary>
        /// 登记状态 1未登记 2已登记
        /// </summary>
        public virtual int? RegisterState { get; set; }

        /// <summary>
        /// 总检状态 1未总检2已分诊3已初检4已审核5审核不通过
        /// </summary>
        public virtual int? SummSate { get; set; }

        /// <summary>
        /// 条码号
        /// </summary>
        public string BarNumBM { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        public string DoctorName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int? Sex { get; set; }

        /// <summary>
        /// 登记状态 1检查医生 2审核医生
        /// </summary>
        public virtual int? EmpType { get; set; }

        /// <summary>
        /// 是否复查1是
        /// </summary>
        public virtual int? review { get; set; }

    }

    public class QueryClassTwo
    {
        /// <summary>
        /// 体检人id
        /// </summary>
        public Guid? CustomerId { get; set; }
        /// <summary>
        /// 科室ID组合
        /// </summary>
        public List<Guid> DepartmentBMList { get; set; }
     
        /// <summary>
        /// 组合项目状态
        /// </summary>
        public int? CheckState { get; set; }
        /// <summary>
        /// 项目状态
        /// </summary>
        public int? ProcessState { get; set; }
        /// <summary>
        /// 修改时间开始
        /// </summary>
        public DateTime? LastModificationTimeBign { get; set; }
        /// <summary>
        /// 修改时间结束
        /// </summary>
        public DateTime? LastModificationTimeEnd { get; set; }
        /// <summary>
        /// 1为按组合检查时间查询其他为按登记时间查询
        /// </summary>
        public int? isJC { get; set; }
        /// <summary>
        /// 危急值1正常2危急值
        /// </summary>
        public int? CrisisSate { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int? Sex { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 单位预约ID
        /// </summary>
        public Guid? ClientRegID { get; set; }
        /// <summary>
        /// 单位预约信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>     
        public virtual Guid? ClientRegId { get; set; }


        /// <summary>
        /// 体检类别 1健康体检2职业健康体检3健康证体检4公务员体检5学生体检6驾驶证体检7婚检
        /// </summary>
        public virtual int? PhysicalType { get; set; }
    }
    /// <summary>
    /// 新增修改传入参数
    /// </summary>
    public class UpdateClass
    {
        /// <summary>
        /// 科室ID
        /// </summary>
        public Guid? DepartmentBM { get; set; }
        /// <summary>
        /// 预约id
        /// </summary>
        public Guid? CustomerRegId { get; set; }
        /// <summary>
        /// 项目状态
        /// </summary>
        public int? ProcessState { get; set; }
        /// <summary>
        /// 危急值
        /// </summary>
        public int? CrisisSate { get; set; }

        /// <summary>
        /// 危急值回访状态 0未上报1已上报2已取消3已审核
        /// </summary>
        public virtual int? CrisisVisitSate { get; set; }

        /// <summary>
        /// 危急值等级
        /// </summary>
        public virtual int? CrisisLever { get; set; }
        /// <summary>
        /// 危急值提示
        /// </summary>
        public virtual string CrisiChar { get; set; }

        /// <summary>
        /// 重要异常说明
        /// </summary>
        public virtual string CrisiContent { get; set; }
        /// <summary>
        /// 体检人项目id
        /// </summary>
        public Guid? CustomerItemId { get; set; }

        /// <summary>
        /// 体检人组合项目id
        /// </summary>
        public Guid? CustomerItemGroupId { get; set; }

        /// <summary>
        /// 组合项目状态
        /// </summary>
        public int? CheckState { get; set; }
        /// <summary>
        /// 组合小结
        /// </summary>
        public string CharacterSummary { get; set; }
        /// <summary>
        /// 科室诊断小结
        /// </summary>
        public string DagnosisSummary { get; set; }


    }
    /// <summary>
    /// 返回人数
    /// </summary>
    public class ReturnClass
    {
        /// <summary>
        /// 已检查人数
        /// </summary>
        public int AlreadyInspect { get; set; }
        /// <summary>
        /// 未检查人数
        /// </summary>
        public int NotInspect { get; set; }
        /// <summary>
        /// 已登记人数
        /// </summary>
        public int AlreadyRegister { get; set; }
    }
    /// <summary>
    /// 统计查询类
    /// </summary>
    public class StatisticalClass
    {
        /// <summary>
        /// 单位
        /// </summary>
        public Guid? ClientInfo_Id { get; set; }
        /// <summary>
        /// 单位预约
        /// </summary>
        public Guid? ClientInfoReg_Id { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 时间类型
        /// </summary>
        public int? TimeState { get; set; }
        /// <summary>
        /// 检查状态
        /// </summary>
        public int? CheckState { get; set; }
        /// <summary>
        /// 登记状态
        /// </summary>
        public int? RegistState { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public List<string> DepartmentName { get; set; }
        /// <summary>
        /// 项目组合名称
        /// </summary>
        public List<string> ItemGroupName { get; set; }

        /// <summary>
        /// 开单医生
        /// </summary>
        public List<string> BillingEmployeeBM { get; set; }
    }

    public class DeparmentCustomerSearch
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 科室ID
        /// </summary>
        public List<Guid> DepartmentBM { get; set; }
        /// <summary>
        /// 体检号/姓名
        /// </summary>
        public string CustomerBMOrName { get; set; }
        /// <summary>
        /// 单位id
        /// </summary>
        public Guid? ClientInfoId { get; set; }
        /// <summary>
        /// 查询我的患者
        /// </summary>
        public bool IsOwn { get; set; }
        /// <summary>
        /// 登陆人id
        /// </summary>
        public long OwnId { get; set; }
        /// <summary>
        /// 查询我的未结论患者
        /// </summary>
        public bool IsOwnSumm { get; set; }
        /// <summary>
        /// 查询当前科室存在未完成项目的患者
        /// </summary>
        public bool NotComplete { get; set; }
        /// <summary>
        /// 体检状态
        /// </summary>
        public int? CheckSate { get; set; }
        /// <summary>
        /// 总检状态
        /// </summary>
        public int? SummSate { get; set; }
        
    }
}