using Abp.Application.Services.Dto;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.HealthCard;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CarSend.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TbmCardType))]
#endif
    
   public  class SimpCardTypeDto :EntityDto<Guid>
    {
        /// <summary>
        /// 编号
        /// </summary>       
        public virtual int CardNum { get; set; }
        /// <summary>
        /// 卡名
        /// </summary>
        [StringLength(64)]
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
    }
}
