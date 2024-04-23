using System;
using System.Collections.Generic;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Test
{
    /// <summary>
    /// 测试表1
    /// </summary>
    public class TestTable1 : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 测试表2集合
        /// </summary>
        public virtual ICollection<TestTable2> TestTable2s { get; set; }

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
        /// 列6
        /// </summary>
        public virtual string Column6 { get; set; }
    }
}