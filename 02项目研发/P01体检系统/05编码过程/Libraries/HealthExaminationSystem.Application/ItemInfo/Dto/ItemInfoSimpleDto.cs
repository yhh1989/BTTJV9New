using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmItemInfo))]
#endif 
    public class ItemInfoSimpleDto : EntityDto<Guid>
    {
        /// <summary>
        /// 单位
        /// </summary>
        public virtual string Unit { get; set; }
        /// <summary>
        /// 科室
        /// </summary>
        public virtual DepartmentIdNameDto Department { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        [StringLength(64)]
        public virtual string ItemBM { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [StringLength(64)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 项目打印名称
        /// </summary>
        [StringLength(64)]
        public virtual string NamePM { get; set; }

        /// <summary>
        /// 项目英文名称
        /// </summary>
        [StringLength(64)]
        public virtual string NameEngAbr { get; set; }

        /// <summary>
        /// 项目序号
        /// </summary>
        public virtual int? OrderNum { get; set; }
        /// <summary>
        /// 项目介绍
        /// </summary>
        public virtual string Remark { get; set; }//注意事项Notice
         /// <summary>
        /// 项目类别 1数值型2计算型3说明型4阴阳性
        /// </summary>
        public virtual int? moneyType { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>   
        public virtual string HelpChar { get; set; }


        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsActive { get; set; }


        /// <summary>
        /// 是否进入小结 1进入2不进入
        /// </summary>
        public virtual int? IsSummary { get; set; }


        /// <summary>
        /// 标准编码
        /// </summary> 
        public virtual string StandardCode { get; set; }

        /// <summary>
        /// 临界值最小值
        /// </summary>
        public virtual decimal? MinValue { get; set; }

        /// <summary>
        /// 临界值最大值
        /// </summary>
        public virtual decimal? MaxValue { get; set; }
        /// <summary>
        /// 是否启用临界值
        /// </summary>
        public virtual int? ISLJ { get; set; }

    }
}
