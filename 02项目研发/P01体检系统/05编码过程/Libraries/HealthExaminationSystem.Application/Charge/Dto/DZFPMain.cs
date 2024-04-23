using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
    /// <summary>
    /// 航天信息江苏电子发票
    /// </summary>
  public  class DZFPMain
    {
        /// <summary>
        /// 请求流水号
        /// </summary>
        public virtual string xsdjbh { get; set; }
        /// <summary>
        /// 销方名称
        /// </summary>
        public virtual string xfmc { get; set; }
        /// <summary>
        /// 销方税号
        /// </summary>
        public virtual string xfsh { get; set; }
        /// <summary>
        /// 销方开户行及银行账号
        /// </summary>
        public virtual string xfyhzh { get; set; }
        /// <summary>
        /// 销方地址电话
        /// </summary>
        public virtual string xfdzdh { get; set; }
        /// <summary>
        /// 分机号
        /// </summary>
        public virtual string fjh { get; set; }
        /// <summary>
        /// 终端号
        /// </summary>
        public virtual string zdh { get; set; }
        /// <summary>
        /// 购方名称
        /// </summary>
        public virtual string gfmc { get; set; }
        /// <summary>
        /// 购方税号
        /// </summary>
        public virtual string gfsh { get; set; }
        /// <summary>
        /// 购方开户行及银行账号
        /// </summary>
        public virtual string gfyhzh { get; set; }
        /// <summary>
        /// 购方地址电话
        /// </summary>
        public virtual string gfdzdh { get; set; }
        /// <summary>
        /// 购方手机
        /// </summary>
        public virtual string gfsj { get; set; }
        /// <summary>
        /// 购方邮箱
        /// </summary>
        public virtual string gfyx { get; set; }
        /// <summary>
        /// 发票种类
        /// </summary>
        public virtual string fpzl { get; set; }
        /// <summary>
        /// 开票类型
        /// </summary>
        public virtual string kplx { get; set; }
        /// <summary>
        /// 蓝票代码
        /// </summary>
        public virtual string lzfpdm { get; set; }
        /// <summary>
        /// 蓝票号码
        /// </summary>
        public virtual string lzfphm { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string bz { get; set; }
        /// <summary>
        /// 开票人
        /// </summary>
        public virtual string kpr { get; set; }
        /// <summary>
        /// 收款人
        /// </summary>
        public virtual string skr { get; set; }
        /// <summary>
        /// 复核人
        /// </summary>
        public virtual string fhr { get; set; }
        /// <summary>
        /// 清单标志
        /// </summary>
        public virtual string qdbz { get; set; }
        /// <summary>
        /// 价税合计
        /// </summary>
        public virtual string jshj { get; set; }
        /// <summary>
        /// 合计不含税金额
        /// </summary>
        public virtual string hjje { get; set; }
        /// <summary>
        /// 合计税额
        /// </summary>
        public virtual string hjse { get; set; }
        /// <summary>
        /// 单据日期
        /// </summary>
        public virtual string djrq { get; set; }
        /// <summary>
        /// 明细
        /// </summary>
        public virtual List<DZFPDetail> listOrder { get; set; }
    }
}
