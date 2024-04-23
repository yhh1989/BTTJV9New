using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReSultStatus.Dto
{
 public    class ReSultCusSumDto
    {
        /// <summary>
        /// 体检人id
        /// </summary>    
        public virtual Guid? CustomerRegid { get; set; }


        /// <summary>
        /// 总检结论
        /// </summary>
        [StringLength(3072)]
        public virtual string CharacterSummary { get; set; }
        /// <summary>
        /// 总检建议
        /// </summary>
        [StringLength(8192)]
        public virtual string Advice { get; set; }


        /// <summary>
        /// 建议内容
        /// </summary>
        [StringLength(3072)]
        public virtual string AdviceContent { get; set; }

        /// <summary>
        /// 建议顺序
        /// </summary>
        public virtual int? SummarizeOrderNum { get; set; }

        /// <summary>
        ///是否职业健康项目1职业健康项目2否
        /// </summary>
        public virtual int? IsZYB { get; set; }
        /// <summary>
        /// 职业健康总检结论
        /// </summary>
        [StringLength(3072)]
        public virtual string occCharacterSummary { get; set; }
       
    }
}
