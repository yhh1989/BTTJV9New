using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public  class ReportDepartDto
    {
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public virtual DateTime? LastTime { get; set; }

        /// <summary>
        /// 编码
        /// </summary>      
        public virtual string DepartmentBM { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
      
        public virtual string Name { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>      
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 五笔码
        /// </summary>       
        public virtual string WBCode { get; set; }

        /// <summary>
        /// 部门类别 检查 检验 功能 放射 彩超 其他 耗材
        /// </summary>       
        public virtual string Category { get; set; }

        /// <summary>
        /// 部门职责
        /// </summary>      
        public virtual string Duty { get; set; }

        /// <summary>
        /// 性别 1男2女3不限
        /// </summary>
        public virtual int? Sex { get; set; }

        /// <summary>
        /// 备注
        /// </summary>       
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 科室序号
        /// </summary>
        public virtual int? OrderNum { get; set; }

        /// <summary>
        /// 最大日检人数
        /// </summary>
        public virtual int? MaxCheckDay { get; set; }

        /// <summary>
        /// 贵宾科室地址
        /// </summary>      
        public virtual string Address { get; set; }

        /// <summary>
        /// 女科室地址
        /// </summary>      
        public virtual string WomenAddress { get; set; }

        /// <summary>
        /// 男科室地址
        /// </summary>       
        public virtual string MenAddress { get; set; }

        /// <summary>
        /// 是否全部1是0否
        /// </summary> 
        public virtual int? State { get; set; }
    }
}
