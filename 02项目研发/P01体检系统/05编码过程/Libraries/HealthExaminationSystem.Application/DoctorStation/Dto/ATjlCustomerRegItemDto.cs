using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
#if !Proxy
using AutoMapper;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 体检人检查项目结果表
    /// 医生站读取患者信息用
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlCustomerRegItem))]
#endif
    public class ATjlCustomerRegItemDto : EntityDto<Guid>
    {
        /// <summary>
        /// 项目Id外键
        /// </summary>
        public virtual Guid ItemId { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public virtual BTbmItemInfoDto ItemBM { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [StringLength(32)]
        public virtual string ItemName { get; set; }

        /// <summary>
        /// 项目类别 1说明性2数值型3计算性
        /// </summary>
        public virtual int? ItemTypeBM { get; set; }

        /// <summary>
        /// 项目序号
        /// </summary>
        public virtual int? ItemOrder { get; set; }

        /// <summary>
        /// 项目状态 1放弃2已检3待查
        /// </summary>
        public virtual int? ProcessState { get; set; }

        /// <summary>
        /// 暂停状态 1正常2暂停3解冻
        /// </summary>
        public virtual int? SuspendState { get; set; }

        /// <summary>
        /// 复合判断状态 1属于复合判断2不属于
        /// </summary>
        public virtual int? DiagnSate { get; set; }

        /// <summary>
        /// 总检退回状态 1未退回2已退回3已处理4已审核
        /// </summary>
        public virtual int? SummBackSate { get; set; }

        /// <summary>
        /// 检查人ID
        /// </summary>
        public virtual UserForComboDto InspectEmployeeBM { get; set; }

        /// <summary>
        /// 审核人ID
        /// </summary>
        public virtual UserForComboDto CheckEmployeeBM { get; set; }

        /// <summary>
        /// 总审核人ID
        /// </summary>
        public virtual UserForComboDto TotalEmployeeBM { get; set; }

        /// <summary>
        /// 项目结果，中文说明 数值型也存入
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemResultChar { get; set; }

        /// <summary>
        /// 项目标示 H偏高 HH超高L偏低 LL 超低M正常P异常
        /// </summary>
        [StringLength(16)]
        public virtual string Symbol { get; set; }

        /// <summary>
        /// 阳性状态 1正常2阳性
        /// </summary>
        public virtual int? PositiveSate { get; set; }

        /// <summary>
        /// 疾病状态 1疾病2重大疾病3阳性发现
        /// </summary>
        public virtual int? IllnessSate { get; set; }

        /// <summary>
        /// 危急值状态 1危急值2正常
        /// </summary>
        public virtual int? CrisisSate { get; set; }

        /// <summary>
        /// 仪器编号
        /// </summary>
        [StringLength(16)]
        public virtual string Instrument { get; set; }

        /// <summary>
        /// 参考值
        /// </summary>
        [StringLength(256)]
        public virtual string Stand { get; set; }

        /// <summary>
        /// 参考值来源 1体检中心维护2接口
        /// </summary>
        public virtual int? StandFrom { get; set; }

        /// <summary>
        /// 项目单位
        /// </summary>
        [StringLength(16)]
        public virtual string Unit { get; set; }
        
        /// <summary>
        /// 科室外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        public virtual Guid DepartmentId { get; set; }

        /// <summary>
        /// 项目小结
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemSum { get; set; }
        /// <summary>
        /// 项目诊断
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemDiagnosis { get; set; }
        /// <summary>
        /// 重度等级 1.轻微、2.中度、3.重度
        /// </summary>
        public virtual int? IllnessLevel { get; set; }

        /// <summary>
        /// 危急值等级
        /// </summary>
        public virtual int? CrisisLever { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
#if Application
        [IgnoreMap]
#endif
        public virtual int? IsMoRen { get; set; }

    }
}