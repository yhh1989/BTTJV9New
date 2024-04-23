using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.MemberShipCard.Dto
{
   public class FullTbmCardTypeDto
    {
        /// <summary>
        /// 体检卡
        /// </summary>
        public TbmCardTypeDto OneTbmCardType { get; set; }
        /// <summary>
        /// 多个套餐
        /// </summary>
        public List<Guid> ManyTbmItemSuit { get; set; }
    }
}
