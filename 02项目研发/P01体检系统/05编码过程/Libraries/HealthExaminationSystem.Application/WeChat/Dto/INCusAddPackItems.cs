#if Application
using Abp.AutoMapper;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
    
    public   class INCusAddPackItems
    {
        /// <summary>
        /// 加项包表ID
        /// </summary>      
        public virtual Guid? ItemSuitID { get; set; }
        /// <summary>
        /// 组合名称
        /// </summary>   
        public virtual Guid? ItemGroupID { get; set; }
        /// <summary>
        /// 项目组合名称
        /// </summary>      
        [StringLength(32)]
        public virtual string ItemGroupName { get; set; }
        /// <summary>
        /// 状态0未选1已选
        /// </summary>
        public virtual int CheckState { get; set; }
    }
}
