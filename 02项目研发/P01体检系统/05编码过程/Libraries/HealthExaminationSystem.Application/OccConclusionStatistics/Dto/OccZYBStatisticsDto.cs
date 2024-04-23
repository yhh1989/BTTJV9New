using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccConclusionStatistics.Dto
{
 public    class OccZYBStatisticsDto
    {

        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientNamed { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string Name { get; set; }


        /// <summary>
        /// 性别
        /// </summary>
        public virtual string Sex { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }
        /// <summary>
        /// 接害工龄
        /// </summary>
        public virtual string InjurAge { get; set; }
        /// <summary>
        /// 危害因素名称
        /// </summary>
        public virtual string RiskName { get; set; }

        /// <summary>
        /// 体检类别
        /// </summary>
        public virtual string PostName { get; set; }
        /// <summary>
        /// 职业病名称
        /// </summary>
        public virtual string ZYBName { get; set; }
        /// <summary>
        /// 报告书编号
        /// </summary>
        public virtual string ReportBM { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }

    }
}
