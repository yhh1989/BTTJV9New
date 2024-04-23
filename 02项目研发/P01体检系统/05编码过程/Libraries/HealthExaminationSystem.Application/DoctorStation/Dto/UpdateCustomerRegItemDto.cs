using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 体检人项目
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomerRegItem))]
#endif
    public class UpdateCustomerRegItemDto : EntityDto<Guid>
    {
        /// <summary>
        /// 项目状态 1放弃2已检3待查
        /// </summary>
        public virtual int? ProcessState { get; set; }
        /// <summary>
        /// 检查人ID外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        public virtual long? InspectEmployeeBMId { get; set; }

        /// <summary>
        /// 审核人ID外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        public virtual long? CheckEmployeeBMId { get; set; }

        /// <summary>
        /// 项目Id外键
        /// </summary>
        public virtual Guid ItemId { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public virtual string ItemName { get; set; }
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
        /// 阳性状态 2正常1阳性
        /// </summary>
        public virtual int? PositiveSate { get; set; }

        /// <summary>
        /// 疾病状态 1疾病2重大疾病3阳性发现4
        /// </summary>
        public virtual int? IllnessSate { get; set; }

        /// <summary>
        /// 危急值状态 1正常2危急值
        /// </summary>
        public virtual int? CrisisSate { get; set; }

        /// <summary>
        /// 项目小结
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemSum { get; set; }
        /// <summary>
        /// 参考值
        /// </summary>
        [StringLength(256)]
        public virtual string Stand { get; set; }
        /// <summary>
        /// 项目单位
        /// </summary>
        [StringLength(16)]
        public virtual string Unit { get; set; }

        /// <summary>
        /// 参考值来源
        /// </summary>

        public virtual int? StandFrom { get; set; }
        /// <summary>
        /// 项目诊断
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemDiagnosis { get; set; }

    }
}
