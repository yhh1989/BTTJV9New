using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
  public   class reportitemInfo
    {
        
        /// <summary>
        /// 项目结果，中文说明 数值型也存入
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemResultChar { get; set; }

        /// <summary>
        /// 项目标示 H偏高 HH超高L偏低 LL 超低M正常P异常
        /// </summary>
        [StringLength(16)]
        public virtual string Symbol { get; set; }

        /// <summary>
        /// 项目单位
        /// </summary>
        [StringLength(16)]
        public virtual string Unit { get; set; }

        /// <summary>
        /// 参考值
        /// </summary>
        [StringLength(256)]
        public virtual string Stand { get; set; }



        /// <summary>
        /// 项目组合名称
        /// </summary>      
        [StringLength(32)]
        public virtual string ItemGroupName { get; set; }
        /// <summary>
        /// 项目组合名称
        /// </summary>      
        [StringLength(32)]
        public virtual string ItemName { get; set; }
        /// <summary>
        /// 科室小结
        /// </summary>      
        [StringLength(32)]
        public virtual string DeptSum { get; set; }
        /// <summary>
        /// 小结医生
        /// </summary>      
        [StringLength(32)]
        public virtual string DeptEmp { get; set; }

        /// <summary>
        /// 检查医生（检查者）
        /// </summary>
        public virtual string CheckDoctor { get; set; }

        /// <summary>
        /// 审核医生
        /// </summary>
        public virtual string CheckEmployeDoctor { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>      
        [StringLength(32)]
        public virtual string DeptName { get; set; }
        /// <summary>
        /// 项目状态 1未检查2已检查3部分检查4放弃5待查
        /// </summary>
        public virtual int? ProcessState { get; set; }

    }
}
