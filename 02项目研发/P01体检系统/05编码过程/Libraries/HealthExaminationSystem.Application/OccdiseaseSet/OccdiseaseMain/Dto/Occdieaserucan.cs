using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseSet.OccdiseaseMain.Dto
{
   public class Occdieaserucan
    {
        /// <summary>
        /// 父级单位标识（如行业大类，症状大类）
        /// </summary>

        public virtual Guid? ParentId { get; set; }
        /// <summary>
        /// 一级分类
        /// </summary>
        public virtual string ParentName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(500)]
        public virtual string Text { get; set; }


        /// <summary>
        /// 是否启用1启用0停用
        /// </summary>   
        public virtual int IsActive { get; set; }
    }
}
