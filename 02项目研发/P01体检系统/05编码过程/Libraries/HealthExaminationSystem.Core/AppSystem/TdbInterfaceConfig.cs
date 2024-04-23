using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.AppSystem
{
    /// <summary>
    /// 接口设置
    /// </summary>
    [Obsolete("暂停使用", true)]
    public class TdbInterfaceConfig : AuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 接口名称
        /// </summary>
        [MaxLength(64)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 科室ID:拼接
        /// </summary>
        [MaxLength(1024)]
        public virtual string DepartmentBM { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public virtual int DataBaseType { get; set; }

        /// <summary>
        /// 服务器地址
        /// </summary>
        [MaxLength(128)]
        public virtual string ServerIp { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        [MaxLength(64)]
        public virtual string DataBaseName { get; set; }

        /// <summary>
        /// 是否启用Windows验证
        /// </summary>
        public virtual bool WindowsCheck { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(64)]
        public virtual string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(64)]
        public virtual string Password { get; set; }

        /// <summary>
        /// SQL语句1
        /// </summary>
        [MaxLength(5120)]
        public virtual string RegSql1 { get; set; }

        /// <summary>
        /// SQL语句2
        /// </summary>
        [MaxLength(5120)]
        public virtual string RegSql2 { get; set; }

        /// <summary>
        /// SQL语句3
        /// </summary>
        [MaxLength(5120)]
        public virtual string RegSql3 { get; set; }

        /// <summary>
        /// SQL语句4
        /// </summary>
        [MaxLength(5120)]
        public virtual string RegSql4 { get; set; }

        /// <summary>
        /// 获取结果
        /// </summary>
        [MaxLength(5120)]
        public virtual string getSql { get; set; }

        /// <summary>
        /// 是否启用图片路径
        /// </summary>
        public virtual bool ImageColumnIsPath { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}