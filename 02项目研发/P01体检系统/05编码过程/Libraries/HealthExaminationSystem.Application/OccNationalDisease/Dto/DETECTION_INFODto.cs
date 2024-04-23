using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease.Dto
{
   public  class DETECTION_INFODto
    {
        /// <summary>
        /// 用 人 单 位
        /// </summary>
        public ENTERPRISE_INFO1Dto ENTERPRISE_INFO { get; set; }
        /// <summary>
        /// 职工总数
        /// </summary>
        public string ALL_WORKER_SUM { get; set; }
        /// <summary>
        /// 用 工 单 位 名称
        /// </summary>
        public string ENTERPRISE_NAME_EMPLOYER { get; set; }
        /// <summary>
        /// 用 工 单 位 统一 社 会 信 用代码
        /// </summary>
        public string CREDIT_CODE_EMPLOYER { get; set; }
        /// <summary>
        /// 当 年 接 触 职业 性 有 害 因素作业人数
        /// </summary>
        public string EXPOSE_HAZARD_WORKER_SUM { get; set; }
        /// <summary>
        /// 当 年 接 触 粉尘人数
        /// </summary>
        public string EXPOSE_HAZARD_WORKER_SUM_DUST { get; set; }
        /// <summary>
        /// 当 年 岗 前 应检人数
        /// </summary>
        public string CURRENT_YEAR_CHECK_SUM_DUST { get; set; }
        /// <summary>
        /// 当 年 在 岗 应检人数
        /// </summary>
        public string IN_CURRENT_YEAR_CHECK_SUM_DUST{ get; set; }
        /// <summary>
        /// 当 年 离 岗 应检人数
        /// </summary>
        public string LEAVE_CURRENT_YEAR_CHECK_SUM_CHEMISTRY { get; set; }
        /// <summary>
        /// 当 年 接 触 噪声人数
        /// </summary>
        public string EXPOSE_HAZARD_WORKER_SUM_NOISE { get; set; }
        /// <summary>
        /// 当 年 岗 前 应检人数
        /// </summary>
        public string CURRENT_YEAR_CHECK_SUM_NOISE { get; set; }
        /// <summary>
        /// 当 年 在 岗 应检人数
        /// </summary>
        public string IN_CURRENT_YEAR_CHECK_SUM_NOISE { get; set; }
        /// <summary>
        /// 当 年 离 岗 应检人数
        /// </summary>
        public string LEAVE_CURRENT_YEAR_CHECK_SUM_NOISE { get; set; }
        /// <summary>
        ///当 年 接 触 其        他 物 理 因 素        人数
        /// </summary>
        public string EXPOSE_HAZARD_WORKER_SUM_PHYSICS { get; set; }
        /// <summary>
        /// 当 年 岗 前 应        检人数
        /// </summary>
        public string CURRENT_YEAR_CHECK_SUM_PHYSICS { get; set; }
        /// <summary>
        /// 当 年 在 岗 应检人数
        /// </summary>
        public string IN_CURRENT_YEAR_CHECK_SUM_PHYSICS { get; set; }
        /// <summary>
        /// 当 年 离 岗 应检人数
        /// </summary>
        public string LEAVE_CURRENT_YEAR_CHECK_SUM_PHYSICS { get; set; }
        /// <summary>
        /// 当 年 接 触 生物因素人数
        /// </summary>
        public string EXPOSE_HAZARD_WORKER_SUM_BIOLOGY { get; set; }
        /// <summary>
        /// 当 年 岗 前 应检人数
        /// </summary>
        public string CURRENT_YEAR_CHECK_SUM_BIOLOGY { get; set; }
        /// <summary>
        /// 当 年 在 岗 应检人数
        /// </summary>
        public string IN_CURRENT_YEAR_CHECK_SUM_BIOLOGY { get; set; }
        /// <summary>
        /// 当 年 离 岗 应检人数
        /// </summary>
        public string LEAVE_CURRENT_YEAR_CHECK_SUM_BIOLOGY { get; set; }
      
        public CHECK_RESULT_LISTDto CHECK_RESULT_LIST { get; set; }
        /// <summary>
        /// 检 测 单 位 名称
        /// </summary>
        public string CHECK_ORGAN_NAME { get; set; }
        /// <summary>
        /// 检 测 单 位 负责人
        /// </summary>
        public string CHECK_ORGAN_PERSON { get; set; }
        /// <summary>
        /// 填表人姓名
        /// </summary>
        public string WRITE_PERSON { get; set; }
        /// <summary>
        /// 填 表 人 联 系电话
        /// </summary>
        public string WRITE_PERSON_TELPHONE { get; set; }
        /// <summary>
        /// 填表日期
        /// </summary>
        public string WRITE_DATE { get; set; }
        /// <summary>
        /// 填 表 单 位 名称
        /// </summary>
        public string WRITE_UNIT { get; set; }
        /// <summary>
        /// 报告人姓名
        /// </summary>
        public string REPORT_PERSON { get; set; }
        /// <summary>
        /// 报 告 人 联 系电话
        /// </summary>
        public string REPORT_PERSON_TEL { get; set; }
        /// <summary>
        /// 报告日期
        /// </summary>
        public string REPORT_DATE { get; set; }
        /// <summary>
        /// 报 告 单 位 名称
        /// </summary>
        public string REPORT_UNIT { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string REMARK { get; set; }
        /// <summary>
        /// 创 建 地 区 代码
        /// </summary>
        public string AREA_CODE { get; set; }
        /// <summary>
        /// 创 建 机 构 代码
        /// </summary>
        public string ORG_CODE { get; set; }
        /// <summary>
        /// 审核
        /// </summary>
        public AUDIT_INFODto AUDIT_INFO { get; set; }

    }
}
