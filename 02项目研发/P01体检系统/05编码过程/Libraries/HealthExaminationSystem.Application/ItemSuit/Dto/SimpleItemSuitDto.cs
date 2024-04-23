using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using System.ComponentModel.DataAnnotations;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Newtonsoft.Json;
#if !Proxy
using AutoMapper;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto
{ 
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TbmItemSuit))]
#endif
    public class SimpleItemSuitDto : EntityDto<Guid>
    {
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
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }
        /// <summary>
        /// 五笔码
        /// </summary>
        [StringLength(64)]
        public virtual string WBCode { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? OrderNum { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual int? Available { get; set; }

        /// <summary>
        /// 1基础套餐2组单3加项
        /// </summary>
        public virtual int? ItemSuitType { get; set; }
        /// <summary>
        /// 体检类别字典 -1ALL体检类别字典
        /// </summary>
        public virtual int? ExaminationType { get; set; }
        /// <summary>
        /// 是否有效期0否1是
        /// </summary>
        public virtual int? IsendDate { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public virtual DateTime? endDate { get; set; }


        //        #region 格式化

        //        /// <summary>
        //        /// 性别格式化
        //        /// </summary>
        //#if Application
        //        [IgnoreMap]
        //#endif
        //        [JsonIgnore]
        //        public virtual string FormatSex => SexHelper.GetSexModelsForItemInfo().Where(o => o.Id == Sex).Select(o => o.Display).FirstOrDefault();

        //        #endregion

    }
}
