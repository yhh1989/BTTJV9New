using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExamination.Drivers.Models.HisInterface
{
    /// <summary>
    /// 物价同步
    /// </summary>
  public  class HisPriceDto
    {
        /// <summary>
        /// 编号
        /// </summary>       
        public virtual string chkit_id { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public virtual string chkit_name { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public virtual string chkit_fmt { get; set; }
        /// <summary>
        /// 助记符
        /// </summary>
        public virtual string chkit_id2 { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public virtual string aut_name { get; set; }
        /// <summary>
        /// 科室代码
        /// </summary>
        public virtual string ut_id { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public virtual decimal? chkit_costn { get; set; }
        /// <summary>
        /// 最近修改时间
        /// </summary>
        public virtual DateTime? upd_date { get; set; }

        /// <summary>
        /// 项目类别
        /// </summary>    
        public string chkit_Type { get; set; }
        /// <summary>
        /// His厂家
        /// </summary>
        public string HISName { get; set; }

    }
}
