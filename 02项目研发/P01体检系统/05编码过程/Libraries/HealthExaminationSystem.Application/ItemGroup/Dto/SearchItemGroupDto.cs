using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto
{
    public class SearchItemGroupDto : EntityDto<Guid>
    {      
        /// <summary>
        /// 科室Id
        /// </summary>
        public virtual Guid DepartmentId { get; set; }
        
        /// <summary>
        /// 项目组合名称
        /// </summary>
        [StringLength(64)]
        public virtual string QueryText { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(64)]
        public virtual string HelpChar { get; set; }

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
        /// 收费类别编码
        /// </summary>
        [StringLength(64)]
        public virtual string ChartCode { get; set; }

        /// <summary>
        /// 收费类别名称
        /// </summary>
        [StringLength(64)]
        public virtual string ChartName { get; set; }

        /// <summary>
        /// 注意事项
        /// </summary>
        [StringLength(256)]
        public virtual string Notice { get; set; }
        
        /// <summary>
        /// 是否属于收费项目 0收费1不收费 默认 收费
        /// </summary>
        public virtual int? ISSFItemGroup { get; set; }

        /// <summary>
        /// 标本类型 字典
        /// </summary>
        public virtual int? SpecimenType { get; set; }

        /// <summary>
        /// 是否用打条码  1是2不是
        /// </summary>
        public virtual int? BarState { get; set; }

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
        /// 试管样式 字典
        /// </summary>
        [StringLength(64)]
        public virtual string TubeType { get; set; }

        /// <summary>
        /// 是否早餐 1不是2是
        /// </summary>
        public virtual int? Breakfast { get; set; }

        /// <summary>
        /// 最大体检人数
        /// </summary>
        public virtual int? MaxCheckDay { get; set; }
        
        /// <summary>
        /// 自动VIP项目  1自动vip 0不自动 如果选该项目自动成为VIP
        /// </summary>
        public virtual int? AutoVIP { get; set; }
        
        /// <summary>
        /// 外送状态 1外送 2自己做
        /// </summary>
        public virtual int? OutgoingState { get; set; }

        /// <summary>
        /// 是否询问自愿（如妇科，乙肝）  1是 0否
        /// </summary>
        public virtual int? VoluntaryState { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int? IsActive { get; set; }

    }
}
