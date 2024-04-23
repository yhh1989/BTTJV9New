using System;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
    /// <summary>
    /// 总检退回
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomerSummBack))]
#endif
    public class TjlCustomerSummBackDto : EntityDto<Guid>
    {
        public virtual Guid? CustomerRegBmId { get; set; }

        /// <summary>
        /// 性别 1男2女3其他
        /// </summary>
        public virtual int? Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public virtual CTbmDepartmentDto Department { get; set; }

        public virtual string DepartmentName { get; set; }

        public virtual Guid? DepartmentId { get; set; }

        /// <summary>
        /// 组合名称
        /// </summary>
        public virtual string ItemGroupName { get; set; }
        
        public virtual Guid? ItemGroupBmId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public virtual string ItemFlag { get; set; }
        
        public virtual Guid? ItemInfoId { get; set; }

        /// <summary>
        /// 第一次项目结果
        /// </summary>
        public virtual string ItemResultChar1 { get; set; }

        /// <summary>
        /// 最后一次结果
        /// </summary>
        public virtual string ItemResultChar2 { get; set; }

        /// <summary>
        /// 第一次退回时间
        /// </summary>
        public virtual DateTime? ReTimes1 { get; set; }

        /// <summary>
        /// 最后一次退回时间
        /// </summary>
        public virtual DateTime? ReTimes2 { get; set; }

        /// <summary>
        /// 第一次退回人
        /// </summary>
        public virtual UserViewDto ReEmp1 { get; set; }

        public virtual long? ReEmp1Id { get; set; }

        /// <summary>
        /// int?
        /// 最后一次退回人
        /// </summary>
        public virtual UserViewDto ReEmp2 { get; set; }

        public virtual long? ReEmp2Id { get; set; }

        /// <summary>
        /// 第一次返回时间
        /// </summary>
        public virtual DateTime? Baktime1 { get; set; }

        /// <summary>
        /// 最后一次返回时间
        /// </summary>
        public virtual DateTime? Baktime2 { get; set; }

        /// <summary>
        /// 第一次返回人
        /// </summary>
        public virtual UserViewDto Bakemp1 { get; set; }

        public virtual long? Bakemp1Id { get; set; }

        /// <summary>
        /// 最后一次返回人
        /// </summary>
        public virtual UserViewDto Bakemp2 { get; set; }

        public virtual long? Bakemp2Id { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public virtual UserViewDto Opemp { get; set; }

        public virtual long? OpempId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public virtual int? State { get; set; }

        /// <summary>
        /// 退回原因
        /// </summary>
        public virtual string ReturnCause { get; set; }
    }
}