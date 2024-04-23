using System.Linq;
using System.ComponentModel.DataAnnotations;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Newtonsoft.Json;
using System;
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
    public class ItemSuitDto : SimpleItemSuitDto
    {
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
        /// 体检类别字典 -1ALL体检类别字典
        /// </summary>
        public virtual int? ExaminationType { get; set; }

        /// <summary>
        /// 结果差价
        /// </summary>
        public virtual decimal? CjPrice { get; set; }

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
        /// 是否备孕 1备孕2不备孕
        /// </summary>
        public virtual int? ConceiveStatus { get; set; }
        /// <summary>
        /// 是否有效期0否1是
        /// </summary>
        public virtual int? IsendDate { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public virtual DateTime? endDate { get; set; }

        /// <summary>
        /// 公卫编码
        /// </summary>     
        [StringLength(64)]
        public virtual string GWBM { get; set; }


        //        #region 格式化

        //        /// <summary>
        //        /// 婚姻格式化
        //        /// </summary>
        //#if Application
        //        [IgnoreMap]
        //#endif
        //        [JsonIgnore]
        //        public virtual string FormatMarital => MarrySateHelper.GetMarrySateModelsForItemInfo().Where(o => o.Id == MaritalStatus).Select(o => o.Display).FirstOrDefault();

        //        /// <summary>
        //        /// 是否备孕格式化
        //        /// </summary>
        //#if Application
        //        [IgnoreMap]
        //#endif
        //        [JsonIgnore]
        //        public virtual string FormatConceive => ConceiveStatus == 1 ? "是" : "否";

        //        #endregion

    }
}