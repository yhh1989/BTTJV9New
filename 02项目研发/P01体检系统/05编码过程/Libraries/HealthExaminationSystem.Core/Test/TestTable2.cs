using System;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Test
{
    /// <summary>
    /// 测试表2
    /// </summary>
    public class TestTable2 : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 测试表1对象
        /// </summary>
        public virtual TestTable1 TestTable1 { get; set; }

        /// <summary>
        /// 列1
        /// </summary>
        public virtual string Column1 { get; set; }

        /// <summary>
        /// 列2
        /// </summary>
        public virtual string Column2 { get; set; }

        /// <summary>
        /// 列3
        /// </summary>
        public virtual string Column3 { get; set; }

        /// <summary>
        /// 列4
        /// </summary>
        public virtual string Column4 { get; set; }

        /// <summary>
        /// 列5
        /// </summary>
        public virtual string Column5 { get; set; }

        /// <summary>
        /// 6
        /// </summary>
        public virtual string Column6 { get; set; }
    }
}