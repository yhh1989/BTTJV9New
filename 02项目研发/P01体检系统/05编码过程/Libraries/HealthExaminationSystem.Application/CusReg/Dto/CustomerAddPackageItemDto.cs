using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlCustomerAddPackageItem))]
#endif
    public class CustomerAddPackageItemDto 
    {
        /// <summary>
        /// 体检人预约标识
        /// </summary>
       
        public virtual Guid? CustomerRegId { get; set; }

      
        /// <summary>
        /// 加项包表ID
        /// </summary>     
        public virtual Guid? ItemSuitID { get; set; }
       
        /// <summary>
        /// 个人加项包表
        /// </summary>    
        public virtual Guid? TjlCusAddpackagesID { get; set; }
       
        /// <summary>
        /// 组合名称
        /// </summary>

        public virtual SimpleItemGroupDto ItemGroup { get; set; }

        /// <summary>
        /// 项目组合名称
        /// </summary>      
        [StringLength(64)]
        public virtual string ItemGroupName { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual decimal? Suitgrouprate { get; set; }

        /// <summary>
        /// 项目原价
        /// </summary>
        public virtual decimal? ItemPrice { get; set; }

        /// <summary>
        /// 折扣后价格
        /// </summary>
        public virtual decimal? PriceAfterDis { get; set; }


        /// <summary>
        /// 选择状态0未选，1已选
        /// </summary>
        public int CheckState { get; set; }
    }
}
