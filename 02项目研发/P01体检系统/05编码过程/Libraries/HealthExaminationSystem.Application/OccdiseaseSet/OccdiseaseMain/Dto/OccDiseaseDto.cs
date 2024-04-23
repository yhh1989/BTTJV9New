using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using System;
#endif
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseSet.OccdiseaseMain.Dto
{

   
    public class OccDiseaseDto : EntityDto<Guid>
    {
        /// <summary>
        /// 父级单位标识（如行业大类，症状大类）
        /// </summary>
        
        public virtual Guid? ParentId { get; set; }
        /// <summary>
        /// 一级分类
        /// </summary>
        public virtual string  ParentName { get; set; }
        /// <summary>
        /// 字典类别，来自程序的枚举
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(500)]
        public virtual string Text { get; set; }
        /// <summary>
        /// 助记码
        /// </summary>
       
        public virtual string HelpChar { get; set; }
        /// <summary>
        /// 是否启用1启用0停用
        /// </summary>   
        public virtual int IsActive { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
    
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? OrderNum { get; set; }

        /// <summary>
        /// 职业健康Id
        /// </summary>

        public virtual Guid? OccDiseasesId { get; set; }

        /// <summary>
        /// 标准编号
        /// </summary>      
        public virtual string StandardNo { get; set; }

        /// <summary>
        /// 职业健康标准
        /// </summary>
        public virtual string StandardName { get; set; }
        /// <summary>
        /// 是否默认1默认0否
        /// </summary>     
        public virtual int IsShow { get; set; }
        /// <summary>
        /// 名称
        /// </summary>

        public virtual string standText { get; set; }


    }
}
