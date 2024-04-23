#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlClientInfo))]
#endif
  public   class ChargeClientDto
    {      
      

        /// <summary>
        /// 单位预约信息
        /// </summary>
        public virtual ICollection<ClientRegDto> ClientReg { get; set; }

        /// <summary>
        /// 单位编码
        /// </summary>
        public virtual string ClientBM { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        [StringLength(256)]
        [Required]
        public virtual string ClientName { get; set; }  
    }
}
