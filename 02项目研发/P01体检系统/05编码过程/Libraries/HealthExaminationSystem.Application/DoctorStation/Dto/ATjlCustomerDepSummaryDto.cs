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
    /// 体检人科室小结
    /// 医生站读取患者信息用
    /// </summary> 
#if !Proxy
    [AutoMap(typeof(TjlCustomerDepSummary))]
#endif
    public class ATjlCustomerDepSummaryDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检人预约信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        public virtual Guid? CustomerRegId { get; set; }

        /// <summary>
        /// 科室信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        public virtual Guid? DepartmentBMId { get; set; }
        /// <summary>
        /// 科室小结
        /// </summary>
        [StringLength(3072)]
        public virtual string CharacterSummary { get; set; }

        /// <summary>
        /// 隐私科室小结
        /// </summary>
        [StringLength(3072)]
        public virtual string PrivacyCharacterSummary { get; set; }

        /// <summary>
        /// 科室诊断小结
        /// </summary>
        [StringLength(3072)]
        public virtual string DagnosisSummary { get; set; }

        /// <summary>
        /// 体检日期
        /// </summary>
        public virtual DateTime? CheckDate { get; set; }

        /// <summary>
        /// 系统生成小结
        /// </summary>
        [StringLength(3072)]
        public virtual string SystemCharacter { get; set; }

        /// <summary>
        /// 科室序号
        /// </summary>
        public virtual int? DepartmentOrder { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        [StringLength(32)]
        public virtual string DepartmentName { get; set; }
        /// <summary>
        ///审核人外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        public virtual long? ExamineEmployeeBMId { get; set; }

    }
}