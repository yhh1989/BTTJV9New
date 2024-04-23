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
    /// <summary>
    /// 医嘱项目表
    /// </summary>
   public class TbmPriceSynchronize : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 组合集合
        /// </summary>
        [NotMapped]
        [Obsolete("暂停使用", true)]
        public virtual ICollection<TbmItemGroup> ItemGroups { get; set; }  

        /// <summary>
        /// 编号
        /// </summary>  
        [StringLength(64)]
        public string chkit_id { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [StringLength(64)]
        public string chkit_name { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        [StringLength(128)]
        public string chkit_fmt { get; set; }

        /// <summary>
        /// 助记符
        /// </summary>
        [StringLength(64)]
        public string chkit_id2 { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [StringLength(64)]
        public string aut_name { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        [StringLength(64)]
        public string ut_id { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        [NotMapped]
        [Obsolete("暂停使用", true)]
        public DateTime? chkit_cost { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal? chkit_costn { get; set; }
        /// <summary>
        /// 最近修改时间
        /// </summary>
        public DateTime? upd_date { get; set; }

        /// <summary>
        /// C  Q   M   S  分别代表  项目  材料   药品   申请单组合
        /// </summary>
        [StringLength(64)]
        public string chkit_Type { get; set; }


        /// <inheritdoc />
        public int TenantId { get; set; }


    }
}
