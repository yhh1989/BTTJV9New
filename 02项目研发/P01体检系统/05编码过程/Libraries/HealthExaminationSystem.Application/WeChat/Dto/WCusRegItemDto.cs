using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
  public  class WCusRegItemDto
    {
        
        /// <summary>
        /// 体检人检查项目组合id
        /// </summary>
        public virtual string  ItemGroupBM { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public virtual string ItemBM { get; set; }
        

        /// <summary>
        /// 项目名称
        /// </summary>
        [StringLength(32)]
        public virtual string ItemName { get; set; }        

      

        /// <summary>
        /// 项目状态 1未检查2已检查3部分检查4放弃5待查
        /// </summary>
        public virtual int? ProcessState { get; set; }
       

        /// <summary>
        /// 检查人ID外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
      
        public virtual string InspectEmployeeName { get; set; }

        
        /// 审核人ID外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>       
        public virtual string CheckEmployeeName { get; set; }

       

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
        /// 阳性状态 1正常2阳性
        /// </summary>
        public virtual int? PositiveSate { get; set; }

        /// <summary>
        /// 疾病状态 1疾病2重大疾病3阳性发现
        /// </summary>
        public virtual int? IllnessSate { get; set; }

        /// <summary>
        /// 危急值状态 1正常2危急值
        /// </summary>
        public virtual int? CrisisSate { get; set; }
       

        /// <summary>
        /// 参考值
        /// </summary>
        [StringLength(256)]
        public virtual string Stand { get; set; }
       

        /// <summary>
        /// 项目单位
        /// </summary>
        [StringLength(16)]
        public virtual string Unit { get; set; }

        /// <summary>
        /// 项目小结
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemSum { get; set; }
        /// <summary>
        /// 项目诊断
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemDiagnosis { get; set; }
        /// <summary>
        /// 重度等级 1.轻微、2.中度、3.重度
        /// </summary>
        public virtual int? IllnessLevel { get; set; }
    }
}
