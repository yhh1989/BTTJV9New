﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
    public class ReportItemSuitDto
    {
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public virtual DateTime? LastTime { get; set; }

        /// <summary>
        /// 套餐组合对照表
        /// </summary>
        public virtual List<ReportItemSuitItemGroupDto> ItemSuitItemGroups { get; set; }

        /// <summary>
        /// 套餐编码
        /// </summary>
        [StringLength(64)]
        public virtual string ItemSuitID { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>
        [StringLength(64)]
        public virtual string ItemSuitName { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(64)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 五笔码
        /// </summary>
        [StringLength(64)]
        public virtual string WBCode { get; set; }

        /// <summary>
        /// 性别
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
        /// 单价
        /// </summary>
        public virtual decimal? Price { get; set; }

        /// <summary>
        /// 成本价
        /// </summary>
        public virtual decimal? CostPrice { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual int? Available { get; set; }

        /// <summary>
        /// 注意事项
        /// </summary>
        [StringLength(256)]
        public virtual string Notice { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(256)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? OrderNum { get; set; }

        /// <summary>
        /// 体检类别字典 -1ALL体检类别字典
        /// </summary>
        public virtual int? ExaminationType { get; set; }

        /// <summary>
        /// 结果差价
        /// </summary>
        public virtual decimal? CjPrice { get; set; }

        /// <summary>
        /// 1基础套餐2组单3加项
        /// </summary>
        public virtual int? ItemSuitType { get; set; }

        //新增 待功能完善
        /// <summary>
        /// 结婚状态
        /// </summary>
        public virtual int? MaritalStatus { get; set; }

        /// <summary>
        /// 危害因素
        /// </summary>
        [StringLength(256)]
        public virtual string RiskName { get; set; }

        /// <summary>
        /// 是否备孕 1备孕2不备孕2其他
        /// </summary>
        public virtual int? ConceiveStatus { get; set; }
        /// <summary>
        /// 是否全部1是0否
        /// </summary> 
        public virtual int? State { get; set; }

    }
}
