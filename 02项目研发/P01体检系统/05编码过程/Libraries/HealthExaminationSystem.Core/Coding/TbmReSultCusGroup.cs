using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
    public class TbmReSultCusGroup : AuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 科室标识
        /// </summary>
        [ForeignKey("ReSultSet")]
        public virtual Guid ReSultSetId { get; set; }


        /// <summary>
        /// 科室表
        /// </summary>
        public virtual TbmReSultSet ReSultSet { get; set; }

        /// <summary>
        /// 组合标识
        /// </summary>
        [ForeignKey("ItemGroup")]
        public virtual Guid? ItemGroupId { get; set; }

        public string ItemGroupName { get; set; }

        /// <summary>
        /// 科室表
        /// </summary>
        public virtual TbmItemGroup ItemGroup { get; set; }
        public int TenantId { get; set; }
        //public int TenantId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
