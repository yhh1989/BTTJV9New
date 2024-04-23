using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 科室建议记录
    /// </summary>
    [Obsolete("暂停使用", true)]
#if !Proxy
    [AutoMap(typeof(TjlCustomerSummary))]
#endif

    public class SearchTjlCustomerSummaryDto : EntityDto<Guid>
    {
        /// <summary>
        /// 预约id
        /// </summary>
        public virtual Guid? CustomerRegBMId { get; set; }

        /// <summary>
        /// 科室ID
        /// </summary>
        public virtual Guid? DepartmentBMId { get; set; }

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
        /// 专科建议
        /// </summary>
        [StringLength(3072)]
        public virtual string CharacterSummary { get; set; }

        /// <summary>
        /// 体检日期
        /// </summary>
        public virtual DateTime? CheckDate { get; set; }
    }
}
