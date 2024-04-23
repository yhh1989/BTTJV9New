using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExamination.Drivers.Models.YKYInterface
{
    public class YHCustomer
    {
        /// <summary>
        /// ArchivesNum
        /// </summary>       
        public virtual string ArchivesNum { get; set; }
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
        /// 出生日期
        /// </summary>
        public virtual DateTime? Birthday { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>      
        public virtual string IDCard { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>      
        public virtual string CustomerTel { get; set; }
        /// <summary>
        /// 会员卡余额
        /// </summary>      
        public virtual decimal? AvailablePrepaid { get; set; }
        /// <summary>
        /// 会员级别
        /// </summary>      
        public virtual string LevelName { get; set; }
        /// <summary>
        /// 会员卡折扣
        /// </summary>      
        public virtual decimal? RebateRate { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>      
        public virtual string code { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>      
        public virtual string err { get; set; }

    }
}
