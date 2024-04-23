using System;
using System.ComponentModel.DataAnnotations;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Users.Dto
{
    /// <summary>
    /// 预约用的角色Dto
    /// </summary>
#if !Proxy
    [AutoMap(typeof(User))]
#endif
    public class UserRegsDto
    {
        /// <summary>
        /// 工号
        /// </summary>
        [StringLength(32)]
        public virtual string EmployeeNum { get; set; }

        /// <summary>
        /// Windows Doamin 域用户帐号, 多个帐号用逗号分开
        /// </summary>
        [StringLength(64)]
        public virtual string DomainName { get; set; }

        /// <summary>
        /// Novell 网用户帐号,多个帐号用逗号分
        /// </summary>
        [StringLength(64)]
        public virtual string NovellAccount { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(32)]
        public virtual string EmployeeName { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(32)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 性别 1男2女3未知
        /// </summary>
        public virtual int? Sex { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public virtual DateTime? Birthday { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        [StringLength(32)]
        public virtual string Mobile { get; set; }

        /// <summary>
        /// 联系地址
        /// </summary>
        [StringLength(32)]
        public virtual string Address { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public virtual string Sign { get; set; }

        /// <summary>
        /// 职位状态 1在岗2离岗
        /// </summary>
        public virtual int? State { get; set; }

        /// <summary>
        /// 员工级别 字典
        /// </summary>
        public virtual int? Degree { get; set; }

        /// <summary>
        /// 员工职能
        /// </summary>
        [StringLength(32)]
        public virtual string Duty { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(32)]
        public virtual string Pwd { get; set; }

        /// <summary>
        /// 是否有收费权限
        /// </summary>
        public virtual bool CanPayment { get; set; }

        /// <summary>
        /// 医生头像
        /// </summary>
        public virtual string EmPhoto { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        [StringLength(64)]
        public virtual string QQ { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        [StringLength(64)]
        public virtual string Email { get; set; }

        /// <summary>
        /// 工作状态
        /// </summary>
        public virtual int WorkState { get; set; }

        /// <summary>
        /// 最后退出系统时间
        /// </summary>
        public virtual DateTime? LastClearTime { get; set; }

        /// <summary>
        /// 是否在线   1/2
        /// </summary>
        public virtual bool OnlineState { get; set; }

        /// <summary>
        /// 折扣范围
        /// </summary>
        [StringLength(64)]
        public virtual string Discount { get; set; }

        /// <summary>
        /// 劳务标准
        /// </summary>
        [StringLength(128)]
        public virtual string ServiceStandard { get; set; }

        /// <summary>
        /// 职称
        /// </summary>
        [StringLength(256)]
        public virtual string Title { get; set; }

        /// <summary>
        /// 临床科室
        /// </summary>
        [StringLength(256)]
        public virtual string ClinicalDepartment { get; set; }

        /// <summary>
        /// 体检分组 -内务信息组
        /// </summary>
        [StringLength(256)]
        public virtual string GroupName { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 姓氏
        /// </summary>
        public virtual string Surname { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        public virtual string EmailAddress { get; set; }

        /// <summary>
        /// 全名
        /// </summary>
        public virtual string FullName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Password { get; set; }

        [StringLength(32)]
        public virtual string PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        /// <summary>
        /// 租户标识
        /// </summary>
        public virtual string TenantId { get; set; }
    }
}