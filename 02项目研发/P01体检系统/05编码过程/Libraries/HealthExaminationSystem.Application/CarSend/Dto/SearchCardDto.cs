#if !Proxy
using Abp.Domain.Entities;
#endif
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CarSend.Dto
{
    public class SearchCardDto:Entity<Guid>
    {
        /// <summary>
        /// 卡号
        /// </summary>       
        [StringLength(64)]
        public virtual string CardNo { get; set; }
        /// <summary>
        /// 卡类别
        /// </summary>
        public virtual Guid? CardType { get; set; }
        /// <summary>
        /// 开卡人
        /// </summary>
        public virtual long? CreatUser { get; set; }
        /// <summary>
        /// 销售员
        /// </summary>
        public virtual long? SendUser { get; set; }
        /// <summary>
        /// 销售方式
        /// </summary>
        public virtual int? SendType { get; set; }

        /// <summary>
        /// 卡类别
        /// </summary>       
        [StringLength(64)]
        public virtual string CardCategory { get; set; }
        /// <summary>
        /// 单位分组ID
        /// </summary>
        public virtual Guid? ClientTeamInfoId { get; set; }

        /// <summary>
        /// 单位预约ID
        /// </summary>
        public virtual Guid? ClientRegId { get; set; }

    }
}
