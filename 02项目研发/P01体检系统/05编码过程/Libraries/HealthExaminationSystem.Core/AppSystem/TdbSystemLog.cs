using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.AppSystem
{
    /// <summary>
    /// 系统日志表
    /// </summary>
    [Obsolete("暂停使用", true)]
    public class TdbSystemLog : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// ID
        /// </summary>
        public virtual Guid LogIndex { get; set; }

        /// <summary>
        /// 操作id
        /// </summary>
        public virtual Guid EmployeeId { get; set; }

        /// <summary>
        /// 操作名称
        /// </summary>
        [MaxLength(64)]
        public virtual string Logname { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public virtual DateTime LogTime { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [MaxLength(64)]
        public virtual string Ip { get; set; }

        /// <summary>
        /// 操作对象：姓名、单位名称、科室名称
        /// </summary>
        [MaxLength(64)]
        public virtual string ObjectName { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        [MaxLength(1024)]
        public virtual string LogText { get; set; }

        /// <summary>
        /// 档案号
        /// </summary>
        public virtual Guid ArchivesNum { get; set; }

        /// <summary>
        /// 日志分类：系统错误、系统设置、单位操作、用户登记、科室录入、总检录入、报告打印、用户接口、体检管理、收费类别
        /// </summary>
        public virtual int Logtype { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}