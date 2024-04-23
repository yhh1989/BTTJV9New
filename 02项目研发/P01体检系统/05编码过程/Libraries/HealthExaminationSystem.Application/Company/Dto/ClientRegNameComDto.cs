using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
   public  class ClientRegNameComDto : EntityDto<Guid>
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [StringLength(24)]
        public virtual string ClientRegBM { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime StartCheckDate { get; set; }

        /// <summary>
        /// 单位编码
        /// </summary>
        [StringLength(24)]
        public virtual string ClientBM { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        [StringLength(256)]    
        public virtual string ClientName { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(128)]
        public virtual string HelpCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }
        
    }
}
