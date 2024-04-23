using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
    /// <summary>
    /// 航天信息江苏电子发票明细
    /// </summary>
    public class DZFPDetail
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public virtual string spmc { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        public virtual string ggxh { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        public virtual string jldw { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public virtual string sl { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public virtual string dj { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public virtual string je { get; set; }
        /// <summary>
        /// 发票行性质
        /// </summary>
        public virtual string fphxz { get; set; }
        /// <summary>
        /// 含税标志
        /// </summary>
        public virtual string hsjbz { get; set; }
        /// <summary>
        /// 税率
        /// </summary>
        public virtual string slv { get; set; }
        /// <summary>
        /// 税额
        /// </summary>
        public virtual string se { get; set; }
        /// <summary>
        /// 税收分类编码
        /// </summary>
        public virtual string flbm { get; set; }
        /// <summary>
        /// 企业自行编码
        /// </summary>
        public virtual string zxbm { get; set; }
        /// <summary>
        /// 优惠政策标志
        /// </summary>
        public virtual string yhbz { get; set; }
        /// <summary>
        /// 零税率标志
        /// </summary>
        public virtual string lslvbs { get; set; }
        /// <summary>
        /// 优惠政策说明
        /// </summary>
        public virtual string yhsm { get; set; }
        /// <summary>
        /// 扣除额
        /// </summary>
        public virtual string kce { get; set; }
       
    }
}
