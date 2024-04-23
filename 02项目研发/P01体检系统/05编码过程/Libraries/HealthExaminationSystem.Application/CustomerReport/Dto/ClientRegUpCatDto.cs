using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto
{
    /// <summary>
    /// 查询客户预约信息Dto
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlClientReg))]
#endif
    public class ClientRegUpCatDto : EntityDto<Guid>
    {
        /// <summary>
        /// 柜子号
        /// </summary>
        [StringLength(32)]
        public virtual string CusCabitBM { get; set; }

        /// <summary>
        /// 存入状态
        /// </summary>        
        public virtual int? CusCabitState { get; set; }

        /// <summary>
        /// 存入时间
        /// </summary>        
        public virtual DateTime? CusCabitTime { get; set; }
    }
}
