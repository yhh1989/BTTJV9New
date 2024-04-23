
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#if !Proxy
using Abp.AutoMapper;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.HealthCard;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.MemberShipCard.Dto
{

#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TbmCardType))]
#endif
    public class ShowTbmCardTypeDto : EntityDto<Guid>
    {
        /// <summary>
        /// 编号
        /// </summary>       
        public virtual int CardNum { get; set; }
        /// <summary>
        /// 卡名
        /// </summary>
        public virtual string CardName { get; set; }
        /// <summary>
        /// 1有期限2无期限
        /// </summary>
        public virtual int Term { get; set; }
        /// <summary>
        /// 有期限（多少月）
        /// </summary>
        public virtual int? Months { get; set; }
        /// <summary>
        /// 面值
        /// </summary>
        public virtual decimal FaceValue { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public virtual string CardType { get; set; }
        /// <summary>
        /// 是否启用枚举InvoiceState
        /// </summary>
        public virtual int? Available { get; set; }
#if Application
        [IgnoreMap]
#endif
        public virtual string ItemSuitName
        {
            get
            {
                if (ItemSuits == null)
                {
                    return "";
                }
                else
                {
                    var ItemSuitName = ItemSuits.Select(o => o.ItemSuitName).ToList();
                    return string.Join(",", ItemSuitName);
                }
            }
        }
        /// <summary>
        /// 关联套餐
        /// </summary>
        public virtual ICollection<TbmItemSuitDto> ItemSuits { get; set; }

        public DateTime? CreationTime { get; set; }
    }
}
