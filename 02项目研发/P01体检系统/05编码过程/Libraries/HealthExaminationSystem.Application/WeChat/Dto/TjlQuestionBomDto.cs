#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlQuestionBom))]
#endif
    public class TjlQuestionBomDto
    {

        /// <summary>
        /// 题目
        /// </summary>
        [StringLength(320)]
        public virtual string bomItemName { get; set; }
        /// <summary>
        /// 题目类型1:填空;2:单选;3: 复选
        /// </summary>   
        public virtual int? bomItemType { get; set; }
        /// <summary>
        /// 是否必做1:是;0:否
        /// </summary>  
        public virtual int? mustFill { get; set; }

        /// <summary>
        /// 填空题答案
        /// </summary>
        [StringLength(320)]
        public virtual string answerContent { get; set; }

        /// <summary>
        /// 选项题备选项
        /// </summary>      
        public virtual List<TjlQuestiontemDto> itemList { get; set; }

        /// <summary>
        /// 题目
        /// </summary>
        [StringLength(320)]
        public virtual string Title { get; set; }
        /// <summary>
        /// 序号
        /// </summary>   
        public virtual int? OrderNum { get; set; }

    }
}
