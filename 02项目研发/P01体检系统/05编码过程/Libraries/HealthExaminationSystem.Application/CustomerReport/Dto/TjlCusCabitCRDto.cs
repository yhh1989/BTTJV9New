using Abp.Application.Services.Dto;
using Newtonsoft.Json;
#if !Proxy
using Abp.AutoMapper;
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
#if !Proxy
    [AutoMap(typeof(TjlCusCabit))]
#endif

    public class TjlCusCabitCRDto : EntityDto<Guid>
    {

        /// <summary>
        /// 体检号
        /// </summary>      
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 体检人
        /// </summary>
        public virtual string CustomerName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public virtual string Mobile { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }
        
        /// <summary>
        /// 柜子号
        /// </summary>
        public string CabitName { get; set; }
        /// <summary>
        /// 取出状态1存入2取出
        /// </summary>
        public int? GetState { get; set; }

        /// <summary>
        /// 报告导出状态 1未导出2已导出
        /// </summary>

#if Proxy
        [JsonIgnore]
        public string  FormatCusCabitState
        {
            get
            {
                if (GetState.HasValue && GetState == 2)
                    return "已领取";
                else
                    return "待领取";
            }
        }
#endif
        /// <summary>
        /// 报告导出状态 1未导出2已导出
        /// </summary>

#if Proxy
        [JsonIgnore]
        public string  FormatCZCabitState
        {
            get
            {
                if (GetState.HasValue && GetState == 2)
                    return "";
                else
                    return "领取";
            }
        }
#endif

        /// <summary>
        /// 交接时间
        /// </summary>
        public virtual DateTime? HandoverTime { get; set; }

      

        /// <summary>
        /// 发单时间
        /// </summary>
        public virtual DateTime? SendTime { get; set; }

        /// <summary>
        /// 领取人
        /// </summary>
        [StringLength(32)]
        public virtual string Receiver { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(256)]
        public virtual string Remarks { get; set; }
    }
}
