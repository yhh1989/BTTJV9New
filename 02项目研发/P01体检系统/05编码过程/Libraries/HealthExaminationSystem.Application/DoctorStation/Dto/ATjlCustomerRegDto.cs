using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 体检人预约登记信息表  
    /// 医生站读取患者信息用
    /// </summary>
#if !Proxy 
    [AutoMap(typeof(TjlCustomerReg))]
#endif
    public class ATjlCustomerRegDto : EntityDto<Guid>
    {
        /// <summary>
        /// 工作历史
        /// </summary>
        public virtual ICollection<ATjlOAHistoryDto> OAHistory { get; set; }

        /// <summary>
        /// 既往史
        /// </summary>
        public virtual ICollection<ATjlOHistoryDto> OHistory { get; set; }

        /// <summary>
        /// 体检人科室小结
        /// </summary>
        public virtual ICollection<ATjlCustomerDepSummaryDto> CustomerDepSummary { get; set; }

        ///// <summary>
        ///// 体检人科室建议
        ///// </summary>
        //public virtual ICollection<SearchTjlCustomerSummaryDto> CustomerSummary { get; set; }
        
        /// <summary>
        /// 体检人
        /// </summary>
        public virtual ATjlCustomerDto Customer { get; set; }

        /// <summary>
        /// 单位预约信息
        /// </summary>
        public virtual ATjlClientInfoDto ClientInfo { get; set; }

        /// <summary>
        /// 体检人外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        public virtual Guid CustomerId { get; set; }
        /// <summary>
        /// 体检ID
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 体检场所-1院内-2外出
        /// </summary>

        public virtual int? ExamPlace { get; set; }

        /// <summary>
        /// 预约日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }

        /// <summary>
        /// 婚姻状况 1未婚2结婚
        /// </summary>
        public virtual int? MarriageStatus { get; set; }

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }

        /// <summary>
        /// 登记状态 1未登记 2已登记
        /// </summary>
        public virtual int? RegisterState { get; set; }

        /// <summary>
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }

        /// <summary>
        /// 总检状态 1未总检2已分诊3已初检4已审核
        /// </summary>
        public virtual int? SummSate { get; set; }

        /// <summary>
        /// 收费状态 1未收费2已收费3欠费
        /// </summary>
        public virtual int? CostState { get; set; }

        /// <summary>
        /// 初审医生id 用于总检分诊
        /// </summary>
        public virtual UserForComboDto CSEmployeeBM { get; set; }

        /// <summary>
        /// 复审医生id 用于总检分诊
        /// </summary>
        public virtual UserForComboDto FSEmployeeBM { get; set; }

        /// <summary>
        /// 是否加急 1不加急2加急
        /// </summary>
        public virtual int? UrgentState { get; set; }

        /// <summary>
        /// 末次月经
        /// </summary>
        [StringLength(16)]
        public virtual string LastMenstruation { get; set; }

        /// <summary>
        /// 孕天
        /// </summary>
        [StringLength(16)]
        public virtual string Gestation { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? RegAge { get; set; }

        /// <summary>
        /// 危害因素 逗号隔开
        /// </summary>
        [StringLength(800)]
        public virtual string RiskS { get; set; }

        /// <summary>
        /// 岗位类别
        /// </summary>
        [StringLength(16)]
        public virtual string PostState { get; set; }

        /// <summary>
        /// 工种
        /// </summary>
        [StringLength(16)]
        public virtual string TypeWork { get; set; }

        /// <summary>
        /// 车间
        /// </summary>
        [StringLength(16)]
        public virtual string WorkName { get; set; }

        /// <summary>
        /// 总工龄
        /// </summary>
        [StringLength(16)]
        public virtual string TotalWorkAge { get; set; }

        /// <summary>
        /// 接害工龄
        /// </summary>
        [StringLength(16)]
        public virtual string InjuryAge { get; set; }

        /// <summary>
        /// 体检类别 字典
        /// </summary>
        [StringLength(16)]
        public virtual string ClientType { get; set; }

        /// <summary>
        /// 准备孕或育
        /// </summary>
        public virtual int? ReadyPregnancybirth { get; set; }
        /// <summary>
        /// 交表确认 1未交表2已交表
        /// </summary>
        public virtual int? SendToConfirm { get; set; }


        /// <summary>
        /// 体检类别 1健康体检2职业健康体检3健康证体检4公务员体检5学生体检6驾驶证体检7婚检
        /// </summary>
        public virtual int? PhysicalType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 人员类别标识
        /// </summary>      
        public Guid? PersonnelCategoryId { get; set; }
    }

}