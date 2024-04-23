using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
    public class ReportItemInfoDto
    {
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public virtual DateTime? LastTime { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>    
        public virtual string DepartmentBM { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [Required]       
        public virtual string Name { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        [Required]       
        public virtual string ItemBM { get; set; }

        /// <summary>
        /// 项目打印名称
        /// </summary>
        [Required]   
        public virtual string NamePM { get; set; }

        /// <summary>
        /// 项目英文名称
        /// </summary>
      
        public virtual string NameEngAbr { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>     
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 五笔码
        /// </summary>     
        public virtual string WBCode { get; set; }

        /// <summary>
        /// 性别 1男2女3不限
        /// </summary>
        public virtual int? Sex { get; set; }

        /// <summary>
        /// 最小年龄
        /// </summary>
        public virtual int? MinAge { get; set; }

        /// <summary>
        /// 最大年龄
        /// </summary>
        public virtual int? MaxAge { get; set; }

        /// <summary>
        /// 单位
        /// </summary>       
        public virtual string Unit { get; set; }

        /// <summary>
        /// 注意事项
        /// </summary>    
        public virtual string Notice { get; set; }

        /// <summary>
        /// 项目说明
        /// </summary>     
        public virtual string Remark { get; set; }

        /// <summary>
        /// 项目序号
        /// </summary>
        public virtual int? OrderNum { get; set; }

        /// <summary>
        /// 项目类别 1数值型2计算型3说明型4阴阳性
        /// </summary>
        public virtual int? moneyType { get; set; }

        /// <summary>
        /// 临床类别
        /// </summary>
        public virtual int? Lclxid { get; set; }

        /// <summary>
        /// 是否在报告打印 1否2打印
        /// </summary>
        public virtual int? Ckisrpot { get; set; }

        /// <summary>
        /// 报告代码
        /// </summary>
        public virtual string ReportCode { get; set; }

        /// <summary>
        /// HIS代码
        /// </summary>  
        public virtual string HISCode { get; set; }

        /// <summary>
        /// 标准编码
        /// </summary>
     
        public virtual string StandardCode { get; set; }
        /// <summary>
        /// 是否全部1是0否
        /// </summary> 
        public virtual int? State { get; set; }

    }
}
