using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
    /// <summary>
    /// 预约查询条件
    /// </summary>
    public class ClientRegSelectDto : EntityDto<Guid>
    {
        /// <summary>
        /// 预约编码
        /// </summary>
        public string ClientRegBM { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartCheckDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndCheckDate { get; set; }
        /// <summary>
        /// 封帐状态 1已封帐 2未封帐
        /// </summary>
        public int? FZState { get; set; }

        /// <summary>
        /// 锁定状态 1锁定2未锁定
        /// </summary>
        public int? SDState { get; set; }

        /// <summary>
        /// 单位状态 1.正常2.散检单位
        /// </summary>
        public int? ClientSate { get; set; }

        /// <summary>
        /// 单位状态：1.已完成；2未完成
        /// </summary>
        public int? ClientCheckSate { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public Guid? ClientInfo_Id { get; set; }
    }
}
