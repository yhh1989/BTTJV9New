using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlClientReg))]
#endif
    public class ClientRegLQDto : EntityDto<Guid>
    {
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
        /// 柜子号
        /// </summary>
        [StringLength(32)]
        public virtual string CusCabitBM { get; set; }

        /// <summary>
        /// 存入状态
        /// </summary>        
        public virtual int? CusCabitState { get; set; }

#if Proxy
        [JsonIgnore]
        public string  FormatCusCabitState
        {
            get
            {
                if (CusCabitState.HasValue && CusCabitState == 1)
                    return "存入";
                else
                    return "未存入";
            }
        }
#endif
#if Proxy
        [JsonIgnore]
        public string CZCabitState
        {
            get
            {
                if (CusCabitState.HasValue && CusCabitState == 1)
                    return "取消存入";
                else
                    return "存入";
            }
        }
#endif

        /// <summary>
        /// 存入时间
        /// </summary>        
        public virtual DateTime? CusCabitTime { get; set; }
    }
}
