#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlClientReg))]
#endif
    public class WXClientRegDto
    {
       /// <summary>
       /// 单位信息
       /// </summary>
        public virtual WXClientInfoDto ClientInfo { get; set; }

        /// <summary>
        /// 单分组信息
        /// </summary>
        public virtual List<WXClientTeamInfoDto> ClientTeamInfo { get; set; }
        

        /// <summary>
        /// 预约编码
        /// </summary>
        public virtual string ClientRegBM { get; set; }

        /// <summary>
        /// 预约次数
        /// </summary>
        public virtual int? ClientRegNum { get; set; }

        /// <summary>
        /// 预约人数
        /// </summary>
        public virtual int? RegPersonCount { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime StartCheckDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime EndCheckDate { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [StringLength(128)]
        public virtual string Remark { get; set; }

        /// <summary>
        /// 单位负责人 默认单位负责人
        /// </summary>
        [StringLength(64)]
        public virtual string linkMan { get; set; } 
        /// <summary>
        /// 封帐状态 1已封帐 2未封帐
        /// </summary>
        public virtual int FZState { get; set; }       

        /// <summary>
        /// 确认状态（财务确认）
        /// </summary>
        public virtual int? ConfirmState { get; set; }


      
       
    }
}
