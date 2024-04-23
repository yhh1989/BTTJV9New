using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExamination.Drivers.Models.HisInterface
{ 
  public   class MZ_YZML_FY
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string yzid { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        public string mzh { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        public string ksbm { get; set; }
        /// <summary>
        /// 医生编码
        /// </summary>
        public string ysbm { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime rq { get; set; }
        /// <summary>
        /// 病人ID
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// 主表ID
        /// </summary>
        public string cfh { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int xh { get; set; }
        /// <summary>
        /// 子序号
        /// </summary>
        public int xh_id { get; set; }
        /// <summary>
        /// 项目编码
        /// </summary>
        public string yzbm { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string yzmc { get; set; }
        /// <summary>
        /// 项目规格
        /// </summary>
        public string gg { get; set; }
        /// <summary>
        /// 产地
        /// </summary>
        public string ypcd { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string dw { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal sl { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal jg { get; set; }
        /// <summary>
        /// 默认 1
        /// </summary>
        public decimal dcsl { get; set; }
        /// <summary>
        /// 默认  0
        /// </summary>
        public string zbbz { get; set; }
        /// <summary>
        /// 收费标志  0 未收费，1已收费
        /// </summary>
        public string sfbz { get; set; }
        /// <summary>
        /// 默认 1
        /// </summary>
        public int ts { get; set; }
        /// <summary>
        /// 默认 1
        /// </summary>
        public int jl { get; set; }
        /// <summary>
        /// 默认 1
        /// </summary>
        public int cs { get; set; }
        /// <summary>
        /// 默认 -1
        /// </summary>
        public string yzzh { get; set; }
        /// <summary>
        /// 默认  0
        /// </summary>
        public string psbz { get; set; }
        /// <summary>
        /// 默认  0
        /// </summary>
        public string fybz { get; set; }
        /// <summary>
        /// 票据流水号 收费生成
        /// </summary>
        public string pjlsh { get; set; }
        /// <summary>
        /// 收费项目编码
        /// </summary>
        public string sfxmbm { get; set; }
        /// <summary>
        /// 统计项目编码
        /// </summary>
        public string tjxmbm { get; set; }
        /// <summary>
        /// 默认 80 体检
        /// </summary>
        public string xmlbbm { get; set; }
        /// <summary>
        /// 默认 0
        /// </summary>
        public string djbz { get; set; }
        /// <summary>
        /// 体检中心的科室编码
        /// </summary>
        public string zxksbm { get; set; }
        /// <summary>
        /// 默认 0
        /// </summary>
        public string tfbz { get; set; }
        /// <summary>
        /// 默认 0
        /// </summary>
        public string dfbz { get; set; }
        /// <summary>
        /// 默认 8
        /// </summary>
        public string sjly { get; set; }
        /// <summary>
        /// 收费单号：TJ+10流水号（一个主表ID，一个收费单号）
        /// </summary>
        public string sfdh { get; set; }
    }
}
