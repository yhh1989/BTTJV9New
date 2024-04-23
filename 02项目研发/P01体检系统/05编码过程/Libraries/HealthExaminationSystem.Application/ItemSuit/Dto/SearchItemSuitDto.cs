using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto
{
    public class SearchItemSuitDto: EntityDto<Guid>
    { 

        /// <summary>
        /// 套餐名称
        /// </summary>
        [StringLength(64)]
        public virtual string QueryText { get; set; }
        
        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }

        /// <summary>
        /// 最小年龄
        /// </summary>
        public virtual int? MinAge { get; set; }

        /// <summary>
        /// 最大年龄
        /// </summary>
        public virtual int? MaxAge { get; set; }
        
        /// <summary>
        /// 体检类别字典 -1ALL体检类别字典
        /// </summary>
        public virtual int? ExaminationType { get; set; }
        
        /// <summary>
        /// 1基础套餐2组单3加项
        /// </summary>
        public virtual int? ItemSuitType { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual int? Available { get; set; }

    }
}
