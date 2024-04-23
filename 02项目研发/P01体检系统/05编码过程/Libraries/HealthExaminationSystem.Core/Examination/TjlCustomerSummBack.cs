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
    /// 总检退回
    /// </summary>
    public class TjlCustomerSummBack : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 体检人预约
        /// </summary>
        public virtual TjlCustomerReg CustomerRegBM { get; set; }

        /// <summary>
        /// 体检人预约标识
        /// </summary>
        [ForeignKey(nameof(CustomerRegBM))]
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
        /// 科室
        /// </summary>
        public virtual TbmDepartment Department { get; set; }

        /// <summary>
        /// 科室标识
        /// </summary>
        [ForeignKey(nameof(Department))]
        public virtual Guid? DepartmentId { get; set; }

        /// <summary>
        /// 组合名称
        /// </summary>
        public virtual string ItemGroupName { get; set; }

        /// <summary>
        /// 项目组合
        /// </summary>
        public virtual TbmItemGroup ItemGroupBM { get; set; }

        /// <summary>
        /// 项目组合标识
        /// </summary>
        [ForeignKey(nameof(ItemGroupBM))]
        public virtual Guid? ItemGroupBmId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public virtual string ItemFlag { get; set; }

        /// <summary>
        /// 项目
        /// </summary>
        public virtual TbmItemInfo ItemInfo { get; set; }

        /// <summary>
        /// 项目标识
        /// </summary>
        [ForeignKey(nameof(ItemInfo))]
        public virtual Guid? ItemInfoId { get; set; }

        /// <summary>
        /// 第一次项目结果
        /// </summary>
        [MaxLength(3072)]
        public virtual string ItemResultChar1 { get; set; }

        /// <summary>
        /// 最后一次结果
        /// </summary>
        [MaxLength(3072)]
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
        public virtual User ReEmp1 { get; set; }

        /// <summary>
        /// 第一次退回人标识
        /// </summary>
        [ForeignKey(nameof(ReEmp1))]
        public virtual long? ReEmp1Id { get; set; }

        /// <summary>
        /// 最后一次退回人
        /// </summary>
        public virtual User ReEmp2 { get; set; }

        /// <summary>
        /// 最后一次退回人标识
        /// </summary>
        [ForeignKey(nameof(ReEmp2))]
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
        public virtual User Bakemp1 { get; set; }

        /// <summary>
        /// 第一次返回人标识
        /// </summary>
        [ForeignKey(nameof(Bakemp1))]
        public virtual long? Bakemp1Id { get; set; }

        /// <summary>
        /// 最后一次返回人
        /// </summary>
        public virtual User Bakemp2 { get; set; }

        /// <summary>
        /// 最后一次返回人标识
        /// </summary>
        [ForeignKey(nameof(Bakemp2))]
        public virtual long? Bakemp2Id { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public virtual User Opemp { get; set; }

        /// <summary>
        /// 操作人标识
        /// </summary>
        [ForeignKey(nameof(Opemp))]
        public virtual long? OpempId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public virtual int? State { get; set; }
        
        /// <summary>
        /// 退回原因
        /// </summary>
        public virtual string ReturnCause { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}