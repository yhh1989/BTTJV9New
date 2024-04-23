using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.FrmMakeCollect.Dto
{
  public   class OutDepartCusDto : EntityDto<Guid>
    {
        /// <summary>
        /// 科室人数
        /// </summary>
        public int? departCount { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string deparTname { get; set; }
        /// <summary>
        /// 组合名称
        /// </summary>
        public string groupName { get; set; }
        /// <summary>
        /// 组合数量
        /// </summary>
        public int? groupCoutn { get; set; }

        /// <summary>
        /// 科室数量
        /// </summary>
        public string deparTnameCount { get; set; }
        /// <summary>
        /// 科室ID
        /// </summary>
        public Guid? DepartId { get; set; }

        /// <summary>
        /// 组合Id
        /// </summary>
        public Guid? GroupId { get; set; }

    }
}
