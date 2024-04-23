using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease.Dto
{
  public   class CHECK_RESULTDto
    {
        /// <summary>
        /// 职 业 病 危 害因素代码
        /// </summary>
        public string FACTOR_CODE { get; set; }
        /// <summary>
        /// 工 作 场 所 名称
        /// </summary>
        public string WORKPLACE_NAME { get; set; }
        /// <summary>
        ///岗位/工种名称
        /// </summary>
        public string POSITION_NAME { get; set; }
        /// <summary>
        ///浓度/强度类型代码
        /// </summary>
        public string DENSITY_TYPE_CODE { get; set; }
        /// <summary>
        /// 检测日期
        /// </summary>
        public string CHECK_DATE { get; set; }
        /// <summary>
        /// 合格情况
        /// </summary>
        public string IS_STANDARD { get; set; }
        /// <summary>
        /// 检 测 结 果 最小值
        /// </summary>
        public string CHECK_RESULT_MIN { get; set; }
        /// <summary>
        /// 检 测 结 果 最大值
        /// </summary>
        public string CHECK_RESULT_MAX { get; set; }
      
    }
}
