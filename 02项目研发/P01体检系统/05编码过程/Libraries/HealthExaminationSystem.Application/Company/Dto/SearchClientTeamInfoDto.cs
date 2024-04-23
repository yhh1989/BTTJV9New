using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
    public class SearchClientTeamInfoDto: EntityDto<Guid>
    {
        /// <summary>
        /// 单位预约ID
        /// </summary>
        public Guid? ClientRegId { get; set; }

        /// <summary>
        /// 分组ID
        /// </summary>
        public virtual int? TeamBM { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }

        /// <summary>
        /// 结婚状态
        /// </summary>
        public virtual int? MaritalStatus { get; set; }

        /// <summary>
        /// 是否备孕 1备孕2不备孕
        /// </summary>
        public virtual int? ConceiveStatus { get; set; }
    }
}
