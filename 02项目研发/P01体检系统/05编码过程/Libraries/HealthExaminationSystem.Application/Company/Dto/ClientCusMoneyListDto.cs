#if !Proxy
using AutoMapper;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
   public  class ClientCusMoneyListDto
    {

        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }
        /// <summary>
        /// 体检号
        /// </summary>        
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 姓名
        /// </summary> 
        public virtual string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>    
        public virtual string IDCardNo { get; set; }

    
        /// <summary>
        ///组合名称
        /// </summary>    
        public virtual ICollection<ItemGroupNameDto> ItemGroupNamelist { get; set; }
        /// <summary>
        /// 个人支付总计
        /// </summary>
        public virtual decimal? PersonageToPay { get; set; }
        /// <summary>
        /// 个人已支付总计
        /// </summary>
        public virtual decimal? PersonageHavePay { get; set; }
#if Application
        [IgnoreMap]
#endif
        public virtual string ItemGroupNames
        {
            get
            {
                if (ItemGroupNamelist != null && ItemGroupNamelist.Count > 0)
                {
                    return string.Join(",", ItemGroupNamelist.Select(p=>p.ItemGroupName).ToList());
                }
                else
                {
                    return "";
                }
            }

        }


    }
}
