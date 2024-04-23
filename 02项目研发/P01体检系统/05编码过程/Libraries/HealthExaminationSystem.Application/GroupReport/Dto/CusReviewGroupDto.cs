using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto
{
  public  class CusReviewGroupDto
    {

        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }
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
        /// 岁
        /// </summary>
        [StringLength(2)]
        public virtual string AgeUnit { get; set; }
        /// <summary>
        /// 移动电话
        /// </summary>
        [StringLength(16)]
        public virtual string Mobile { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public virtual string Department { get; set; }
        /// <summary>
        /// 工种
        /// </summary>
        [StringLength(32)]
        public virtual string TypeWork { get; set; }
        /// <summary>
        /// 危害因素 逗号隔开
        /// </summary>
        [StringLength(800)]
        public virtual string RiskS { get; set; }

        /// <summary>
        /// 接害工龄
        /// </summary>
        [StringLength(16)]
        public virtual string InjuryAge { get; set; }

        /// <summary>
        /// 职业健康结论
        /// </summary>
        [StringLength(5000)]
        public virtual string Conclusion { get; set; }

        /// <summary>
        /// 项目组合名称
        /// </summary>     
        public virtual string ItemGroupName { get; set; }



        /// <summary>
        /// 复查备注
        /// </summary>
        public string Remart { get; set; }

    }
}
