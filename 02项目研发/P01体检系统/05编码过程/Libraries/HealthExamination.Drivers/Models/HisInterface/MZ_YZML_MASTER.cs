using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExamination.Drivers.Models.HisInterface
{

  public   class MZ_YZML_MASTER
    {
        /// <summary>
        /// HIS名称
        /// </summary>
        public string HISName { get; set; }
        /// <summary>
        /// 主键ID TJ+14位流水号
        /// </summary>
        public string yzid { get; set; }
        /// <summary>
        /// 患者ID
        /// </summary>
        public string blh { get; set; }
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
        /// 项目分类编码 80 体检
        /// </summary>
        public string xmlbbm { get; set; }
        /// <summary>
        /// 申请单号
        /// </summary>
        public string sqdh { get; set; }
        /// <summary>
        /// 执行科室
        /// </summary>
        public string zxksbm { get; set; }
        /// <summary>
        /// 诊断编码
        /// </summary>
        public string zdbm01 { get; set; }
        /// <summary>
        /// 诊断名称
        /// </summary>
        public string zdmc01 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ysyy { get; set; }
        public string bz { get; set; }
        /// <summary>
        /// 类别 M
        /// </summary>
        public string lb { get; set; }
        /// <summary>
        /// 体检
        /// </summary>
        public string cflx { get; set; }
        /// <summary>
        /// 0 未提取 ， 1已提前
        /// </summary>
        public int yzxh { get; set; }
        /// <summary>
        /// 收费标志 0未收 1已收
        /// </summary>
        public string fybz { get; set; }
        /// <summary>
        /// 收费操作员
        /// </summary>
        public string fyczy { get; set; }
        /// <summary>
        /// 收费明细
        /// </summary>
        public  List< MZ_YZML_FY> detail { get; set; }
    }
}
