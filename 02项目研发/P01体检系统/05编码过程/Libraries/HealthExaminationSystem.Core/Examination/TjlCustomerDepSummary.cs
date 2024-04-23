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
    /// 体检人科室小结
    /// </summary>
    public class TjlCustomerDepSummary : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 体检人预约信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary> 
        [ForeignKey("CustomerReg")]
        public virtual Guid? CustomerRegId { get; set; }
        /// <summary>
        /// 体检人预约登记信息表
        /// </summary>
        public virtual TjlCustomerReg CustomerReg { get; set; }

        /// <summary>
        /// 科室信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey("DepartmentBM")]
        public virtual Guid? DepartmentBMId { get; set; }
        /// <summary>
        /// 科室ID
        /// </summary>
        public virtual TbmDepartment DepartmentBM { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        [StringLength(32)]
        public virtual string DepartmentName { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        [StringLength(32)]
        public virtual string DepartmentCodeBM { get; set; }
        /// <summary>
        /// 科室序号
        /// </summary>
        public virtual int? DepartmentOrder { get; set; }

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
        /// 原生科室小结 
        /// </summary>
        [StringLength(3072)]
        public virtual string  OriginalDiag { get; set; }

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
        ///审核人外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey("ExamineEmployeeBM")]
        public virtual long? ExamineEmployeeBMId { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public virtual User ExamineEmployeeBM { get; set; }

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}