using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    /// <summary>
    /// 体检人检查项目结果表 
    /// </summary>
    public class TjlCustomerRegItem : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 体检人检查项目组合id
        /// </summary>
        [ForeignKey("CustomerItemGroupBM")]
        public virtual Guid? CustomerItemGroupBMid { get; set; }
        /// <summary>
        /// 体检人检查项目组合id
        /// </summary>
        public virtual TjlCustomerItemGroup CustomerItemGroupBM { get; set; }

        /// <summary>
        /// 预约外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey("CustomerRegBM")]
        public virtual Guid CustomerRegId { get; set; }

        /// <summary>
        /// 预约id
        /// </summary>
        public virtual TjlCustomerReg CustomerRegBM { get; set; }

        /// <summary>
        /// 科室外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey("DepartmentBM")]
        public virtual Guid DepartmentId { get; set; }

        /// <summary>
        /// 科室ID
        /// </summary>
        public virtual TbmDepartment DepartmentBM { get; set; }

        /// <summary>
        /// 组合外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey("ItemGroupBM")]
        public virtual Guid? ItemGroupBMId { get; set; }

        /// <summary>
        /// 组合ID
        /// </summary>
        public virtual TbmItemGroup ItemGroupBM { get; set; }

        /// <summary>
        /// 项目Id外键
        /// </summary>
        [ForeignKey(nameof(ItemBM))]
        public virtual Guid ItemId { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public virtual TbmItemInfo ItemBM { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemCodeBM { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [StringLength(64)]
        public virtual string ItemName { get; set; }

        /// <summary>
        /// 项目类型///数值型1,计算2，说明型3，阴阳型4
        /// </summary>
        public virtual int? ItemTypeBM { get; set; }

        /// <summary>
        /// 项目序号
        /// </summary>
        public virtual int? ItemOrder { get; set; }

        /// <summary>
        /// 项目状态 1未检查2已检查3部分检查4放弃5待查
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
        /// 检查人ID外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey("InspectEmployeeBM")]
        public virtual long? InspectEmployeeBMId { get; set; }

        /// <summary>
        /// 检查人ID
        /// </summary>
        public virtual User InspectEmployeeBM { get; set; }

        /// <summary>
        /// 审核人ID外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey("CheckEmployeeBM")]
        public virtual long? CheckEmployeeBMId { get; set; }

        /// <summary>
        /// 审核人ID
        /// </summary>
        public virtual User CheckEmployeeBM { get; set; }

        /// <summary>
        /// 总审核人标识
        /// </summary>
        [ForeignKey(nameof(TotalEmployeeBM))]
        public virtual long? TotalEmployeeBMId { get; set; }

        /// <summary>
        /// 总审核人
        /// </summary>
        public virtual User TotalEmployeeBM { get; set; }

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
        /// 危急值状态2正常1危急值
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
        /// 参考值来源 1体检中心维护2接口3数据导入
        /// </summary>
        public virtual int? StandFrom { get; set; }

        /// <summary>
        /// 项目单位
        /// </summary>
        [StringLength(16)]
        public virtual string Unit { get; set; }

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
        /// 危急值回访状态 0未上报1已上报2已取消3已审核
        /// </summary>
        public virtual int? CrisisVisitSate { get; set; }

        /// <summary>
        /// 危急值等级
        /// </summary>
        public virtual int? CrisisLever { get; set; }
        /// <summary>
        /// 重要异常提示
        /// </summary>
        public virtual string CrisiChar { get; set; }
        /// <summary>
        /// 重要异常说明
        /// </summary>
        public virtual string CrisiContent { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public virtual DateTime? ExamineTime { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>     
        [ForeignKey("ExamineUser")]
        public virtual long? ExamineUserId { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public virtual User ExamineUser { get; set; }

        /// <summary>
        /// 通知日期
        /// </summary>
        public virtual DateTime? MessageTime { get; set; }
        /// <summary>
        /// 通知人
        /// </summary>     
        [ForeignKey("MessageUser")]
        public virtual long? MessageUserId { get; set; }

        /// <summary>
        /// 通知人
        /// </summary>
        public virtual User MessageUser { get; set; }
        /// <summary>
        /// 通知状态1未通知2已通知
        /// </summary>
        public virtual int? MessageState { get; set; }

        /// <summary>
        /// 报告编码 
        /// </summary>
        [StringLength(640)]
        public virtual string ReportBM { get; set; }

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }
        /// <inheritdoc />
        public int TenantId { get; set; }


    }
}