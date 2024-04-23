using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.HisInterface
{
  public   class TJSQMXDto
    {
        /// <summary>
        /// 申请单号
        /// </summary>
        public string SQDH { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int? XH { get; set; }

        /// <summary>
        /// 费用代码
        /// </summary>
        public string FYDM { get; set; }
        /// <summary>
        /// 费用数量
        /// </summary>
        public int? FYSL { get; set; }
        /// <summary>
        /// 费用金额
        /// </summary>
        public decimal? FYJE { get; set; }
        /// <summary>
        /// 体检项目代码
        /// </summary>
        public string TJXMDM { get; set; }

        /// <summary>
        /// 体检人员工号
        /// </summary>
        public int? TJRYGH { get; set; }

        /// <summary>
        /// 体检科室编号
        /// </summary>
        public string TJKSDM { get; set; }
    }
}
