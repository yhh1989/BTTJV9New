using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
  public   class WXGroupPriceSynchronizesDto
    {
        /// <summary>
        /// 业务ID
        /// </summary>    
        [StringLength(64)]
        public virtual string ItemGroupBM { get; set; }

        /// <summary>
        /// 项目组合名称
        /// </summary>
        [Required]
        [StringLength(64)]
        public virtual string ItemGroupName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public virtual decimal? Count { get; set; }


        /// <summary>
        /// 总价格
        /// </summary>
        public virtual decimal? chkit_costn { get; set; }


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
        /// 最后修改时间
        /// </summary>

        public virtual DateTime? LastDate { get; set; }
    }
}
