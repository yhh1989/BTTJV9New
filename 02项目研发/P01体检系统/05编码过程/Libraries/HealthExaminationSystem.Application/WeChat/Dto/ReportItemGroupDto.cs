using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
    public class ReportItemGroupDto
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
        /// 组合编码
        /// </summary>    
        public virtual string ItemGroupBM { get; set; }

        /// <summary>
        /// 项目组合名称
        /// </summary>       
        public virtual string ItemGroupName { get; set; }
        /// <summary>
        /// 项目列表
        /// </summary>
        public virtual List<ReportGroupItems> GroupItems { get; set; }

        /// <summary>
        /// HIS组合ID
        /// </summary>      
        public virtual string HISID { get; set; }

        /// <summary>
        /// HIS组合名称
        /// </summary>     
        public virtual string HISName { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>     
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 五笔码
        /// </summary>       
        public virtual string WBCode { get; set; }

        /// <summary>
        /// 性别 1男2女3不限制
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
        /// 单价 最小折扣核算后的价格
        /// </summary>
        public virtual decimal? Price { get; set; }

        /// <summary>
        /// 成本价
        /// </summary>
        public virtual decimal? CostPrice { get; set; }       

        /// <summary>
        /// 注意事项
        /// </summary>
        [StringLength(256)]
        public virtual string Notice { get; set; }

        /// <summary>
        /// 组合介绍
        /// </summary>
        [StringLength(256)]
        public virtual string ItemGroupExplain { get; set; }

        /// <summary>
        /// 备注说明
        /// </summary>
        [StringLength(256)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 组合序号
        /// </summary>
        public virtual int? OrderNum { get; set; }
        

        /// <summary>
        /// 是否隐私项目 1是2不是
        /// </summary>
        public virtual int? PrivacyState { get; set; }

        /// <summary>
        /// 是否抽血 1是2不是
        /// </summary>
        public virtual int? DrawState { get; set; }

        /// <summary>
        /// 餐前餐后 1餐前2餐后
        /// </summary>
        public virtual int? MealState { get; set; }

        /// <summary>
        /// 是否妇检 1是2不是
        /// </summary>
        public virtual int? WomenState { get; set; }

        /// <summary>
        /// 禁忌备注
        /// </summary>
        [StringLength(256)]
        public virtual string Taboo { get; set; }
      
       

        /// <summary>
        /// 最大折扣
        /// </summary>
        public virtual decimal? MaxDiscount { get; set; }

        /// <summary>
        /// 外送状态 1外送 2自己做
        /// </summary>
        public virtual int? OutgoingState { get; set; }    

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 是否全部1是0否
        /// </summary> 
        public virtual int? State { get; set; }
    }
}
