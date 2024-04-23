#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlClientInfo))]
#endif
  public   class ClientInfoSimpDto
    { /// <summary>
      /// 单位编码
      /// </summary>
        public virtual string ClientBM { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }       

        /// <summary>
        /// 企业负责人
        /// </summary>
        public virtual string LinkMan { get; set; }

        /// <summary>
        /// 服务专员
        /// </summary>
        public virtual SearchUserDto user { get; set; }
    }
}
