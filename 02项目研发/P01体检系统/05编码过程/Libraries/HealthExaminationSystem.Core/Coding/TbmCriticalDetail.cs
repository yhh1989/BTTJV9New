using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
    public class TbmCriticalDetail : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 项目标识
        /// </summary>
        [ForeignKey(nameof(CriticalSet))]
        public virtual Guid CriticalSetId { get; set; }

        /// <summary>
        /// 项目
        /// </summary>
        public virtual TbmCriticalSet CriticalSet { get; set; }
        /// <summary>
        /// 并且/或者
        /// </summary>            
        public virtual string relations { get; set; }
        /// <summary>
        /// 项目标识
        /// </summary>
        //[ForeignKey(nameof(ItemInfo))]
        public virtual Guid ItemInfoId { get; set; }

        /// <summary>
        /// 项目
        /// </summary>
        [NotMapped]
        [Obsolete("暂停使用", true)]
        public virtual TbmItemInfo ItemInfo { get; set; }
        /// <summary>
        /// 运算符
        /// </summary>            
        public virtual int Operator { get; set; }

        /// <summary>
        /// 数值结果
        /// </summary>            
        public virtual decimal? ValueNum { get; set; }


        /// <summary>
        /// 包含文字
        /// </summary>            
        [StringLength(640)]
        public virtual string ValueChar { get; set; }
       
        public int TenantId { get; set; }


    }
}
