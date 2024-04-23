using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
#endif
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison.Dto
{
    /// <summary>
    /// 项目对应
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TdbInterfaceItemComparison))]
#endif
    public class InterfaceItemsDto : EntityDto<Guid>
    {
        /// <summary>
        /// 科室
        /// </summary>
        public virtual string  DepartmentName { get; set; }

        /// <summary>
        /// 科室ID
        /// </summary>      
        public virtual Guid DepartmentId { get; set; }

        /// <summary>
        /// 组合
        /// </summary>
        public virtual string  ItemGroupName { get; set; }

        /// <summary>
        /// 组合ID
        /// </summary>     
        public virtual Guid? ItemGroupId { get; set; } 

        /// <summary>
        /// 项目
        /// </summary>
        public virtual string ItemBM { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>      
        public virtual Guid ItemInfoId { get; set; }

        /// <summary>
        /// 项目名称?
        /// </summary>
        [StringLength(64)]
        public virtual string ItemName { get; set; }



        /// <summary>
        /// 对应项目ID
        /// </summary>
        public virtual string ObverseItemId { get; set; }

        /// <summary>
        /// 对应项目名称?
        /// </summary>
        [StringLength(64)]
        public virtual string ObverseItemName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(1024)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 仪器型号
        /// </summary>
        [StringLength(1024)]
        public virtual string InstrumentModelNumber { get; set; }

        /// <summary>
        /// 导入状态
        /// </summary>
        [StringLength(64)]
        public virtual string CheckState { get; set; }
        /// <summary>
        /// 科室序号
        /// </summary>
        public virtual int? departNum { get; set; }
        /// <summary>
        /// 组合序号
        /// </summary>
        public virtual int? GroupNum { get; set; }

        /// <summary>
        /// 项目序号
        /// </summary>
        public virtual int? itemNum { get; set; }


    }
}
